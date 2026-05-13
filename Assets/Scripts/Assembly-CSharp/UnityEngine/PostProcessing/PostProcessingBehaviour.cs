using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	[ImageEffectAllowedInSceneView]
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	[AddComponentMenu("Effects/Post-Processing Behaviour", -1)]
	[DisallowMultipleComponent]
	public class PostProcessingBehaviour : MonoBehaviour
	{
		public PostProcessingProfile profile;

		public Func<Vector2, Matrix4x4> jitteredMatrixFunc;

		private Dictionary<Type, KeyValuePair<CameraEvent, CommandBuffer>> m_CommandBuffers;

		private List<PostProcessingComponentBase> m_Components;

		private Dictionary<PostProcessingComponentBase, bool> m_ComponentStates;

		private MaterialFactory m_MaterialFactory;

		private RenderTextureFactory m_RenderTextureFactory;

		private PostProcessingContext m_Context;

		private Camera m_Camera;

		private PostProcessingProfile m_PreviousProfile;

		private bool m_RenderingInSceneView;

		private BuiltinDebugViewsComponent m_DebugViews;

		private AmbientOcclusionComponent m_AmbientOcclusion;

		private ScreenSpaceReflectionComponent m_ScreenSpaceReflection;

		private FogComponent m_FogComponent;

		private MotionBlurComponent m_MotionBlur;

		private TaaComponent m_Taa;

		private EyeAdaptationComponent m_EyeAdaptation;

		private DepthOfFieldComponent m_DepthOfField;

		private BloomComponent m_Bloom;

		private ChromaticAberrationComponent m_ChromaticAberration;

		private ColorGradingComponent m_ColorGrading;

		private UserLutComponent m_UserLut;

		private GrainComponent m_Grain;

		private VignetteComponent m_Vignette;

		private DitheringComponent m_Dithering;

		private FxaaComponent m_Fxaa;

		private List<PostProcessingComponentBase> m_ComponentsToEnable;

		private List<PostProcessingComponentBase> m_ComponentsToDisable;

		private void OnEnable()
		{ }

		private void OnPreCull()
		{ }

		private void OnPreRender()
		{ }

		private void OnPostRender()
		{ }

		// Source: Unity PostProcessing v1 (open-source pre-URP package).
		// Original implementation: applies the effect chain (bloom, vignette, DOF, etc.) from `profile`,
		// then Graphics.Blit final result to destination. When profile==null OR no effects enabled,
		// it does Graphics.Blit(source, destination) passthrough.
		// Our Cpp2IL extract has empty body — Unity treats empty OnRenderImage as "didn't write",
		// resulting in BLACK screen output. This breaks UI visibility regardless of Canvas state.
		// 1-1 with passthrough semantics until full PostProcessing package replacement (Phase H paid/free SDK):
		//   - Lua side (PostProcessMgr.lua) creates empty PostProcessingProfile by default
		//   - With no enabled effects, the effect chain is no-op → just Blit
		// This passthrough matches "no effects enabled" branch of the original Unity package.
		// TODO: full port from Unity PostProcessing v1 GitHub source when needed for production builds.
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination);
		}

		private void OnGUI()
		{ }

		private void OnDisable()
		{ }

		public void ResetTemporalEffects()
		{ }

		private void CheckObservers()
		{ }

		private void DisableComponents()
		{ }

		private CommandBuffer AddCommandBuffer<T>(CameraEvent evt, string name) where T : PostProcessingModel
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void RemoveCommandBuffer<T>() where T : PostProcessingModel
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private CommandBuffer GetCommandBuffer<T>(CameraEvent evt, string name) where T : PostProcessingModel
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void TryExecuteCommandBuffer<T>(PostProcessingComponentCommandBuffer<T> component) where T : PostProcessingModel
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private bool TryPrepareUberImageEffect<T>(PostProcessingComponentRenderTexture<T> component, Material material) where T : PostProcessingModel
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private T AddComponent<T>(T component) where T : PostProcessingComponentBase
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public PostProcessingBehaviour()
		{ }
	}
}
