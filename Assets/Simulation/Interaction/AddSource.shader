Shader "Hidden/AddSource"
{
	Properties
	{
		_MainTex ("Brush", 2D) = "white" {}
		_Scale("Scaling factor, include delta time if wanted here", Vector) = (1, 1, 1, 1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			Blend One One

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _Scale;

			float4 frag(v2f_img i) : SV_Target
			{
				return tex2D(_MainTex, i.uv) * _Scale;
			}
			ENDCG
		}
	}
}
