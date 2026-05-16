// Render-state 1-1 from APK uifx.uab Shader 'Particles/01_AddMultiply' m_ParsedForm:
//   Tags Queue=Transparent RenderType=Transparent IgnoreProjector=true;
//   Blend One OneMinusSrcAlpha (src=1 dst=10); ZWrite Off; ZTest LEqual; Cull Off; ColorMask RGBA.
// Vert/frag 1-1 from extracted GLES GLSL (binary-verify-decompile skill Phase B):
//   work/06_ghidra/shader_dumps/AddMultiply_andr_sub0.txt (Android uifx.uab, GLES20).
//   frag: rgb = saturate(tex(_MainTex).rgb * _TintColor.rgb * vColor.rgb * tex(_AlphaTex).r); rgb += rgb (×2)
//         a   = saturate(tex(_AlphaTex).r * vColor.a * _TintColor.a)
//   (Earlier reconstruction added a wrong `rgb *= a` premultiply → dark/black ring;
//    the real shader does NOT premultiply. Now 1-1 from the extracted shader program.)
// Was an AssetRipper //DummyShaderTextExporter stub (Opaque, no Blend) → opaque-black quad.
Shader "Particles/01_AddMultiply" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Mask (R)", 2D) = "white" {}
		[Toggle(HEIGHTMASK)] _EnableHeightMask ("Enable Height Mask", Float) = 0
		_HeightMask ("Height Mask Min(X) Max (Y)", Vector) = (0,0,0,0)
	}

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200

		Blend One OneMinusSrcAlpha   // m_ParsedForm rtBlend0: src=1 (One) dst=10 (OneMinusSrcAlpha)
		ZWrite Off                   // m_ParsedForm zWrite=0
		ZTest LEqual                 // m_ParsedForm zTest=4
		Cull Off                     // m_ParsedForm culling=0
		ColorMask RGBA
		Lighting Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			fixed4 _TintColor;
			float4 _MainTex_ST;

			struct appdata_t {
				float4 vertex   : POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			// 1-1 from GLES vertex: OB→W→VP (UnityObjectToClipPos), TRANSFORM_TEX, pass vertex color.
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			// 1-1 from extracted GLES fragment (AddMultiply_andr_sub0.txt).
			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 c = tex2D(_MainTex, i.texcoord).xyz;   // u_xlat10_0.xyz = tex(_MainTex).xyz
				c = c * _TintColor.xyz;                        // * _TintColor.xyz
				c = c * i.color.xyz;                           // * vs_COLOR0.xyz
				fixed mask = tex2D(_AlphaTex, i.texcoord).x;   // u_xlat10_0.x = tex(_AlphaTex).x
				c = mask.xxx * c;                              // rgb *= mask
				c = clamp(c, 0.0, 1.0);                         // clamp(...,0,1)
				fixed3 outRgb = c + c;                          // SV_Target.xyz = c + c  (×2)
				fixed av = i.color.w * _TintColor.w;            // vs_COLOR0.w * _TintColor.w
				fixed outA = clamp(mask * av, 0.0, 1.0);        // SV_Target.w = mask * av, clamped
				return fixed4(outRgb, outA);
			}
			ENDCG
		}
	}
	Fallback Off
}
