Shader "Unlit/Glint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_PxPerUnit("Pixels per unit (should be 16)", Int) = 16
		_ShinePxWidth("Shine pixel width", Int) = 2
		_ShineTint("Shine tint", Color) = (1,1,1,1)
		_NShineTint("Tint outside shine", Color) = (1,1,1,1)
		_Speed("Shine speed (px per second)", Float) = 2
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			int _PxPerUnit, _ShinePxWidth;
			float _Speed;
			float4 _ShineTint, _NShineTint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

				float xPx = i.uv.x * _PxPerUnit;
				float xCur = (_Time.y % (_PxPerUnit / _Speed)) * _Speed * ((1.0 + _ShinePxWidth * 2)) - _ShinePxWidth;

				col *= (xPx > xCur&& xPx < xCur + _ShinePxWidth) ? _ShineTint : _NShineTint;

                return col;
            }
            ENDCG
        }
    }
}
