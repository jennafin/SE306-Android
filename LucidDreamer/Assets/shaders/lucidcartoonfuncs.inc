float posterise2(float rawValue, float step0, float step1)
{
	return 0.5f * (step(step0, rawValue) + step(step1, rawValue));
}

float calcFresnelFactor(float3 norm, float3 vVec)
{
	return pow(1.f + saturate(dot(norm, vVec)), -3.0);
}

float calcFresnelFactorCartoon(float3 norm, float3 vVec)
{
	return step(-0.5f, -dot(norm, vVec));
}

float perpReflect(float3 vector, float3 norm)
{
	return vector - norm * dot(vector, norm);
}

float2 calcSpecFactorsCartoon(float albedo, float3 norm, float3 lVec, float3 vVec, float attenuation)
{
	float rimLight	 = lerp(1.f, albedo, saturate(dot(vVec, norm)));
	rimLight		*= lerp(0.5f, 1.f, dot(reflect(-lVec, norm), vVec));
	rimLight		*= attenuation;
	return float2(step(0.3333f, rimLight), step(0.6667f, rimLight));
}

float calcDiffuseFactor(float3 norm, float3 lVec, float attenuation)
{
	return saturate(dot(norm, lVec)) * attenuation;
}

float calcDiffuseFactorCartoon(float3 norm, float3 lVec, float attenuation)
{
	return posterise2(dot(norm, lVec) * attenuation, 0.01f, 0.5f);
}

float4 shadeDiff(float3 diff, float3 norm, float3 lVec, float attenuation)
{
	float3 diffOut = diff * calcDiffuseFactorCartoon(norm, lVec, attenuation);
	return float4(diffOut, 1.0);
}

float4 shadeSpec(float3 diff, float albedo, float3 norm, float3 lVec, float3 vVec, float attenuation)
{
	float2 specOut = calcSpecFactorsCartoon(albedo, norm, lVec, vVec, attenuation);
	float3 outCol = lerp(diff, 1.f, specOut.y) * specOut.x;
	
	return float4(outCol, 1.0);
}

float calcAttenuationCartoon(float3 lVec)
{
	#ifdef USING_DIRECTIONAL_LIGHT
		return 1.f;
	#else
		//return tex2D(_LightTexture0, dot(lVec,lVec).rr / unity_4LightAtten0.w).UNITY_ATTEN_CHANNEL;
		//float rawAtten = dot(unity_4LightAtten0.w, unity_4LightAtten0.w) / dot(lVec, lVec);
		lVec = mul(_LightMatrix0, float4(lVec, 0.f)).xyz;
		float rawAtten = dot(unity_4LightAtten0.w, unity_4LightAtten0.w) / dot(lVec, lVec);
		return max(0.f, rawAtten - 1.f);
	#endif
}