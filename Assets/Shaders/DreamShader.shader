Shader "Custom/GrainCelUnlit"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.2,0.3,0.4,1)
        _LightDirection ("Light Direction", Vector) = (-0.5,0.8,1)
        _GrainStrength ("Grain Strength", Float) = 0.07
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _BaseColor;
            float4 _LightDirection;
            float _GrainStrength;

            // Random number generator
            float random(float2 st)
            {
                return frac(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            float3 celShade(float3 baseColor, float3 lightDir, float3 normal)
            {
                float diff = max(dot(lightDir, normal), 0.0);
                float levels = 3.0;
                diff = floor(diff * levels) / (levels - 1.0);
                return baseColor * diff;
            }

            float3 fakeNormalFromUV(float2 uv)
            {
                return normalize(float3(uv - 0.5, 1.0));
            }

            float grainNoise(float2 uv, float time)
            {
                float scale = 300.0;
                return random(uv * scale + time * 0.5);
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float3 lightDir = normalize(_LightDirection.xyz);
                float3 normal = fakeNormalFromUV(IN.uv);
                float3 baseColor = _BaseColor.rgb;

                float3 shaded = celShade(baseColor, lightDir, normal);

                float grain = grainNoise(IN.uv, _Time.y); // Use built-in _Time.y
                shaded += (grain - 0.5) * _GrainStrength;

                return float4(shaded, 1.0);
            }

            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
