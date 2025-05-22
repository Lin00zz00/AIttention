Shader "Custom/WireframeNoGS"
{
    Properties
    {
        _WireColor ("Wire Color", Color) = (1, 1, 1, 1)
        _WireThickness ("Wire Thickness", Range(0, 1)) = 0.1
        _EmissionIntensity ("Emission Intensity", Range(0, 10)) = 1
        [Toggle] _UseEmission ("Use Emission", Float) = 1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 100
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            
            Cull Back
            ZWrite On
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
                float3 barycentric : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 barycentric : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            CBUFFER_START(UnityPerMaterial)
                float4 _WireColor;
                float _WireThickness;
                float _EmissionIntensity;
                float _UseEmission;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.barycentric = input.barycentric;
                
                return output;
            }
            
            float4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                
                float3 barys = input.barycentric;
                float3 deltas = fwidth(barys);
                float3 smoothing = deltas * _WireThickness;
                float3 thickness = smoothstep(0, smoothing, barys);
                float minThickness = min(thickness.x, min(thickness.y, thickness.z));
                
                float4 finalColor = float4(0, 0, 0, 1);
                if (minThickness < 1.0)
                {
                    finalColor = _WireColor;
                    
                    if (_UseEmission > 0.5)
                    {
                        finalColor.rgb *= _EmissionIntensity;
                    }
                }
                else
                {
                    discard;
                }
                
                return finalColor;
            }
            ENDHLSL
        }
    }
}