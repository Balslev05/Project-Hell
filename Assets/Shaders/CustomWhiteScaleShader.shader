Shader "Custom/FullControlShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Exposure("Exposure", Range(0, 3)) = 1.2
        _Contrast("Contrast", Range(0, 3)) = 1.5
        _Saturation("Saturation", Range(0, 2)) = 1.0
        _Tint("Tint Color", Color) = (1,1,1,1)
        _HighlightStrength("Highlight Strength", Range(0, 2)) = 1.0
        _ShadowStrength("Shadow Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
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
            float _Exposure;
            float _Contrast;
            float _Saturation;
            float4 _Tint;
            float _HighlightStrength;
            float _ShadowStrength;

            // Function to apply saturation
            float3 AdjustSaturation(float3 color, float saturation)
            {
                float gray = dot(color, float3(0.299, 0.587, 0.114));
                return lerp(float3(gray, gray, gray), color, saturation);
            }

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
                float3 color = col.rgb;

                // Apply exposure (brightens or darkens)
                color *= _Exposure;

                // Apply contrast (increase depth)
                color = (color - 0.5) * _Contrast + 0.5;

                // Apply saturation (control color intensity)
                color = AdjustSaturation(color, _Saturation);

                // Apply tint (shift colors towards a different hue)
                color *= _Tint.rgb;

                // Apply highlights and shadows
                float brightness = dot(color, float3(0.299, 0.587, 0.114));
                float shadowEffect = smoothstep(0.0, 1.0, brightness) * _ShadowStrength;
                float highlightEffect = (1.0 - smoothstep(0.0, 1.0, brightness)) * _HighlightStrength;
                color = color + highlightEffect - shadowEffect;

                // Ensure colors are clamped
                color = saturate(color);

                return fixed4(color, col.a);
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}
