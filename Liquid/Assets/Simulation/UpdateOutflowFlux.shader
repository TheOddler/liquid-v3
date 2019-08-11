Shader "Hidden/UpdateOutflowFlux"
{
	Properties
	{
		_MainTex("Flux RLBT", 2D) = "white" {} //main assumed to be the current outflow flux field
		_WaterSandRockSedimentTex("Water, Sand, Rock, Sediment heights", 2D) = "white" {}

		_DT("Delta Time", Float) = 0.2
		_L("Pipe Length", Float) = 0.2
		_A("Pipe Cross Section Area", Float) = 1.0
		_G("Gravity Constant", Float) = 9.81

		_Damping("Damping level per tick, 1 means no damping, 0 everything", Float) = 0.99
		_FluxDeltaLimit("Limit how much flux can change per step", Float) = 0.1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D_float _MainTex;
			sampler2D_float _WaterSandRockSedimentTex;
			float4 _WaterSandRockSedimentTex_TexelSize;

			float _DT;
			float _L;
			float _A;
			float _G;

			float _Damping;
			float _FluxDeltaLimit;

			float4 frag(v2f_img i) : SV_Target
			{
				float4 flux = tex2D(_MainTex, i.uv);
				float4 heights = tex2D(_WaterSandRockSedimentTex, i.uv);

				float4 hR = tex2D(_WaterSandRockSedimentTex, i.uv + fixed2(_WaterSandRockSedimentTex_TexelSize.x, 0));
				float4 hL = tex2D(_WaterSandRockSedimentTex, i.uv - fixed2(_WaterSandRockSedimentTex_TexelSize.x, 0));
				float4 hB = tex2D(_WaterSandRockSedimentTex, i.uv + fixed2(0, _WaterSandRockSedimentTex_TexelSize.y));
				float4 hT = tex2D(_WaterSandRockSedimentTex, i.uv - fixed2(0, _WaterSandRockSedimentTex_TexelSize.y));

				//float otherHeight = heights.g + heights.b; //only sand and rock
				//float totalHeight = heights.r + heights.g + heights.b; //with water
				//float4 totalHeight4 = float4(totalHeight, totalHeight, totalHeight, totalHeight);
				float4 totalHeight4 = heights.r + heights.g + heights.b + heights.a;
				float4 totalH_RLBT = float4(
					hR.r + hR.g + hR.b + hR.a,
					hL.r + hL.g + hL.b + hL.a,
					hB.r + hB.g + hB.b + hB.a,
					hT.r + hT.g + hT.b + hT.a);

				// Formula 3
				float4 dh = totalHeight4 - totalH_RLBT;

				// Formula 2
				float4 fluxDelta = _DT * _A * _G * dh / _L;
				//fluxDelta = clamp(fluxDelta, -_FluxDeltaLimit, _FluxDeltaLimit);
				//fluxDelta = _FluxDeltaLimit * fluxDelta / (1 + abs(fluxDelta)); // Smooth clamping function
				fluxDelta = _FluxDeltaLimit * tanh(fluxDelta / _FluxDeltaLimit); // Smooth clamping function
				//fluxDelta = _FluxDeltaLimit * fluxDelta / sqrt(1 + fluxDelta * fluxDelta); // Smooth clamping function
				float4 fn = max(0, flux + fluxDelta) * _Damping; // fn = flux next;

				// Formula 4
				float K = clamp((heights.r * _L * _L) / ((fn.r + fn.g + fn.b + fn.a) * _DT), 0, 1);

				// Formula 5
				return K * fn;
			}
			ENDCG
		}
	}
}
