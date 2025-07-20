Shader "Custom/CustomOutline" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Outline ("Outline Color", Color) = (0, 0, 0, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Size ("Outline Thickness", Float) = 1.5
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        // Outline Pass
        Pass {
            Stencil {
                Ref 1
                Comp NotEqual
            }
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            half _Size;
            fixed4 _Outline;
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert (appdata_base v) {
                v2f o;
                v.vertex.xyz += v.normal * _Size;
                o.pos = UnityObjectToClipPos (v.vertex);
                return o;
            }
            half4 frag (v2f i) : SV_Target
            {
                return _Outline;
            }
            ENDCG
        }

        // Main Unlit Pass
        Pass {
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }
            Tags { "LightMode" = "Always" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Texture"
}