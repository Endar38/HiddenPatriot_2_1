Shader "Custom/player_Shader"

{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth("Outline Width", Range(0.0, 0.1)) = 0.005
        _OutlineEnabled("Outline Enabled", Range(0, 1)) = 0
    }
        SubShader
        {
            Tags { "Queue" = "Overlay" }
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float2 uv_outline : TEXCOORD1; // Tambahkan texcoord untuk outline
                };

                sampler2D _MainTex;
                fixed4 _OutlineColor;
                float _OutlineWidth;
                float _OutlineEnabled; // Properti untuk mengontrol apakah outline aktif

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;

                    // Hitung texcoord untuk garis tepi dengan menambahkan offset ke arah semua arah
                    float2 outlineOffset = _OutlineWidth * 2.0 * (0.5 - abs(v.texcoord - 0.5));
                    o.uv_outline = v.texcoord + outlineOffset;

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Jika outline tidak aktif, kembalikan warna sprite tanpa perubahan
                    if (_OutlineEnabled == 0)
                    {
                        return tex2D(_MainTex, i.uv);
                    }

                // Jika outline aktif, lakukan perhitungan untuk garis tepi
                float4 texColor = tex2D(_MainTex, i.uv);
                float4 outlineColor = _OutlineColor;

                // Hitung alpha untuk garis tepi
                float alpha_outline = tex2D(_MainTex, i.uv_outline).a;

                // Tetapkan warna dari garis tepi
                fixed4 outline = outlineColor;
                outline.a *= step(0.5, alpha_outline); // Garis tepi tetap solid, tidak transparan

                // Kombinasikan warna sprite dan garis tepi
                fixed4 col = lerp(outline, texColor, texColor.a);

                return col;
            }
            ENDCG
        }
        }
}


