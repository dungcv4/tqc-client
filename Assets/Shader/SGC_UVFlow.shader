Shader "SGC_UVFlow" {
	Properties {
		[Header(Base______________________________________)] _TintColor ("Tint_Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		_Alpha_Tex ("Alpha_Tex(B)", 2D) = "white" {}
		[MaterialToggle] _UseAlphaChannel ("Use A Channel", Float) = 0
		[Header(UV Flow______________________________________)] [MaterialToggle] _Alpha_Flow ("Alpha_Flow", Float) = 1
		_Flow_Tex ("Flow_Tex(RG)", 2D) = "white" {}
		_Flow_Speed ("Flow_Speed", Float) = 1
		_Flow_Intensity ("Flow_Intensity", Float) = 1
		[MaterialToggle] [HideInInspector] _AFlow_1 ("AFlow_1", Float) = 0
		[MaterialToggle] [HideInInspector] _AFlow_2 ("AFlow_2", Float) = 0
		[Header(UV Disturbance______________________________________)] _DisTex ("Disturbance Tex", 2D) = "black" {}
		_DisIntensity ("Intensity", Range(-1, 1)) = 0
		[Header(Shader Setting______________________________________)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend Mode", Float) = 10
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _MainTex_ST;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, input.uv.xy);
			}

			ENDHLSL
		}
	}
}