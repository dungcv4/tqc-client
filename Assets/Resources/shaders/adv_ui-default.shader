// Reconstruction fidelity:
//  - Properties + SubShader Tags + Stencil/Blend/ZWrite/ZTest/Cull/ColorMask render-state:
//    1-1 from APK Shader asset m_ParsedForm
//    (work/01_apk_extracted/assets/bin/Data/6728e65b108560a43bdd17d477e4d927, name "ADV/UI/Default").
//    The earlier file here was an AssetRipper "DummyShaderTextExporter" stub (Opaque, no Stencil/
//    Blend) — that is why UI Mask never clipped (skill/item quick-slot bars showed all pages).
//  - vert/frag program: GPU bytecode (GpuProgramID 12301) is not statically recoverable.
//    Reconstructed from Unity's canonical built-in UI-Default shader (engine-standard; AdvImage
//    is just Image+effect, so the only game-specific bit is the `_EffectAmount` desaturation).
//    Grayscale = standard Rec.601 luminance lerp by _EffectAmount.
Shader "ADV/UI/Default"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)

		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		_EffectAmount ("Effect Amount", Range(0, 1)) = 0

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
			Name "Default"
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
			#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;
			float4 _MainTex_ST;
			float _EffectAmount;

			v2f vert(appdata_t v)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.worldPosition = v.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

				OUT.color = v.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

				#ifdef UNITY_UI_CLIP_RECT
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				// Game-specific effect (frag bytecode unrecoverable): Rec.601 luminance
				// desaturation by _EffectAmount. AdvImage drives this via GrayScaleAmount.
				fixed gray = dot(color.rgb, fixed3(0.299, 0.587, 0.114));
				color.rgb = lerp(color.rgb, fixed3(gray, gray, gray), saturate(_EffectAmount));

				return color;
			}
		ENDCG
		}
	}
}
