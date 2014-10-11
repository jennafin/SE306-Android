struct VS_IN
{
	float4 pos : POSITION;
	float3 norm : NORMAL;
	float4 tex0 : TEXCOORD0;
};  

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 tex0 : TEXCOORD0;
	float3 norm : TEXCOORD1;
	float3 lVec : TEXCOORD2;
	float3 vVec : TEXCOORD3;
};

PS_IN vert(VS_IN vsInput)
{
	PS_IN vsOutput;
	
	vsOutput.pos = mul(UNITY_MATRIX_MVP, vsInput.pos);
	vsOutput.tex0 = vsInput.tex0;
	vsOutput.norm = vsInput.norm;
	vsOutput.lVec = ObjSpaceLightDir(vsInput.pos);
	vsOutput.vVec = ObjSpaceViewDir(vsInput.pos);
	
	return vsOutput;
}

fixed4 frag(PS_IN psInput) : COLOR
{
	// Calculate derived values from the input vectors
	float3 normal  = normalize(psInput.norm);
	float3 lVector = normalize(psInput.lVec);
	float3 vVector = normalize(psInput.vVec);
	float attenuation = calcAttenuationCartoon(psInput.lVec);
	
	// Sample textures
	float4 texCol = tex2D(_MainTex, psInput.tex0.xy);
	 
	// Get component parts
	float4 outDiff = _LightColor0 * shadeDiff(texCol.xyz, normal, lVector, attenuation);
	float4 outSpec = _LightColor0 * shadeSpec(texCol.xyz, 0.f, normal, lVector, vVector, attenuation);
	
	#ifdef LUCID_HAS_AMBIENT
		outDiff.xyz += UNITY_LIGHTMODEL_AMBIENT.xyz * texCol.xyz;
	#endif
	
	
	// Mix component parts
	return outDiff + outSpec;
}