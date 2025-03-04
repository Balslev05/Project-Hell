Shader "Custom/FixedWhiteScaleShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlendFactor("White Scale Intensity", Range(0,1)) = 0.3
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        ZWrite On
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

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

            sampler2D _MainTex;
            float _BlendFactor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float grey = dot(col.rgb, float3(0.299, 0.587, 0.114)); 

                float blendFactor = max(_BlendFactor, 0.001); 
                float whiteScale = lerp(grey, 1.0, blendFactor);

                return fixed4(whiteScale, whiteScale, whiteScale, max(col.a, 0.1)); // Minimum alpha fix
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}