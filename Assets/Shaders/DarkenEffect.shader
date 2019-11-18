// This whole shader is basically the default unity shader, except for the commented parts.

Shader "ImageEffect/DarkenEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Transition("Transition", Range(0, 1)) = 0 // Transition property declaration
    }
    SubShader
    {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 100

		// Post-multiplied subtractive blending, basically finalColor = screenColor - (myColor * myAlpha)
		Blend SrcAlpha One
		BlendOp RevSub

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			float _Transition; // Need to declare the property here also, so unity can properly compile the shader.

            fixed4 frag (v2f i) : SV_Target
            {
				// Round transition to increments of 0.25 (0, 0.25, 0.5, 0.75, 1)
                return float4(1, 1, 1, round(_Transition * 4) / 4);
            }
            ENDCG
        }
    }
}
