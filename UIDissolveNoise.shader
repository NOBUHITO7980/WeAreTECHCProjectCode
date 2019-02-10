Shader "Unlit/UIDissolveNoise"
{
	Properties
	{
		_BurnColor("BurnColor", Color) = (1,1,1,1)
		_AshColor("AshColor", Color) = (1,1,1,1)

		_MainColor("AshColor", Color) = (1,1,1,1)

		_MainTex("Texture", 2D) = "white" {}


		_Threshold("Threshold", Range(0,1)) = 0.0


		_Fvalue5("Float Value05", float) = 127.1
		_Fvalue6("Float Value06", float) = 311.7
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _BurnTex;

			float4 _MainTex_ST;

			fixed4 _AshColor;
			fixed4 _BurnColor;
			fixed4 _MainColor;

			half _Threshold;
			half _Gradation;

			half _Fvalue5;
			half _Fvalue6;

			fixed2 random2(fixed2 st)
			{
				st = fixed2(dot(st, fixed2(_Fvalue5, _Fvalue6)), dot(st, fixed2(269.5, 183.3)));

				return -1.0 + 2.0 * frac(sin(st) * 43758.543123);
			}

			float perlinNoise(fixed2 st)
			{
				fixed2 p = floor(st);
				fixed2 f = frac(st);
				fixed2 u = f * f * (3.0 - 2.0 * f);

				float v00 = random2(p + fixed2(0, 0));
				float v10 = random2(p + fixed2(1, 0));
				float v01 = random2(p + fixed2(0, 1));
				float v11 = random2(p + fixed2(1, 1));

				return lerp(lerp(dot(v00, f - fixed2(0, 0)), dot(v10, f - fixed2(1, 0)), u.x), lerp(dot(v01, f - fixed2(0, 1)), dot(v11, f - fixed2(1, 1)), u.x), u.y) + 0.5f;

			}

			float fBm(fixed2 st)
			{
				float f = 0;
				fixed2 q = st;

				f += 0.500  * perlinNoise(q); q = q * 2.01;	//0.500
				f += 0.2500 * perlinNoise(q); q = q * 2.02;		//0.2500
				f += 0.1250 * perlinNoise(q); q = q * 2.03;		//0.1250
				f += 0.0625 * perlinNoise(q); q = q * 21.01;		//0.0625

				return f;
			}

			v2f vert(appdata v)
			{

				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half4 col;
				half c = fBm(i.uv * 10);
				fixed4 m = fixed4(c, c, c, 1);

				half g = m.r * 0.25 + m.g * 0.45 + m.b * 0.1;
				half s = m.r * 0.35 + m.g * 0.55 + m.b * 0.1;

				if (g < _Threshold && s > _Threshold)
				{
					col.rgb = _BurnColor;
					col.rgb *= fixed4(lerp(1, 0, 1 - saturate(_Threshold / 0.3)), 0, 0, 1);
					col.rgb *= c;
					return col;
				}
				if (g < _Threshold) { discard; }

				col = tex2D(_MainTex, i.uv) * _MainColor;
			return col;
		}
		ENDCG
	}
	}
}
