// Source: 1-1 port from extracted GLSL bytecode (NOT AssetRipper dummy).
//   Bundle scan: tools/extract_all_shaders.py
//   Reference GLSL: work/06_ghidra/shader_dumps/Sango_Online_Classic_Role_ETC1/GLES20_sub0.glsl
//   gpuProgramID 14459, Platforms [GLES20, GLES3, Vulkan]
//
// Original GLES20 fragment (1-1, op-for-op):
//   u_xlat10_0.x = texture2D(_AlphaTex, uv).x;
//   u_xlat0      = (-u_xlat10_0.x) + 1.0;          // 1 - alphaTex.r
//   u_xlat16_1   = u_xlat0 + (-_Cutoff);
//   if (u_xlat16_1 < 0.0) discard;                 // alpha cutout
//   u_xlat16_1   = ((-_LightColor.w) + 1.0) * 0.3; // t = (1-_LightColor.a)*0.3
//   u_xlat10_0   = texture2D(_MainTex, uv);
//   u_xlat2      = u_xlat10_0 * vs_COLOR0;                       // base
//   u_xlat16_0   = (-u_xlat10_0) * vs_COLOR0 + _LightColor;      // _LightColor - c*vColor
//   SV_Target0   = vec4(u_xlat16_1) * u_xlat16_0 + u_xlat2;      // t*lit + base
// Original vertex (1-1): standard MVP chain; vs_TEXCOORD0 = uv*_MainTex_ST;
//   vs_COLOR0 = in_COLOR0 * _Color  (vertex color modulated by material _Color)
//
// Render state from SerializedShader m_State (meta.json):
//   ZWrite On, ZTest LEqual, Cull Back, Blend One Zero (opaque),
//   Queue Geometry-2, RenderType TransparentCutout, IgnoreProjector, LightMode Always
Shader "Sango Online Classic/Role ETC1"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _AlphaTex ("Transmissive Colour (A)", 2D) = "black" {}
        _Cutoff ("_Cutoff", Range(0, 1)) = 0.5
        _FogDensity ("Fog Density [0~1]", Float) = 0.04
    }

    SubShader
    {
        Tags { "IgnoreProjector" = "True" "LightMode" = "Always" "Queue" = "Geometry-2" "RenderType" = "TransparentCutout" }
        LOD 200

        Pass
        {
            ZWrite On
            ZTest LEqual
            Cull Back
            Blend One Zero

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _AlphaTex;
            fixed4 _Color;
            fixed4 _LightColor;
            float _Cutoff;

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
                o.color = v.color * _Color;            // 1-1: vs_COLOR0 = in_COLOR0 * _Color
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 1-1 op-for-op from extracted GLSL fragment:
                fixed alpha = tex2D(_AlphaTex, i.uv).r;
                clip((1.0 - alpha) - _Cutoff);                       // discard if (1-alpha) < _Cutoff
                fixed t = (1.0 - _LightColor.w) * 0.300000012;
                fixed4 c = tex2D(_MainTex, i.uv);
                fixed4 base = c * i.color;
                fixed4 lit  = (-c) * i.color + _LightColor;          // = _LightColor - c*vColor
                return fixed4(t, t, t, t) * lit + base;
            }
            ENDCG
        }
    }
}
