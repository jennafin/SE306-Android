Shader "Custom/lucidcartoonbump"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpTex ("Bump/Spec/Opacity (RGB)", 2D) = "white"
		_BumpSzX ("Bump/Spec/Opacity width", Float) = 512
		_BumpSzY ("Bump/Spec/Opacity height", Float) = 512
		_BumpSzZ ("Bump/Spec/Opacity depth", Float) = 1
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
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
		
		Pass
		{
			Tags {"LightMode" = "ForwardAdd"}
			Fog {Color (0,0,0,0)}
			Blend One One
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdadd
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			
			#define LUCID_HAS_BUMPTEX
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
