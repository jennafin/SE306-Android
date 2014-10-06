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

float calcSpecFactorCartoon(float albedo, float3 norm, float3 lVec, float3 vVec)
{
	float rimLight	 = 1.f - saturate(dot(vVec, norm));
	rimLight		*= lerp(0.5f, 1.f, dot(lVec, norm));
	return posterise2(rimLight, 0.3333f, 0.6667f);
}

float calcDiffuseFactor(float3 norm, float3 lVec)
{
	return saturate(dot(norm, lVec));
}

float calcDiffuseFactorCartoon(float3 norm, float3 lVec)
{
	return posterise2(dot(norm, lVec), 0.f, 0.5f);
}

float4 shadeDiff(float3 diff, float3 norm, float3 lVec)
{
	float3 diffOut = diff * calcDiffuseFactorCartoon(norm, lVec);
	return float4(diffOut, 1.0);
}

float shadeSpec(float albedo, float3 norm, float3 lVec, float3 vVec)
{
	float specOut = calcSpecFactorCartoon(albedo, norm, lVec, vVec);
	return specOut;
}