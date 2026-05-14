using System;
using Cpp2IlInjected;
using UnityEngine;

[ExecuteInEditMode]
public abstract class SpriteRoot : MonoBehaviour, IEZLinkedListItem<ISpriteAnimatable>, IUseCamera
{
	public enum SPRITE_PLANE
	{
		XY = 0,
		XZ = 1,
		YZ = 2
	}

	public enum ANCHOR_METHOD
	{
		UPPER_LEFT = 0,
		UPPER_CENTER = 1,
		UPPER_RIGHT = 2,
		MIDDLE_LEFT = 3,
		MIDDLE_CENTER = 4,
		MIDDLE_RIGHT = 5,
		BOTTOM_LEFT = 6,
		BOTTOM_CENTER = 7,
		BOTTOM_RIGHT = 8,
		TEXTURE_OFFSET = 9
	}

	public enum WINDING_ORDER
	{
		CCW = 0,
		CW = 1
	}

	public delegate void SpriteResizedDelegate(float newWidth, float newHeight, SpriteRoot sprite);

	public bool managed;

	public SpriteManager manager;

	// HAND-FIX visibility: dump.cs declares `protected bool addedToManager` but production IL2CPP
	// doesn't enforce protected and SpriteManager (non-subclass, same assembly) writes this field
	// directly inside AddSprite/RemoveSprite. Promoted to `internal` so the assembly-mate access
	// compiles without restructuring the rest of the auto-gen stubs.
	internal bool addedToManager;

	public int drawLayer;

	public bool persistent;

	public SPRITE_PLANE plane;

	public WINDING_ORDER winding;

	public float width;

	public float height;

	public Vector2 bleedCompensation;

	public ANCHOR_METHOD anchor;

	public bool pixelPerfect;

	public bool autoResize;

	protected Vector2 bleedCompensationUV;

	protected Vector2 bleedCompensationUVMax;

	protected SPRITE_FRAME frameInfo;

	protected Rect uvRect;

	protected Vector2 scaleFactor;

	protected Vector2 topLeftOffset;

	protected Vector2 bottomRightOffset;

	protected Vector3 topLeft;

	protected Vector3 bottomRight;

	protected Vector3 unclippedTopLeft;

	protected Vector3 unclippedBottomRight;

	protected Vector2 tlTruncate;

	protected Vector2 brTruncate;

	protected bool truncated;

	protected Rect3D clippingRect;

	protected Rect localClipRect;

	protected float leftClipPct;

	protected float rightClipPct;

	protected float topClipPct;

	protected float bottomClipPct;

	protected bool clipped;

	[HideInInspector]
	public bool billboarded;

	[NonSerialized]
	public bool isClone;

	[NonSerialized]
	public bool uvsInitialized;

	protected bool m_started;

	protected bool deleted;

	public Vector3 offset;

	public Color color;

	protected ISpriteMesh m_spriteMesh;

	protected ISpriteAnimatable m_prev;

	protected ISpriteAnimatable m_next;

	protected Vector2 screenSize;

	public Camera renderCamera;

	protected Vector2 sizeUnitsPerUV;

	[HideInInspector]
	public Vector2 pixelsPerUV;

	[HideInInspector]
	public Renderer _renderer;

	protected float worldUnitsPerScreenPixel;

	protected SpriteResizedDelegate resizedDelegate;

	protected EZScreenPlacement screenPlacer;

	public bool hideAtStart;

	protected bool m_hidden;

	public bool ignoreClipping;

	protected SpriteRootMirror mirror;

	protected Vector2 tempUV;

	protected Mesh oldMesh;

	protected SpriteManager savedManager;

	public bool isMirror;

	public Color Color
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

	public virtual Camera RenderCamera
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

	public Vector2 PixelSize
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

	public Vector2 ImageSize
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public bool Managed
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

	public bool Started
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual Rect3D ClippingRect
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

	public virtual bool Clipped
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

	public ANCHOR_METHOD Anchor
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

	public Vector3 UnclippedTopLeft
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public Vector3 UnclippedBottomRight
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public Vector3 TopLeft
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public Vector3 BottomRight
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public ISpriteMesh spriteMesh
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

	public bool AddedToManager
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

	public ISpriteAnimatable prev
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

	public ISpriteAnimatable next
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

	protected virtual void Awake()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Start()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void CalcSizeUnitsPerUV()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected virtual void Init()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Clear()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Copy(SpriteRoot s)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void InitUVs()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Delete()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected virtual void OnEnable()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected virtual void OnDisable()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void OnDestroy()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void CalcEdges()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void CalcSize()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void SetSize(float w, float h)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void SetSizeXY(float w, float h)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void SetSizeXZ(float w, float h)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void SetSizeYZ(float w, float h)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void TruncateRight(float pct)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void TruncateLeft(float pct)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void TruncateTop(float pct)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void TruncateBottom(float pct)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Untruncate()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Unclip()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void UpdateUVs()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetMirror()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void TransformBillboarded(Transform t)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void SetColor(Color c)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPixelToUV(int texWidth, int texHeight)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPixelToUV(Texture tex)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void CalcPixelToUV()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetTexture(Texture2D tex)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetMaterial(Material mat)
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

	public virtual void SetCamera(Camera c)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Hide(bool tf)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool IsHidden()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void DestroyMesh()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void AddMesh()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetBleedCompensation()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetBleedCompensation(float x, float y)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetBleedCompensation(Vector2 xy)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPlane(SPRITE_PLANE p)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetWindingOrder(WINDING_ORDER order)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetDrawLayer(int layer)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetFrameInfo(SPRITE_FRAME fInfo)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetUVs(Rect uv)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetUVsFromPixelCoords(Rect pxCoords)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Rect GetUVs()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector3[] GetVertices()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector3 GetCenterPoint()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetAnchor(ANCHOR_METHOD a)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetOffset(Vector3 o)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public abstract Vector2 GetDefaultPixelSize(PathFromGUIDDelegate guid2Path, AssetLoaderDelegate loader);

	public Vector2 PixelSpaceToUVSpace(Vector2 xy)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector2 PixelSpaceToUVSpace(int x, int y)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector2 PixelCoordToUVCoord(Vector2 xy)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Vector2 PixelCoordToUVCoord(int x, int y)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public abstract int GetStateIndex(string stateName);

	public abstract void SetState(int index);

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

	protected SpriteRoot()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
