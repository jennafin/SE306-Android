﻿Shader "Custom/lucidcartoonnotex"
{
	Properties
	{
		_Colour ("Cloud Colour", Color) = (1,1,1,1)
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
			#define LUCID_USE_NOTEX
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
			
			#define LUCID_USE_NOTEX
			#include "lucidcartoonfuncs.inc"
			
			#include "lucidcartooncommon.inc"
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
