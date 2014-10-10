struct VS_IN
{
	float4 pos : POSITION;
	float3 nrm : NORMAL;
	float4 tex0 : TEXCOORD0;
	
	#ifdef LUCID_HAS_BUMPTEX
		float4 tan : TANGENT;
	#endif
};  

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 tex0 : TEXCOORD0;
	float3 nrm : TEXCOORD1;
	float3 lVec : TEXCOORD2;
	float3 vVec : TEXCOORD3;
	
	#ifdef LUCID_HAS_BUMPTEX
		float4 tan : TEXCOORD4;
	#endif
};

PS_IN vert(VS_IN vsInput)
{
	PS_IN vsOutput;
	
	vsOutput.pos = mul(UNITY_MATRIX_MVP, vsInput.pos);
	vsOutput.tex0 = vsInput.tex0;
	vsOutput.nrm = vsInput.nrm;
	vsOutput.lVec = ObjSpaceLightDir(vsInput.pos);
	vsOutput.vVec = ObjSpaceViewDir(vsInput.pos);
	
	#ifdef LUCID_HAS_BUMPTEX
		vsOutput.tan = vsInput.tan;
	#endif
	
	return vsOutput;
}

fixed4 frag(PS_IN psInput) : COLOR
{
	// Normalise input vectors
	float3 lVector = normalize(psInput.lVec);
	float3 vVector = normalize(psInput.vVec);

	// Sample textures
	float2 texCoord = psInput.tex0.xy;
	float4 texCol = tex2D(_MainTex, texCoord);
	#ifdef LUCID_HAS_BUMPTEX
		float4 bmpCol = tex2D(_BumpTex, texCoord);
		
		// Create a matrix to convert between object-space and tangent-space
		// If we assume this is an orthogonal matrix, we can use the transpose as the inverse
		float3 vertVecN = psInput.nrm;
		float3 vertVecT = psInput.tan.xyz;
		float3 vertVecB = cross(vertVecN, vertVecT) * psInput.tan.w;
		float3x3 obj2tan = float3x3(vertVecT, vertVecB, vertVecN);
		
		// Offset for parallax mapping
		#ifdef LUCID_USE_PARALLAX
			texCoord += calcOffsetTexCoord(mul(obj2tan, vVector), bmpCol.x);
		#endif
		
		// Sample object-space normal
		float3 normal = mul(sampleNormal(texCoord), obj2tan);
	#else
		float3 normal  = normalize(psInput.nrm);
	#endif
	
	// Calculate attenuation from light
	float attenuation = calcAttenuationCartoon(psInput.lVec);
	
	// Calculate the specularity of the sample
	#ifdef LUCID_HAS_BUMPTEX
		float specularity = bmpCol.y;
	#else
		float specularity = 0.f;
	#endif
	 
	// Get component parts
	float4 outDiff = _LightColor0 * shadeDiff(texCol.xyz, normal, lVector, attenuation);
	float4 outSpec = _LightColor0 * shadeSpec(texCol.xyz, 0.f, normal, lVector, vVector, attenuation);
	
	#ifdef LUCID_HAS_AMBIENT
		outDiff.xyz += UNITY_LIGHTMODEL_AMBIENT.xyz * texCol.xyz;
	#endif
	
	// Mix component parts
	float4 finalColour = outDiff + outSpec;
	
	// Override transparency
	#ifdef LUCID_HAS_BUMPTEX
		finalColour.a = bmpCol.z;
	#else
		finalColour.a = 1.f;
	#endif
	return outDiff + outSpec;
}