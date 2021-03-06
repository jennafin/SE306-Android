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
	float3 vVec : TEXCOORD2;
	float3 lVecObj : TEXCOORD3;
	float3 lVecTan : TEXCOORD4;
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
	
	vsOutput.lVecObj = ObjSpaceLightDir(vsInput.pos);
	vsOutput.lVecTan = mul(obj2tan, vsOutput.lVecObj);
	
	vsOutput.vVec = mul(obj2tan, ObjSpaceViewDir(vsInput.pos));
	
	return vsOutput;
}

fixed4 frag(PS_IN psInput) : COLOR
{
	// Normalise input vectors
	float3 lVector = normalize(psInput.lVecTan);
	float3 vVector = normalize(psInput.vVec);

	// Sample textures
	float2 texCoord = psInput.tex0.xy;
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
	#if defined(LUCID_USE_NOTEX)
		float4 texCol = _Colour;
	#elif defined(LUCID_USE_PEARL)
		float4 texCol = tex2D(_MainTex, float2(dot(normal,vVector), dot(normal, lVector)));
	#else
		float4 texCol = tex2D(_MainTex, texCoord);
	#endif
	
	// Calculate the specularity of the sample
	#if defined(LUCID_HAS_BUMPTEX)
		float specularity = bmpCol.x;
	#elif defined(LUCID_USE_NOTEX)
		float specularity = _Colour.a;
	#else
		float specularity = 0.f;
	#endif
	 
	// Get component parts
	float attenuation = calcAttenuationCartoon(psInput.lVecObj);
	float4 outDiff = _LightColor0;
	outDiff *= shadeDiff(texCol.xyz, normal, lVector, attenuation);
	
	#ifdef LUCID_USE_GLOW
		outDiff.rgb += texCol.rgb * bmpCol.b;
	#endif
	#ifdef LUCID_HAS_AMBIENT
		outDiff.rgb += UNITY_LIGHTMODEL_AMBIENT.rgb * texCol.rgb;
	#endif
	
	#if defined(LUCID_USE_GLASS)
		texCol.a = lerp(texCol.a, 1.f, calcFresnelFactorCartoon(normal, vVector));
		float4 outSpec = _LightColor0 * shadeSpecPrecompFresnel(texCol.xyz, specularity, normal, lVector, vVector, attenuation, texCol.a);
		
		outDiff *= texCol.a;
	#else
		float4 outSpec = _LightColor0 * shadeSpec(texCol.xyz, specularity, normal, lVector, vVector, attenuation);
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
	#elif defined(LUCID_USE_GLASS)
		finalColour.a = texCol.a;
	#else
		finalColour.a = 1.f;
	#endif
	
	return finalColour;
}