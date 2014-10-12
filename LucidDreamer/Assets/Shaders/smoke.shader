Shader "Custom/smoke"
{
	Properties 
	{
	_Colour			("Cloud Colour", Color) = (1,1,1,1)
	_MainTex		("Cloud Diffuse", 2D) = "white" {}
	_Breeze			("Breeze Direction", Vector) = (-1, 0, 0, 0)
	
	_ResetRate		("Micro Reset Rate", Float) = 0.5
	_MoveRate		("Micro Movement Rate", Float) = 15
	_MoveTex		("Micro Movement Texture", 2D) = "white" {}
	
	_ResetBigRate	("Macro Reset Rate", Float) = 0.5
	_MoveBigRate	("Macro Movement Rate", Float) = 15
	_MoveBigTex		("Macro Movement Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" }
		Pass
		{
			ZWrite Off
			
			Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
			
			CGPROGRAM 
			
			#pragma vertex vert 
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			fixed4 _Colour;
			half4 _Breeze;
			half _ResetRate;
			half _ResetBigRate;
			half _MoveRate;
			half _MoveBigRate;
			sampler2D _MainTex;
			sampler2D _MoveTex;
			sampler2D _MoveBigTex;
			float4 _MainTex_ST;
			float4 _MoveTex_ST;
			
			struct VS_IN
			{
				float4 pos : POSITION;
				float4 tex0 : TEXCOORD0;
			};
			
			struct PS_IN
			{
				float4 pos : SV_POSITION;
				float3 tex0 : TEXCOORD0;
			};
			
			PS_IN vert(VS_IN vsIn)
			{
				PS_IN vsOut;
				vsOut.pos = mul(UNITY_MATRIX_MVP, vsIn.pos);
				vsOut.tex0.xy = vsIn.tex0.xy;
				vsOut.tex0.z = -dot(mul(_Object2World, vsIn.pos).xyz, _Breeze.xyz) / dot(_Breeze.xyz, _Breeze.xyz);
				return vsOut;
			}
			
			half4 getMovementAndWeights(float time, half resetRate)
			{
				float fracTime = frac(time * resetRate);
				half4 movAndWeights;
				movAndWeights.xy = half2(fracTime, fracTime - 1);
				movAndWeights.xy *= _MoveRate / resetRate;
				
				movAndWeights.w = fracTime;//smoothstep(0, 1, fracTime);
				movAndWeights.z = 1 - movAndWeights.w;
				
				return movAndWeights;
			}
			
			fixed4 sampleCloud(float2 pos)
			{
				return tex2D(_MainTex, pos);
			}
			
			half4 sampleMoveBigTex(float pos)
			{
				return tex2D(_MoveBigTex, float2(pos, 0.5f));
			}
			
			half processAlpha(half rawAlpha)
			{
				return sampleMoveBigTex(rawAlpha).b;
			}
			
			fixed4 frag(PS_IN psIn) : COLOR 
			{
				float time = _Time.y + psIn.tex0.z;
				half4 microMW = getMovementAndWeights(time, _ResetRate);
				
				half4 microMov = tex2D(_MoveTex   , psIn.tex0.xy * _MoveTex_ST.xy + _MoveTex_ST.zw).xyxy * 2 - 1;
				half4 macroMov = sampleMoveBigTex(frac(time * _ResetBigRate)).xyxy * 2 - 1;
				macroMov *= _MoveBigRate;
				
				microMov *= _MainTex_ST.xyxy;
				microMov += macroMov;
				microMov *= microMW.xxyy;
				microMov += _MainTex_ST.zwzw;
				
				fixed4 cloudCol = _Colour;
				cloudCol.a = sampleCloud(psIn.tex0.xy * _MainTex_ST.xy + microMov.xy).g * microMW.z;
				cloudCol.a+= sampleCloud(psIn.tex0.xy * _MainTex_ST.xy + microMov.zw).g * microMW.w;
				cloudCol.a*= tex2D(_MainTex, psIn.tex0.xy).r;
				cloudCol.a = processAlpha(cloudCol.a * _Colour.a);
				
				return cloudCol;
			}
			
			ENDCG
		}
	}
}