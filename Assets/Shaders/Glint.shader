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
		Tags { "RenderType"="Transparent" "Queue"="Transparent" } // Transparent queue
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha // Normal alpha blending

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

				// X pixel for current fragment.
				float xPx = i.uv.x * _PxPerUnit;

				// Current left edge for the shine
				float xCur = ((_Time.y * _Speed) % 1) * ((_PxPerUnit + _ShinePxWidth * 2)) - _ShinePxWidth;

				// If between the left and right edges of shine, tint by _ShineTint, otherwise tint by _NShineTint.
				// The ?: operator actaully saves time here, because of how gpu's work.
				col *= (xPx > xCur&& xPx < xCur + _ShinePxWidth) ? _ShineTint : _NShineTint;

                return col;
            }
            ENDCG
        }
    }
}
