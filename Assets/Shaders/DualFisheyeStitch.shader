Shader "Custom/DualFisheyeStitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Front

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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 finalUV;

                if (uv.x > 0.5)
                {
                    // Right side = rear lens, bottom half of texture
                    float u = (uv.x - 0.5) * 2.0;
                    float v = uv.y;
                    finalUV = float2(u, v * 0.5);
                }
                else
                {
                    // Left side = front lens, top half of texture
                    float u = uv.x * 2.0;
                    float v = uv.y;
                    finalUV = float2(u, 0.5 + v * 0.5);
                }

                return tex2D(_MainTex, finalUV);
            }
            ENDCG
        }
    }
}