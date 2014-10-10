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
			
			#define LUCID_HAS_AMBIENT
			#define LUCID_USE_BASE
			uniform sampler2D _MainTex;
			fixed4 _LightColor0;
			#include "lucidcartoonfuncs.inc"
			
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
			
			uniform sampler2D _MainTex;
			fixed4 _LightColor0;
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
