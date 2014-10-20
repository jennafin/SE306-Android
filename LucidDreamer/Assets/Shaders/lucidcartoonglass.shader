Shader "Custom/lucidcartoonglass"
{
	Properties
	{
		_Colour ("Base Colour", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags {"Queue" = "Transparent"}
		
		Pass
		{
			ZWrite Off
			Blend One OneMinusSrcAlpha
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			
			#define LUCID_HAS_AMBIENT
			#define LUCID_USE_BASE
			#define LUCID_USE_NOTEX
			#define LUCID_USE_GLASS
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
		
		Pass
		{
			Tags {"Queue" = "Transparent" "LightMode" = "ForwardAdd"}
			Fog {Color (0,0,0,0)}
			Blend One One
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdadd
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			
			#define LUCID_USE_NOTEX
			#define LUCID_USE_GLASS
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
