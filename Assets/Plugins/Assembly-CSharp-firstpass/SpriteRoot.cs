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

	// Source: Ghidra get_Color.c RVA 0x01585434 — return color (offset 0x16c).
	// set_Color: TODO RVA 0x01585444 — virtual ColorUpdateLogic chain, pending mapping.
	public Color Color
	{
		get { return color; }
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_RenderCamera.c RVA 0x01585948 — return renderCamera (offset 0x1a0).
	// set_RenderCamera: TODO — Ghidra body has camera-attach logic.
	public virtual Camera RenderCamera
	{
		get { return renderCamera; }
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_PixelSize.c RVA 0x01585E44
	// 1-1: return Vector2(width / pixelsPerUV.x, ...).
	// Ghidra simplifies to single float — only the x-component visible. In C# return
	// the full Vector2 derived from width/pixelsPerUV.
	public Vector2 PixelSize
	{
		get { return new Vector2(width / pixelsPerUV.x, height / pixelsPerUV.y); }
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

	// Source: Ghidra get_Managed.c RVA 0x01585E88 — return managed (offset 0x20).
	// set_Managed: complex — see Ghidra RVA 0x01585E90 (Manager.RemoveSprite call + AddMesh).
	// TODO for setter: implement Manager attach/detach logic.
	public bool Managed
	{
		get { return managed; }
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_Started.c RVA 0x01586224 — return m_started (offset 0x15c).
	public bool Started { get { return m_started; } }

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

	// Source: Ghidra get_Clipped.c RVA 0x01586A84 — return clipped (offset 0x158).
	// set_Clipped: TODO — Ghidra has set body but typically `clipped = value; if changed CalcClip()`.
	public virtual bool Clipped
	{
		get { return clipped; }
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_Anchor.c RVA 0x01586ADC / set_Anchor.c RVA 0x01586AE4
	// 1-1 get: return anchor (offset 0x54).
	// 1-1 set: anchor = value; CalcSize() (virtual at vtable+0x298 — SpriteRoot.CalcSize).
	public ANCHOR_METHOD Anchor
	{
		get { return anchor; }
		set
		{
			anchor = value;
			CalcSize();   // virtual on this (vtable+0x298) — resolves to SpriteRoot.CalcSize override chain
		}
	}

	// Source: Ghidra get_UnclippedTopLeft.c RVA 0x01586B1C
	// 1-1: if (!m_started) Awake();   // virtual call vtable+0x208 = SpriteRoot.Awake
	//      return unclippedTopLeft;   // offset 0xd4
	public Vector3 UnclippedTopLeft
	{
		get
		{
			if (!m_started) Awake();
			return unclippedTopLeft;
		}
	}

	// Source: Ghidra get_UnclippedBottomRight.c RVA 0x01586B50
	// 1-1: if (!m_started) Awake(); return unclippedBottomRight; (Vector3 at offset 0xe0).
	public Vector3 UnclippedBottomRight
	{
		get
		{
			if (!m_started) Awake();
			return unclippedBottomRight;
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

	// Source: Ghidra get_spriteMesh.c RVA 0x01586DA0 — return m_spriteMesh (offset 0x180).
	// set_spriteMesh: TODO RVA 0x01581BC4 (92 lines) — complex with sprite-attachment logic.
	public ISpriteMesh spriteMesh
	{
		get { return m_spriteMesh; }
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_AddedToManager.c RVA 0x01586DA8 / set_AddedToManager.c RVA 0x01586DB0
	// 1-1: return/assign addedToManager (offset 0x30, byte stored with mask 0x1).
	public bool AddedToManager
	{
		get { return addedToManager; }
		set { addedToManager = value; }
	}

	// Source: Ghidra get_prev.c RVA 0x01586E38 / set_prev.c RVA 0x01586E40
	// 1-1: return/assign m_prev (offset 0x188).
	public ISpriteAnimatable prev
	{
		get { return m_prev; }
		set { m_prev = value; }
	}

	// Source: Ghidra get_next.c RVA 0x01586E50 / set_next.c RVA 0x01586E58
	// 1-1: return/assign m_next (offset 0x190 = 400 decimal).
	public ISpriteAnimatable next
	{
		get { return m_next; }
		set { m_next = value; }
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

	// Source: Ghidra IsHidden.c RVA 0x01585E3C — return m_hidden (byte at offset 0x1d9).
	public bool IsHidden() { return m_hidden; }

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

	// Source: Ghidra SetWindingOrder.c RVA 0x015862AC
	// 1-1: winding = order (offset 0x40). If !managed && spriteMesh != null, dispatch virtual
	// vtable+0x388 on spriteMesh (ISpriteMesh.SetWindingOrder). Type-cast checked via klass.
	public void SetWindingOrder(WINDING_ORDER order)
	{
		winding = order;
		if (managed) return;
		// TODO: spriteMesh virtual vtable+0x388 (SetWindingOrder on SpriteMesh implementation) —
		// no public C# property for this on ISpriteMesh interface yet. Stays a no-op until
		// SpriteMesh.SetWindingOrder is exposed.
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

	// Source: Ghidra PixelSpaceToUVSpace.c RVA 0x01586230
	// 1-1: if (pixelsPerUV.x == 0 || pixelsPerUV.y == 0) return Vector2.zero;
	//      return new Vector2(xy.x / pixelsPerUV.x, xy.y / pixelsPerUV.y);
	// Ghidra simplifies to single float; full Vector2 inferred from signature.
	public Vector2 PixelSpaceToUVSpace(Vector2 xy)
	{
		if (pixelsPerUV.x == 0f || pixelsPerUV.y == 0f) return Vector2.zero;
		return new Vector2(xy.x / pixelsPerUV.x, xy.y / pixelsPerUV.y);
	}

	// Source: Ghidra PixelSpaceToUVSpace_1.c RVA 0x01586DBC — calls Vector2 overload with (float)x, (float)y.
	public Vector2 PixelSpaceToUVSpace(int x, int y) { return PixelSpaceToUVSpace(new Vector2(x, y)); }

	// Source: Ghidra PixelCoordToUVCoord.c RVA 0x01586DC8 — identical body to PixelSpaceToUVSpace.
	public Vector2 PixelCoordToUVCoord(Vector2 xy)
	{
		if (pixelsPerUV.x == 0f || pixelsPerUV.y == 0f) return Vector2.zero;
		return new Vector2(xy.x / pixelsPerUV.x, xy.y / pixelsPerUV.y);
	}

	// Source: Ghidra PixelCoordToUVCoord_1.c RVA 0x01586644 — calls Vector2 overload with (float)x, (float)y.
	public Vector2 PixelCoordToUVCoord(int x, int y) { return PixelCoordToUVCoord(new Vector2(x, y)); }

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
