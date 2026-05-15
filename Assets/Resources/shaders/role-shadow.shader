// Source: 1-1 port from extracted GLSL bytecode
//   Bundle: work/01_apk_extracted/assets/BundleData/model/shadow1.uab
//   Shader path_id: 2246466228478251824 (Sango Mobile/Role Shadow)
//   GpuProgramID 25818, Platforms [GLES20=5, GLES3=9, Vulkan=18]
//   Extracted via tools/decompress_shader_blob.py (LZ4 decompress of compressedBlob)
//   Saved at work/06_ghidra/shader_dumps/role_shadow_GLES20_sub0.txt
//
// Original GLES20 fragment shader (1-1 from binary):
//   u_xlat10_0 = texture2D(_MainTex, vs_TEXCOORD0.xy);
//   u_xlat16_0 = u_xlat10_0 * vs_COLOR0;
//   u_xlatb1 = 0.00999999978 >= u_xlat16_0.w;
//   SV_Target0 = u_xlat16_0;
//   if(u_xlatb1){discard;}
//
// Render state (from m_ParsedForm SerializedShader):
//   gpuProgramID=25818, Blend Zero SrcColor (srcBlend=0=Zero, destBlend=3=SrcColor)
//   ZWrite Off (zWrite=0), Cull Off (culling=0), ZTest LEqual (zTest=4)
//   Queue Geometry+1, RenderType Transparent
Shader "Sango Mobile/Role Shadow"
{
    Properties
    {
        _MainTex ("Particle Texture", 2D) = "white" {}
        _TintColor ("Tint Color (unused by shader)", Color) = (0.5, 0.5, 0.5, 0.5)
    }

    SubShader
    {
        LOD 100
        Tags { "IgnoreProjector" = "True" "LightMode" = "Always" "Queue" = "Geometry+1" "RenderType" = "Transparent" }

        Pass
        {
            LOD 100
            Tags { "IgnoreProjector" = "True" "LightMode" = "Always" "Queue" = "Geometry+1" "RenderType" = "Transparent" }
            Blend Zero SrcColor
            ZWrite Off
            Cull Off
            Fog { Mode Off }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color  : COLOR;       // mesh vertex color → vs_COLOR0
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
                // 1-1: ObjectToWorld → MatrixVP, matches GLSL hlslcc_mtx4x4unity_*  chain
                o.vertex = UnityObjectToClipPos(v.vertex);
                // 1-1: vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // 1-1: vs_COLOR0 = clamp(in_COLOR0, 0.0, 1.0)
                o.color = saturate(v.color);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 1-1 from extracted GLSL fragment:
                //   u_xlat10_0 = texture2D(_MainTex, vs_TEXCOORD0.xy);
                //   u_xlat16_0 = u_xlat10_0 * vs_COLOR0;
                //   if (u_xlat16_0.w <= 0.00999999978) discard;
                //   return u_xlat16_0;
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                clip(col.a - 0.00999999978);
                return col;
            }
            ENDCG
        }
    }
}
