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

float perpReflect(float3 v, float3 norm)
{
	return v - norm * dot(v, norm);
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

float2 calcParallaxOffset(float3 vVecTS)
{
	float eyeHorizon = 0.1;
	float2 baseOffset = vVecTS.xy * _BumpSzZ;
	baseOffset /= float2(_BumpSzX, _BumpSzY) * max(vVecTS.z, eyeHorizon);
	return baseOffset;
}

float2 calcOffsetTexCoord(float3 vVecTS, float heightSample)
{
	return calcParallaxOffset(vVecTS) * (heightSample - 1.f);
}

float3 sampleNormal(float2 sampleAtCoord)
{
	float2 texOffX = float2(1.f / _BumpSzX, 0.f);
	float2 texOffY = float2(0.f, 1.f / _BumpSzY);
	
	float3 outNormal = float3(_BumpSzZ, _BumpSzZ, 1.f);
	
	outNormal.x *= tex2D(_BumpTex, sampleAtCoord + texOffX).y - tex2D(_BumpTex, sampleAtCoord - texOffX).y;
	outNormal.y *= tex2D(_BumpTex, sampleAtCoord + texOffY).y - tex2D(_BumpTex, sampleAtCoord - texOffY).y;
	
	return normalize(outNormal);
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