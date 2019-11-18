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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST, _Col1, _Col2;
			float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;

				// Scaled time
				float stime = _Time.y * _Speed;

				// THE OBJECT IS F---ING ROTATED 90 DEGREES
				// So i needed to swap the y and x coords of the uv
				
				// Scroll the sprite horizontally in pixel increments.
				// modulo 1 to wrap the uv around.
				uv.y = (i.uv.y + floor(stime * 16) / 16) % 1;

				// Displace half of the texture 1 px up, and swap the half every (stime) seconds
				uv.x += ((stime * 2) % 1 > 0.5 ? uv.y > 0.5 : uv.y < 0.5) ? 1.0 / 16 : 0;

				// Smooth between colors, in increments of 0.5 ( _Col1, (_Col1 + _Col2) / 2, _Col2 )
				float t = saturate(round((sin(_Time.y * 3.1415) + 1)) / 2);

				// Final composite
                fixed4 col = tex2D(_MainTex, uv) * lerp(_Col1, _Col2, t);
                return col;
            }
            ENDCG
        }
    }
}
