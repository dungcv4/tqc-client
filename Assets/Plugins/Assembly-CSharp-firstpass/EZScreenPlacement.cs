using System;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
[AddComponentMenu("EZ GUI/Utility/EZ Screen Placement")]
public class EZScreenPlacement : MonoBehaviour, IUseCamera
{
	public enum HORIZONTAL_ALIGN
	{
		NONE = 0,
		SCREEN_LEFT = 1,
		SCREEN_RIGHT = 2,
		SCREEN_CENTER = 3,
		OBJECT = 4
	}

	public enum VERTICAL_ALIGN
	{
		NONE = 0,
		SCREEN_TOP = 1,
		SCREEN_BOTTOM = 2,
		SCREEN_CENTER = 3,
		OBJECT = 4
	}

	[Serializable]
	public class RelativeTo
	{
		public HORIZONTAL_ALIGN horizontal;

		public VERTICAL_ALIGN vertical;

		protected EZScreenPlacement script;

		public EZScreenPlacement Script
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			set
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool Equals(RelativeTo rt)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Copy(RelativeTo rt)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public RelativeTo(EZScreenPlacement sp, RelativeTo rt)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public RelativeTo(EZScreenPlacement sp)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public Camera renderCamera;

	public Vector3 screenPos;

	public bool ignoreZ;

	public RelativeTo relativeTo;

	public Transform relativeObject;

	public bool alwaysRecursive;

	public bool allowTransformDrag;

	protected Vector2 screenSize;

	[NonSerialized]
	protected bool justEnabled;

	[NonSerialized]
	protected EZScreenPlacementMirror mirror;

	protected bool m_awake;

	protected bool m_started;

	[HideInInspector]
	public EZScreenPlacement[] dependents;

	public Camera RenderCamera
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public Vector3 ScreenCoord
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	private void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PositionOnScreenRecursively()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector3 ScreenPosToLocalPos(Vector3 screenPos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector3 ScreenPosToParentPos(Vector3 screenPos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector3 ScreenPosToWorldPos(Vector3 screenPos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PositionOnScreen()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PositionOnScreen(int x, int y, float depth)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void PositionOnScreen(Vector3 pos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void UpdateCamera()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetCamera()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetCamera(Camera c)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void WorldToScreenPos(Vector3 worldPos)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static bool TestDepenency(EZScreenPlacement sp)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void DoMirror()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void OnDrawGizmosSelected()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void OnDrawGizmos()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public EZScreenPlacement()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
