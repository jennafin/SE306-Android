Shader "Custom/lucidcartoon"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}
		
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "lucidcartoonfuncs.inc"
			
			uniform sampler2D _MainTex;
			fixed4 _LightColor0; 
			
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
				LIGHTING_COORDS(4,5)
			};
			
			PS_IN vert(VS_IN vsInput)
			{
				PS_IN vsOutput;
				
				vsOutput.pos = mul(UNITY_MATRIX_MVP, vsInput.pos);
				vsOutput.tex0 = vsInput.tex0;
				vsOutput.norm = vsInput.norm;
				vsOutput.lVec = ObjSpaceLightDir(vsInput.pos);
				vsOutput.vVec = ObjSpaceViewDir(vsInput.pos);
				
				TRANSFER_VERTEX_TO_FRAGMENT(vsOutput);
				
				return vsOutput;
			}
			
			fixed4 frag(PS_IN psInput) : COLOR
			{
				// Calculate normalised versions of the input vectors
				float3 normal = normalize(psInput.norm);
				float3 lVector = normalize(psInput.lVec);
				float3 vVector = normalize(psInput.vVec);
				
				// Sample textures
				float4 texCol = tex2D(_MainTex, psInput.tex0.xy);
				 
				// Get component parts
				float4 outDiff = _LightColor0 * shadeDiff(texCol.xyz, normal, lVector);                      
				float4 outSpec = _LightColor0 * shadeSpec(0.f, normal, lVector, vVector);
				
				// Mix component parts
				return outDiff + outSpec;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
