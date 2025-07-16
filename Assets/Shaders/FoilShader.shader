Shader "Unlit/HolofoilShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HolofoilTex("Holofoil Texture", 2D) = "white" {}
        _Scale("Scale", Float) = 1
        _Intensity("Foil Intensity", Float) = 1
        _Color1("Color One", color) = (1, 1, 1, 1)
        _Color2("Color Two", color) = (1, 1, 1, 1)
        _Color3("Color Three", color) = (1, 1, 1, 1)
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
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex, _HolofoilTex;
            float4 _MainTex_ST;
            float _Scale, _Intensity;
            float4 _Color1, _Color2, _Color3;

            float3 Plasma(float2 uv)
            {
                uv = uv * _Scale - _Scale / 2;
                float time = 0;
                float wave1 = sin(uv.x + time);
                float wave2 = sin(uv.y + time) * 0.5;
                float wave3 = sin(uv.x + uv.y + time);

                float r = sin(sqrt(uv.x * uv.x + uv.y * uv.y) + time) * 2;

                float finalValue = wave1 + wave2 + wave3 + r;

                float3 c1 = sin(finalValue * UNITY_PI) * _Color1;
                float3 c2 = cos(finalValue * UNITY_PI) * _Color2;
                float3 c3 = sin(finalValue) * _Color3;

                return c1 + c2 + c3;
            }


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewDir = WorldSpaceViewDir(v.vertex); //remember this
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

                fixed4 foil = tex2D(_HolofoilTex, i.uv);
                float2 newUV = i.viewDir.xy + foil.rg; //this too
                float3 plasma = Plasma(newUV) * _Intensity;
                fixed4 col = tex2D(_MainTex, i.uv);
                return fixed4(col.rgb + col.rgb * plasma.rgb, 1);
            }
            ENDCG
        }
    }
}
