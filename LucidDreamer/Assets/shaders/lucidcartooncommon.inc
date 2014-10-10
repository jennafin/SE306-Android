struct VS_IN
{
	float4 pos : POSITION;
	float3 nrm : NORMAL;
	float4 tex0 : TEXCOORD0;
	float4 tan : TANGENT;
};  

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 tex0 : TEXCOORD0;
	float3 nrm : TEXCOORD1;
	float4 lVec : TEXCOORD2;
	float3 vVec : TEXCOORD3;
};

PS_IN vert(VS_IN vsInput)
{
	PS_IN vsOutput;
	
	vsOutput.pos	= mul(UNITY_MATRIX_MVP, vsInput.pos);
	vsOutput.tex0	= vsInput.tex0;
	vsOutput.nrm	= vsInput.nrm;
	
	// Create a matrix to convert between object-space and tangent-space
	// If we assume this is an orthogonal matrix, we can use the transpose as the inverse
	float3 vertVecN	= normalize(vsInput.nrm);
	float3 vertVecT	= normalize(vsInput.tan.xyz);
	float3 vertVecB	= cross(vertVecN, vertVecT) * vsInput.tan.w;
	float3x3 obj2tan= float3x3(vertVecT, vertVecB, vertVecN);
	
	vsOutput.lVec.xyz = mul(obj2tan, ObjSpaceLightDir(vsInput.pos));
	vsOutput.lVec.w = calcAttenuationCartoon(vsOutput.lVec.xyz);
	
	vsOutput.vVec = mul(obj2tan, ObjSpaceViewDir(vsInput.pos));
	
	return vsOutput;
}

fixed4 frag(PS_IN psInput) : COLOR
{
	// Normalise input vectors
	float3 lVector = normalize(psInput.lVec.xyz);
	float3 vVector = normalize(psInput.vVec);

	// Sample textures
	float2 texCoord = psInput.tex0.xy;
	float4 texCol = tex2D(_MainTex, texCoord);
	#ifdef LUCID_HAS_BUMPTEX
		float4 bmpCol = tex2D(_BumpTex, texCoord);
		
		// Offset for parallax mapping
		#ifdef LUCID_USE_PARALLAX
			texCoord += calcOffsetTexCoord(vVector, bmpCol.y);
		#endif
		
		// Sample tangent-space normal
		float3 normal = sampleNormal(texCoord);
	#else
		float3 normal  = float3(0.f, 0.f, 1.f);//normalize(psInput.nrm);
	#endif
	
	// Calculate the specularity of the sample
	#ifdef LUCID_HAS_BUMPTEX
		float specularity = bmpCol.x;
	#else
		float specularity = 0.f;
	#endif
	 
	// Get component parts
	float4 outDiff = _LightColor0 * shadeDiff(texCol.xyz, normal, lVector, psInput.lVec.w);
	float4 outSpec = _LightColor0 * shadeSpec(texCol.xyz, specularity, normal, lVector, vVector, psInput.lVec.w);
	
	#ifdef LUCID_HAS_AMBIENT
		outDiff.xyz += UNITY_LIGHTMODEL_AMBIENT.xyz * texCol.xyz;
	#endif
	
	// Mix component parts
	float4 finalColour = outDiff + outSpec;
	
	// Override transparency
	#if defined(LUCID_HAS_BUMPTEX) && defined(LUCID_USE_CUTOUT)
		#ifdef LUCID_USE_BASE
			finalColour.a = saturate(bmpCol.z - _AlphaThres);
			finalColour.rgb = saturate(finalColour.rgb * finalColour.a);
		#else
			finalColour.rgb *= bmpCol.z;
			finalColour.a = 1.f;
		#endif
	#else
		finalColour.a = 1.f;
	#endif
	return finalColour;
}