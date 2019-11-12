Shader "Unlit/Lava"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Speed ("Wave speed", Float) = 1
		_Col1("Color 1", Color) = (1,1,1,1)
		_Col2("Color 2", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
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
				float4 col : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 col : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST, _Col1, _Col2;
			float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.col = v.col;
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;

				float stime = _Time.y * _Speed;

				uv.y = (i.uv.y + floor(stime * 16) / 16) % 1;

				uv.x += ((stime * 2) % 1 > 0.5 ? uv.y > 0.5 : uv.y < 0.5) ? 1.0 / 16 : 0;

				float t = saturate(round((sin(_Time.y * 3.1415) + 1)) / 2);

                fixed4 col = tex2D(_MainTex, uv) * lerp(_Col1, _Col2, t);
                return col;
            }
            ENDCG
        }
    }
}
