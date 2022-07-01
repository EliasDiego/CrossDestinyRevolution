Shader "Unlit/BoundsShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Test ("Test", Vector) = (0, 0, 0, 0)
        _MaxDistance ("Max Distance", float) = 15
        _TransparentColor ("Transparent Color", Color) = (0, 0, 0, 0)
        _OpaqueColor ("Opaque Color", Color) = (1, 1, 1, 1)
         
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Test;
            float _MaxDistance;
            float4 _TransparentColor;
            float4 _OpaqueColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 d = distance(i.vertex, UnityWorldToClipPos(_Test.xyz));
                float normalizedDistance = min(d, _MaxDistance) / _MaxDistance;
                
                

                // fixed4 col = tex2D(_MainTex, i.uv);
                float4 col = lerp(_TransparentColor, _OpaqueColor, normalizedDistance);
                // // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
