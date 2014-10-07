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
			#pragma multi_compile_fwdbase
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "lucidcartoonfuncs.inc"
			
			uniform sampler2D _MainTex;
			fixed4 _LightColor0;
			
			#define LUCID_HAS_AMBIENT
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
		
		Pass
		{
			Tags {"LightMode" = "ForwardAdd"}
			Blend One One
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdadd
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "lucidcartoonfuncs.inc"
			
			uniform sampler2D _MainTex;
			fixed4 _LightColor0; 
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
