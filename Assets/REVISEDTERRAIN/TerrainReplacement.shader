/* Code provided by Chris Morris of Six Times Nothing (http://www.sixtimesnothing.com) */
/* Free to use and modify  */


Shader "Hidden/TerrainEngine/Splatmap/Lightmap-FirstPass" {
Properties {
	_Control ("Control (RGBA)", 2D) = "red" {}
	_Splat3 ("Layer 3 (A)", 2D) = "white" {}
	_Splat2 ("Layer 2 (B)", 2D) = "white" {}
	_Splat1 ("Layer 1 (G)", 2D) = "white" {}
	_Splat0 ("Layer 0 (R)", 2D) = "white" {}
	// used in fallback on old cards
	_MainTex ("BaseMap (RGBA)", 2D) = "white" {}
	_Color ("Main Color", Color) = (1,1,1,1)
	
	_SpecColor ("Specular Color", Color) = (1, 1, 1, 1)	
}

SubShader {
	Tags {
		"SplatCount" = "4"
		"Queue" = "Geometry-100"
		"RenderType" = "Opaque"
	}
	
CGPROGRAM
#pragma surface surf BlinnPhong vertex:vert
#pragma target 3.0
#include "UnityCG.cginc"

struct Input {
	float3 worldPos;
	float4 screenPos;
	float2 uv_Control : TEXCOORD0;
	float2 uv_Splat0 : TEXCOORD1;
	float2 uv_Splat1 : TEXCOORD2;
	float2 uv_Splat2 : TEXCOORD3;
	float2 uv_Splat3 : TEXCOORD4;
};

// Supply the shader with tangents for the terrain
void vert (inout appdata_full v) {

	// A general tangent estimation	
	float3 T1 = float3(1, 0, 1);
	float3 Bi = cross(T1, v.normal);
	float3 newTangent = cross(v.normal, Bi);
	
	normalize(newTangent);

	v.tangent.xyz = newTangent.xyz;
	
	if (dot(cross(v.normal,newTangent),Bi) < 0)
		v.tangent.w = -1.0f;
	else
		v.tangent.w = 1.0f;
}

sampler2D _Control;
sampler2D _BumpMap0, _BumpMap1, _BumpMap2, _BumpMap3;
sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
float4 _Mix;
float _SpecularAdd;
float _MixScale;
float _SpecularScale;

void surf (Input IN, inout SurfaceOutput o) {
    float inv;
	half4 splat_control = tex2D (_Control, IN.uv_Control);
	half3 col;
	half4 splat;

	// for testing, rotates the uv coordinates	
	float2 uv_Splat0;
	uv_Splat0.x = IN.uv_Splat0.y;
	uv_Splat0.y = IN.uv_Splat0.x;
	
    inv = 1.0 - _Mix.r;
	splat = tex2D (_Splat0, IN.uv_Splat0);
    col += ((splat_control.r * splat.rgb)*inv)  + (splat_control.r *(tex2D (_Splat0, IN.uv_Splat0 * -_MixScale).rgb)*_Mix.r);	
	o.Normal = splat_control.r * UnpackNormal(tex2D(_BumpMap0, uv_Splat0));
	o.Gloss = splat.a * splat_control.r;
	o.Specular = _SpecularScale * splat_control.r;


	inv = 1.0 - _Mix.g;
	splat = tex2D (_Splat1, IN.uv_Splat1);
    col += ((splat_control.g * splat.rgb)*inv) + (splat_control.g *(tex2D (_Splat1, IN.uv_Splat1 * -_MixScale).rgb)*_Mix.g);
	o.Normal += splat_control.g * UnpackNormal(tex2D(_BumpMap1, IN.uv_Splat1));
	o.Gloss += splat.a * splat_control.g;
	o.Specular += _SpecularScale * splat_control.g;
	
	inv = 1.0 - _Mix.b;
	splat = tex2D (_Splat2, IN.uv_Splat2);
    col += ((splat_control.b * splat.rgb)*inv) + (splat_control.b *(tex2D (_Splat2, IN.uv_Splat2 * -_MixScale).rgb)*_Mix.b);	
	o.Normal += splat_control.b * UnpackNormal(tex2D(_BumpMap2, IN.uv_Splat2));
	o.Gloss += splat.a * splat_control.b;
	o.Specular += _SpecularScale * splat_control.b;
	
	inv = 1.0 - _Mix.a;
	splat = tex2D (_Splat3, IN.uv_Splat3);
    col += ((splat_control.a * splat.rgb)*inv) + (splat_control.a *(tex2D (_Splat3, IN.uv_Splat3 * -_MixScale).rgb)*_Mix.a);	
	o.Normal += splat_control.a * UnpackNormal(tex2D(_BumpMap3, IN.uv_Splat3));
	o.Gloss += splat.a * splat_control.a;
	o.Specular += _SpecularScale * splat_control.a;

    o.Gloss *= _SpecularScale;
	o.Albedo = col;
	o.Alpha = 1.0;
}
ENDCG  
}

// Fallback to Diffuse
Fallback "Diffuse"
}