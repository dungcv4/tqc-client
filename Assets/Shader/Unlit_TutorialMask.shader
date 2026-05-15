// Source: 1-1 auto-port from extracted GLSL bytecode
//   Extracted: tools/extract_all_shaders.py → work/06_ghidra/shader_dumps/Unlit_TutorialMask/
//   Reference GLSL: work/06_ghidra/shader_dumps/Unlit_TutorialMask/GLES20_sub0.glsl
//   Fragment template: tex_rgba (transcribed op-for-op from GLSL main())
//   Vertex: standard hlslcc MVP chain + uv*_MainTex_ST
//   Render state taken verbatim from SerializedShader m_State (meta.json).
Shader "Unlit/TutorialMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "true" }
        LOD 100

        Pass
        {
            Blend SrcAlpha SrcAlphaSaturate
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color  : COLOR;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                fixed4 color  : COLOR0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = saturate(v.color);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
