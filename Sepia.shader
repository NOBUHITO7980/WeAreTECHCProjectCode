Shader "Custom/Sepia"
{
	Properties
	{
		_Darkness("Dark", Range(0, 0.1)) = 0.04
		_Strength("Strength", Range(0.05, 0.15)) = 0.05
		_RedStrength("RedStrength", Range(0.0, 1.0)) = 0.3
		_GreenStrength("GreenStrength", Range(0.0, 1.0)) = 0.6
		_BlueStrength("BlueStrength", Range(0.0, 1.0)) = 0.1
		_AlphaStrength("AlphaStrength", Range(0.0, 1.0)) = 1
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

		//ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			float _RedStrength;
			float _GreenStrength;
			float _BlueStrength;

			float _AlphaStrength;

			half _Darkness;
			half _Strength;

			fixed4 _Color;


			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				half gray = col.r * _RedStrength + col.g * _GreenStrength + col.b * _BlueStrength - _Darkness;

				gray = gray < 0 ? 0 : gray;

				half R = gray + _Strength;
				half B = gray - _Strength;

				R = (R > 1.0) ? 1.0 : R;
				B = (B < 0) ? 0 : B;
				col.rgb = fixed3(R, gray, B);

				col.a = _AlphaStrength;

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
				ENDCG
		}
	}
}
