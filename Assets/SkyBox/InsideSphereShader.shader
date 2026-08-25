Shader "Custom/InsideSphereShader"
{
    Properties
    {
        _MainTex ("360 Texture", 2D) = "white" {}
        _Rotation ("Rotation", Range(0, 360)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background" }
        Cull Front
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Rotation;

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

            float2 RotateUV(float2 uv, float rotation)
            {
                uv.x = frac(uv.x + rotation / 360.0);
                return uv;
            }

            v2f vert (appdata v)
            {
                v2f o;

                // Flip horizontally so panorama appears correctly from inside
                o.uv = float2(1.0 - v.uv.x, v.uv.y);
                o.uv = RotateUV(o.uv, _Rotation);

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}