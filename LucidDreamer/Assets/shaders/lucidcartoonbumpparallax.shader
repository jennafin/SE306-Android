Shader "Custom/lucidcartoonbumpparallax"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpTex ("Bump/Spec/Opacity (RGB)", 2D) = "white"
		_BumpSzX ("Bump/Spec/Opacity width", Float) = 512
		_BumpSzY ("Bump/Spec/Opacity height", Float) = 512
		_BumpSzZ ("Bump/Spec/Opacity depth", Float) = 16
	}
	SubShader
	{
		Tags {"Queue" = "Geometry" "RenderType" = "Opaque"} // TransparentCutout
		
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			
			#define LUCID_HAS_BUMPTEX
			#define LUCID_HAS_AMBIENT
			#define LUCID_USE_BASE
			#define LUCID_USE_PARALLAX
			uniform sampler2D _MainTex;
			uniform sampler2D _BumpTex;
			uniform half _BumpSzX;
			uniform half _BumpSzY;
			uniform half _BumpSzZ;
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
			
			#define LUCID_HAS_BUMPTEX
			#define LUCID_HAS_AMBIENT
			#define LUCID_USE_PARALLAX
			uniform sampler2D _MainTex;
			uniform sampler2D _BumpTex;
			uniform half _BumpSzX;
			uniform half _BumpSzY;
			uniform half _BumpSzZ;
			fixed4 _LightColor0;
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
