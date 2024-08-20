Shader "Custom/LineRendererAlwaysOnTop"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1) // Variable para color
        _MainTex ("Texture", 2D) = "white" {} // Textura opcional
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            ZTest Always         // Siempre pasar la prueba de profundidad
            ZWrite Off           // No escribir en el buffer de profundidad
            Cull Off             // Renderizar ambas caras
            Blend SrcAlpha OneMinusSrcAlpha  // Mezcla estándar de alfa

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            fixed4 _Color; // Variable para color

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}
