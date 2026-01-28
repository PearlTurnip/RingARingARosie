Shader "Sprites/Outline2D"
{
    Properties {
        _MainTex ("Sprite", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", Float) = 1
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            float4 _OutlineColor;
            float _OutlineSize;
            float4 _MainTex_ST;

            // Vertex shader. Just kept this
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            // Black magic
            fixed4 frag(v2f i) : SV_Target {
                // Getting color from 2d texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Returning if invisible (useless calculation otherwise)
                if (col.a > 0) return col;

                // Normalizing vector for outline
                float2 offset = _OutlineSize / _ScreenParams.xy;

                // Getting the alpha difference between pixels. If its a great difference then use as outline
                if (
                    tex2D(_MainTex, i.uv + float2(offset.x,0)).a > 0 ||
                    tex2D(_MainTex, i.uv - float2(offset.x,0)).a > 0 ||
                    tex2D(_MainTex, i.uv + float2(0,offset.y)).a > 0 ||
                    tex2D(_MainTex, i.uv - float2(0,offset.y)).a > 0
                ) return _OutlineColor * float4(1,1,1,(_CosTime.a + 1) / 2); // Outline

                return col; // Otherwise return normal color
            }
            ENDCG
        }
    }
}
