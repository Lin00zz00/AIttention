Shader "Custom/d"
{
    Properties
    {
        _PointSize("Point Size", Range(0.001, 0.1)) = 0.01
        _Color("Color", Color) = (1,1,1,1)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct v2g
            {
                float4 pos : SV_POSITION;
            };
            
            struct g2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            float _PointSize;
            fixed4 _Color;
            
            v2g vert(appdata_base v)
            {
                v2g o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            [maxvertexcount(4)]
            void geom(point v2g IN[1], inout TriangleStream<g2f> triStream)
            {
                g2f o;
                
                // 创建一个小四边形代表点
                float4 pos = IN[0].pos;
                float size = _PointSize;
                
                o.pos = pos + float4(-size, -size, 0, 0);
                o.uv = float2(0, 0);
                triStream.Append(o);
                
                o.pos = pos + float4(size, -size, 0, 0);
                o.uv = float2(1, 0);
                triStream.Append(o);
                
                o.pos = pos + float4(-size, size, 0, 0);
                o.uv = float2(0, 1);
                triStream.Append(o);
                
                o.pos = pos + float4(size, size, 0, 0);
                o.uv = float2(1, 1);
                triStream.Append(o);
                
                triStream.RestartStrip();
            }
            
            fixed4 frag(g2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}