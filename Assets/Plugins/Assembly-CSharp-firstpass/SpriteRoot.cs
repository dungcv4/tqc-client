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
	// Source: Ghidra set_Color.c RVA 0x01585448 — virtual dispatch SetColor (vtable+0x318).
	public Color Color
	{
		get { return color; }
		set { SetColor(value); }
	}

	// Source: Ghidra get_RenderCamera.c RVA 0x01585948 — return renderCamera (offset 0x1a0).
	// Source: Ghidra set_RenderCamera.c RVA 0x01585950
	// 1-1: renderCamera = value; SetCamera(value) virtual vtable+0x368.
	public virtual Camera RenderCamera
	{
		get { return renderCamera; }
		set
		{
			renderCamera = value;
			SetCamera(value);
		}
	}

	// Source: Ghidra get_PixelSize.c RVA 0x01585E44
	// 1-1: return Vector2(width / pixelsPerUV.x, height / pixelsPerUV.y).
	// Source: Ghidra set_PixelSize.c RVA 0x01585E58
	// 1-1: SetSize(value.x * worldUnitsPerScreenPixel, value.y * worldUnitsPerScreenPixel) (vtable+0x298).
	public Vector2 PixelSize
	{
		get { return new Vector2(width / pixelsPerUV.x, height / pixelsPerUV.y); }
		set { SetSize(value.x * worldUnitsPerScreenPixel, value.y * worldUnitsPerScreenPixel); }
	}

	public Vector2 ImageSize
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_Managed.c RVA 0x01585E88 — return managed (offset 0x20).
	// Source: Ghidra set_Managed.c RVA 0x01585E90
	// 1-1: if (!value) {
	//          if (managed && manager != null/destroyed) manager.RemoveSprite(this);
	//          manager = null; managed = false;
	//          if (spriteMesh == null || not SpriteMesh) AddMesh();
	//      } else {
	//          if (!managed) DestroyMesh();
	//          managed = true;
	//      }
	public bool Managed
	{
		get { return managed; }
		set
		{
			if (!value)
			{
				if (managed && (UnityEngine.Object)manager != null)
				{
					manager.RemoveSprite(this);
				}
				manager = null;
				managed = false;
				if (m_spriteMesh == null) AddMesh();
				return;
			}
			if (!managed) DestroyMesh();
			managed = true;
		}
	}

	// Source: Ghidra get_Started.c RVA 0x01586224 — return m_started (offset 0x15c).
	public bool Started { get { return m_started; } }

	// Source: Ghidra get_ClippingRect.c RVA 0x01586938 — copy Rect3D struct from offset 0x100..0x130.
	// 1-1 get: return clippingRect (Rect3D is 7 fields of 8 bytes = 56 bytes total).
	// set: TODO — Ghidra has set body; pending Rect3D field-name mapping.
	public virtual Rect3D ClippingRect
	{
		get { return clippingRect; }
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	// Source: Ghidra get_Clipped.c RVA 0x01586A84 — return clipped (offset 0x158).
	// Source: Ghidra set_Clipped.c RVA 0x01586A8C
	// 1-1: if (deleted) return. If value matches current clipped state → call vtable+0x2f8 (Unclip
	// or no-op). If value is true and !clipped → set clipped=true + CalcSize + UpdateUVs.
	// TODO: virtual slot 0x2f8 mapping pending (likely UpdateUVs/Unclip). For now: simple field set
	// + CalcSize if transitioning from unclipped to clipped.
	public virtual bool Clipped
	{
		get { return clipped; }
		set
		{
			if (deleted) return;
			if (!value && !clipped) return;
			if (value && !clipped)
			{
				clipped = true;
				CalcSize();
				return;
			}
			// Transition clipped → unclipped or no change: dispatch UpdateUVs.
			UpdateUVs();
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

	// Source: Ghidra get_TopLeft.c RVA 0x01586B84
	// 1-1: if (spriteMesh == null) return Vector3.zero;
	//      else return spriteMesh.vertices[0] (virtual vtable+4 = get_vertices).
	public Vector3 TopLeft
	{
		get
		{
			if (m_spriteMesh == null) return Vector3.zero;
			Vector3[] verts = m_spriteMesh.vertices;
			if (verts == null || verts.Length == 0) throw new System.NullReferenceException();
			return verts[0];
		}
	}

	// Source: Ghidra get_BottomRight.c RVA 0x01586C90
	// 1-1: if (spriteMesh == null) return Vector3.zero;
	//      else return spriteMesh.vertices[2] (offset 0x20 + 2*12 = 0x38 in Ghidra).
	public Vector3 BottomRight
	{
		get
		{
			if (m_spriteMesh == null) return Vector3.zero;
			Vector3[] verts = m_spriteMesh.vertices;
			if (verts == null || verts.Length < 3) throw new System.NullReferenceException();
			return verts[2];
		}
	}

	// Source: Ghidra get_spriteMesh.c RVA 0x01586DA0 — return m_spriteMesh (offset 0x180).
	// Source: Ghidra set_spriteMesh.c RVA 0x01581BC4 (92 lines)
	// 1-1: m_spriteMesh = value. If value != null: call value.get_sprite() virtual; if it !=
	// this, call value.set_sprite(this) virtual (vtable+1 on ISpriteMesh). If managed && value
	// is SpriteMesh_Managed, manager = value.manager.
	public ISpriteMesh spriteMesh
	{
		get { return m_spriteMesh; }
		set
		{
			m_spriteMesh = value;
			if (m_spriteMesh == null) return;
			SpriteRoot owner = m_spriteMesh.sprite;
			if ((UnityEngine.Object)owner != this)
			{
				m_spriteMesh.sprite = this;
			}
			if (managed)
			{
				SpriteMesh_Managed sm = m_spriteMesh as SpriteMesh_Managed;
				if (sm == null) throw new System.InvalidCastException();
				manager = sm.manager;
			}
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

	// Source: Ghidra Awake.c RVA 0x0157FFF0
	// 1-1: savedManager = null; check name suffix; if (!managed) cache+clear MeshFilter.sharedMesh
	// and AddMesh; else if (manager==null) LogError, else manager.AddSprite(this).
	// Note: PTR_StringLiteral_650 (the "_mirror" suffix?) not yet resolved — skip the EndsWith check.
	protected virtual void Awake()
	{
		savedManager = null;
		string n = gameObject.name;
		if (n == null) throw new System.NullReferenceException();
		// TODO: isMirror = n.EndsWith(MIRROR_SUFFIX) — literal at PTR_StringLiteral_650.
		if (!managed)
		{
			MeshFilter mf = GetComponent<MeshFilter>();
			if ((UnityEngine.Object)mf != null)
			{
				oldMesh = mf.sharedMesh;
				mf.sharedMesh = null;
			}
			AddMesh();
			return;
		}
		if ((UnityEngine.Object)manager == null)
		{
			Debug.LogError("SpriteRoot " + n + " has no manager assigned.");
			return;
		}
		manager.AddSprite(this);
	}

	// Source: Ghidra Start.c RVA 0x015802F0
	// 1-1: m_started=true; managed-mode branch: if !managed && isPlaying → destroy oldMesh; else if
	// manager!=null → Init() (virtual vtable+0x218). If persistent → DontDestroyOnLoad. If no manager
	// and !managed → AddMesh(). CalcSizeUnitsPerUV(). If renderCamera == null → Camera.main.
	// SetCamera(renderCamera). If hideAtStart → Hide(true).
	// TODO: virtual slot mappings for vtable+0x218 (Init?) and vtable+0x308 (autoResize handler)
	// and texture-pull from manager at vtable+2 still pending.
	public virtual void Start()
	{
		m_started = true;
		if (!managed)
		{
			if (UnityEngine.Application.isPlaying && oldMesh != null)
			{
				UnityEngine.Object.Destroy(oldMesh);
			}
			oldMesh = null;
		}
		else if ((UnityEngine.Object)manager != null)
		{
			Init();
		}
		if (persistent)
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		if ((UnityEngine.Object)manager == null && !managed) AddMesh();
		CalcSizeUnitsPerUV();
		if ((UnityEngine.Object)renderCamera == null)
		{
			renderCamera = Camera.main;
		}
		SetCamera(renderCamera);
		if (hideAtStart) Hide(true);
	}

	// Source: Ghidra CalcSizeUnitsPerUV.c RVA 0x01583E60
	// 1-1:
	//   if (uvRect.size.x == 0 || uvRect.size.y == 0 || (uvRect.size.x==1 && uvRect.position.y==1)) {
	//       sizeUnitsPerUV = Vector2.zero;
	//   } else {
	//       sizeUnitsPerUV.x = width / uvRect.width;       // 0x44 / 0x74
	//       sizeUnitsPerUV.y = height / uvRect.height;     // 0x48 / 0x78
	//   }
	// Ghidra also checks frameInfo.uvs at 0x6c/0x70/0x74/0x78 (same as uvRect — they're kept in sync).
	protected void CalcSizeUnitsPerUV()
	{
		if (uvRect.width == 0f || uvRect.height == 0f ||
		    (uvRect.y == 1f && uvRect.x == 1f))
		{
			sizeUnitsPerUV = Vector2.zero;
		}
		else
		{
			sizeUnitsPerUV = new Vector2(width / uvRect.width, height / uvRect.height);
		}
	}

	// Source: Ghidra Init.c RVA 0x01583EFC
	// 1-1: cache EZScreenPlacement component into screenPlacer (0x1d0). If it exists, set its camera
	// to this.renderCamera. If spriteMesh != null and persistent and !managed, DontDestroyOnLoad on
	// spriteMesh.gameObject (vtable+0x308 on SpriteMesh). Then try fetching texture via spriteMesh
	// vtable+2 and SetPixelToUV(tex). Final virtual on spriteMesh vtable+9 (likely UpdateUVs).
	// If !isPlaying → CalcSizeUnitsPerUV.
	// TODO: virtual slot mappings on SpriteMesh (offset 0x308 = gameObject? +2 = texture? +9 = update).
	// Skeletal port preserves the screenPlacer cache and CalcSizeUnitsPerUV.
	protected virtual void Init()
	{
		screenPlacer = GetComponent<EZScreenPlacement>();
		if ((UnityEngine.Object)screenPlacer != null)
		{
			screenPlacer.SetCamera(renderCamera);
		}
		// TODO: spriteMesh virtuals for DontDestroyOnLoad, texture pull, and post-init update.
		if (!UnityEngine.Application.isPlaying)
		{
			CalcSizeUnitsPerUV();
		}
	}

	// Source: Ghidra Clear.c RVA 0x0158063C
	// 1-1: truncated = false (offset 0x159); SetColor(Color.white) via virtual vtable+0x318;
	// offset = Vector3.zero (offsets 0x160..0x16b).
	public virtual void Clear()
	{
		truncated = false;
		SetColor(Color.white);
		offset = Vector3.zero;
	}

	// Source: Ghidra Copy.c RVA 0x01580B3C (~150 lines)
	// Complex: copy texture/material from `s` via spriteMesh virtuals + Renderer.sharedMaterial.
	// Copies drawLayer, renderCamera, plane, anchor, bleedCompensation, autoResize, pixelPerfect,
	// m_hidden, uvRect, scaleFactor, topLeftOffset, bottomRightOffset, width/height, and color via
	// virtual SetColor (vtable+0x318).
	// TODO: virtual slot mappings on spriteMesh — full port pending. Skeletal port handles primitive
	// field copy only.
	public virtual void Copy(SpriteRoot s)
	{
		if (s == null) throw new System.NullReferenceException();
		drawLayer = s.drawLayer;
		if ((UnityEngine.Object)s.renderCamera != null)
		{
			SetCamera(s.renderCamera);
		}
		if ((UnityEngine.Object)renderCamera == null) renderCamera = Camera.main;
		plane = s.plane;
		anchor = s.anchor;
		bleedCompensation = s.bleedCompensation;
		autoResize = s.autoResize;
		pixelPerfect = s.pixelPerfect;
		// m_hidden mirror at offset 0x1da (not 0x1d9 — could be a separate "shouldBeHidden" flag)
		uvRect = s.uvRect;
		scaleFactor = s.scaleFactor;
		topLeftOffset = s.topLeftOffset;
		bottomRightOffset = s.bottomRightOffset;
		width = s.width;
		height = s.height;
		SetColor(s.color);
	}

	// Source: Ghidra InitUVs.c RVA 0x015842BC
	// 1-1: uvRect.size = frameInfo.uvs.size; uvRect.position = frameInfo.uvs.position. Equivalent to
	// uvRect = frameInfo.uvs (single struct copy).
	public virtual void InitUVs()
	{
		uvRect = frameInfo.uvs;
	}

	// Source: Ghidra Delete.c RVA 0x015806E8
	// 1-1: deleted = true; if !managed && isPlaying && spriteMesh is SpriteMesh_Managed,
	// destroy spriteMesh's gameObject (via virtual vtable+0x308) and set it to null (vtable+0x318).
	// TODO: spriteMesh virtual slots — preserve deleted-flag assignment only.
	public virtual void Delete()
	{
		deleted = true;
		// TODO: spriteMesh destroy logic via virtual slots.
	}

	// Source: Ghidra OnEnable.c RVA 0x01580998
	// 1-1: if (managed && manager != null && m_started) → save uvRect fields, AddSprite(this),
	// restore uvRect, SetBleedCompensation. Else if savedManager != null → AddSprite(this) on it.
	// Pragmatic port: just re-add to manager if started.
	protected virtual void OnEnable()
	{
		if (managed && (UnityEngine.Object)manager != null && m_started)
		{
			// Save and restore uvRect+frameInfo (AddSprite may mutate them)
			Rect savedUv = uvRect;
			SPRITE_FRAME savedFrame = frameInfo;
			manager.AddSprite(this);
			uvRect = savedUv;
			frameInfo = savedFrame;
			SetBleedCompensation(bleedCompensation);
			return;
		}
		if ((UnityEngine.Object)savedManager != null)
		{
			savedManager.AddSprite(this);
		}
	}

	// Source: Ghidra OnDisable.c RVA 0x0158086C
	// 1-1: if (managed && manager != null) → savedManager = manager; manager.RemoveSprite(this).
	protected virtual void OnDisable()
	{
		if (!managed) return;
		if ((UnityEngine.Object)manager == null) return;
		savedManager = manager;
		manager.RemoveSprite(this);
	}

	// Source: Ghidra OnDestroy.c RVA 0x015842C8
	// 1-1: virtual call vtable+0x258 — likely Delete (the virtual cleanup). Dispatches through C# vcall.
	public virtual void OnDestroy()
	{
		Delete();
	}

	// Source: Ghidra CalcEdges.c RVA 0x015842D8 (260 lines)
	// Branches on `anchor` enum (10 cases) to set topLeft/bottomRight extents based on width/height.
	// Then applies offset (0x160), truncation (tlTruncate/brTruncate at 0xec/0xf4), and clipping
	// (clippingRect at 0x138/0x13c/0x140/0x144). Complex geometric math.
	// TODO RVA 0x015842D8 — defer multi-branch anchor math + clip logic to a focused session.
	public void CalcEdges()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra CalcSize.c RVA 0x01581284
	// 1-1:
	//   if (uvRect.width == 0)  uvRect.width  = 1e-7f;     // 0x9c (offset within uvRect)
	//   if (uvRect.height == 0) uvRect.height = 1e-7f;     // 0xa0
	//   if (!pixelPerfect) {                                 // (param_1+0xb)
	//       if (autoResize && sizeUnitsPerUV.x != 0 && sizeUnitsPerUV.y != 0) {
	//           width  = sizeUnitsPerUV.x * uvRect.width;
	//           height = sizeUnitsPerUV.y * uvRect.height;
	//       }
	//   } else {
	//       width  = uvRect.width  * worldUnitsPerScreenPixel * pixelsPerUV.x;
	//       height = uvRect.height * worldUnitsPerScreenPixel * pixelsPerUV.y;
	//   }
	//   CalcEdges();   // virtual vtable+0x298
	// Field map: 0x35 (long-array) = sizeUnitsPerUV.x at byte 0x1a8; 0x38 (byte float)
	// = worldUnitsPerScreenPixel at 0x1c0; 0x36 = pixelsPerUV at 0x1b0.
	public void CalcSize()
	{
		// Guard against zero divisor (Ghidra writes 1e-7f sentinel 0x33d6bf95)
		if (uvRect.width  == 0f) uvRect = new Rect(uvRect.x, uvRect.y, 1e-7f, uvRect.height);
		if (uvRect.height == 0f) uvRect = new Rect(uvRect.x, uvRect.y, uvRect.width, 1e-7f);
		if (!pixelPerfect)
		{
			if (autoResize && sizeUnitsPerUV.x != 0f && sizeUnitsPerUV.y != 0f)
			{
				width  = sizeUnitsPerUV.x * uvRect.width;
				height = sizeUnitsPerUV.y * uvRect.height;
			}
		}
		else
		{
			width  = uvRect.width  * worldUnitsPerScreenPixel * pixelsPerUV.x;
			height = uvRect.height * worldUnitsPerScreenPixel * pixelsPerUV.y;
		}
		CalcEdges();
	}

	// Source: Ghidra SetSize.c RVA 0x015846C4
	// 1-1: only proceed if spriteMesh != null; width = w; height = h; CalcSizeUnitsPerUV();
	// dispatch by plane: SetSizeYZ/XZ/XY. If resizedDelegate != null, invoke it.
	public virtual void SetSize(float w, float h)
	{
		if (m_spriteMesh == null) return;
		width = w;
		height = h;
		CalcSizeUnitsPerUV();
		switch ((int)plane)
		{
			case 2: SetSizeYZ(w, h); break;
			case 1: SetSizeXZ(w, h); break;
			default: SetSizeXY(w, h); break;
		}
		if (resizedDelegate != null)
		{
			resizedDelegate.Invoke(width, height, this);
		}
	}

	// Source: Ghidra SetSizeXY.c RVA 0x01584740
	// 1-1: CalcEdges(); fill spriteMesh.vertices (virtual vtable+4) with 4 quad corners in XY plane:
	//   v[0] = (offset.x + topLeft.x,      offset.y + topLeft.y,      offset.z)
	//   v[1] = (offset.x + topLeft.x,      offset.y + bottomRight.y,  offset.z)
	//   v[2] = (offset.x + bottomRight.x,  offset.y + bottomRight.y,  offset.z)
	//   v[3] = (offset.x + bottomRight.x,  offset.y + topLeft.y,      offset.z)
	// Then dispatch UpdateVerts (spriteMesh virtual vtable+10).
	// Field map: topLeft (0xbc,0xc0,0xc4 — Vector3), bottomRight (0xc8,0xcc,0xd0 — Vector3).
	protected void SetSizeXY(float w, float h)
	{
		CalcEdges();
		if (m_spriteMesh == null) return;
		Vector3[] verts = m_spriteMesh.vertices;
		if (verts == null || verts.Length < 4) return;
		verts[0] = new Vector3(offset.x + topLeft.x,      offset.y + topLeft.y,      offset.z);
		verts[1] = new Vector3(offset.x + topLeft.x,      offset.y + bottomRight.y,  offset.z);
		verts[2] = new Vector3(offset.x + bottomRight.x,  offset.y + bottomRight.y,  offset.z);
		verts[3] = new Vector3(offset.x + bottomRight.x,  offset.y + topLeft.y,      offset.z);
		m_spriteMesh.UpdateVerts();
	}

	// Source: Ghidra SetSizeXZ.c RVA 0x015848F8
	// 1-1: Same as XY but in XZ plane: y is offset.y, x/z swap with topLeft.x/bottomRight.y.
	//   v[0] = (offset.x + topLeft.x,      offset.y, offset.z + topLeft.y)
	//   v[1] = (offset.x + topLeft.x,      offset.y, offset.z + bottomRight.y)
	//   v[2] = (offset.x + bottomRight.x,  offset.y, offset.z + bottomRight.y)
	//   v[3] = (offset.x + bottomRight.x,  offset.y, offset.z + topLeft.y)
	protected void SetSizeXZ(float w, float h)
	{
		CalcEdges();
		if (m_spriteMesh == null) return;
		Vector3[] verts = m_spriteMesh.vertices;
		if (verts == null || verts.Length < 4) return;
		verts[0] = new Vector3(offset.x + topLeft.x,      offset.y, offset.z + topLeft.y);
		verts[1] = new Vector3(offset.x + topLeft.x,      offset.y, offset.z + bottomRight.y);
		verts[2] = new Vector3(offset.x + bottomRight.x,  offset.y, offset.z + bottomRight.y);
		verts[3] = new Vector3(offset.x + bottomRight.x,  offset.y, offset.z + topLeft.y);
		m_spriteMesh.UpdateVerts();
	}

	// Source: Ghidra SetSizeYZ.c RVA 0x01584AD0
	// 1-1: YZ plane (x is offset.x, vertices vary in y/z).
	//   v[0] = (offset.x, offset.y + topLeft.x, offset.z + topLeft.y)         (NEON_rev64 swap noted)
	//   v[1] = (offset.x, offset.y + bottomRight.y, offset.z + topLeft.x)
	//   v[2] = (offset.x, offset.y + bottomRight.x, offset.z + bottomRight.y) (NEON_rev64)
	//   v[3] = (offset.x, offset.y + topLeft.y, offset.z + bottomRight.x)
	// Note: YZ uses topLeft/bottomRight swapped components due to plane rotation.
	protected void SetSizeYZ(float w, float h)
	{
		CalcEdges();
		if (m_spriteMesh == null) return;
		Vector3[] verts = m_spriteMesh.vertices;
		if (verts == null || verts.Length < 4) return;
		verts[0] = new Vector3(offset.x, offset.y + topLeft.x,      offset.z + topLeft.y);
		verts[1] = new Vector3(offset.x, offset.y + bottomRight.y,  offset.z + topLeft.x);
		verts[2] = new Vector3(offset.x, offset.y + bottomRight.x,  offset.z + bottomRight.y);
		verts[3] = new Vector3(offset.x, offset.y + topLeft.y,      offset.z + bottomRight.x);
		m_spriteMesh.UpdateVerts();
	}

	// Source: Ghidra TruncateRight.c RVA 0x01584C94
	// 1-1: tlTruncate.x = 1; brTruncate.x = clamp01(pct);
	//   if (pct < 0) brTruncate.x = 0 (truncate everything off right side);
	//   if (pct > 1) brTruncate.x = 1 (no truncation);
	//   if (pct == 1 && tlTruncate.y == 1 && brTruncate.y == 1) → Untruncate (virtual vtable+0x2e8).
	//   else → truncated = true; UpdateUVs (virtual vtable+0x308); CalcSize.
	public virtual void TruncateRight(float pct)
	{
		tlTruncate.x = 1f;
		if (pct < 0f)      brTruncate.x = 0f;
		else if (pct > 1f) brTruncate.x = 1f;
		else               brTruncate.x = pct;
		if (brTruncate.x >= 1f && tlTruncate.y >= 1f && brTruncate.y >= 1f)
		{
			Untruncate();
			return;
		}
		truncated = true;
		UpdateUVs();
		CalcSize();
	}

	// Source: Ghidra TruncateLeft.c RVA 0x01584D30
	// 1-1: brTruncate.x = 1; tlTruncate.x = clamp01(pct). Same Untruncate-or-update logic.
	public virtual void TruncateLeft(float pct)
	{
		brTruncate.x = 1f;
		if (pct < 0f)      tlTruncate.x = 0f;
		else if (pct > 1f) tlTruncate.x = 1f;
		else               tlTruncate.x = pct;
		if (tlTruncate.x >= 1f && tlTruncate.y >= 1f && brTruncate.y >= 1f)
		{
			Untruncate();
			return;
		}
		truncated = true;
		UpdateUVs();
		CalcSize();
	}

	// Source: Ghidra TruncateTop.c RVA 0x01584DDC
	// 1-1: brTruncate.y = 1; tlTruncate.y = clamp01(pct). Untruncate-or-update.
	public virtual void TruncateTop(float pct)
	{
		brTruncate.y = 1f;
		if (pct < 0f)      tlTruncate.y = 0f;
		else if (pct > 1f) tlTruncate.y = 1f;
		else               tlTruncate.y = pct;
		if (tlTruncate.y >= 1f && tlTruncate.x >= 1f && brTruncate.x >= 1f)
		{
			Untruncate();
			return;
		}
		truncated = true;
		UpdateUVs();
		CalcSize();
	}

	// Source: Ghidra TruncateBottom.c RVA 0x01584E88
	// 1-1: tlTruncate.y = 1; brTruncate.y = clamp01(pct). Untruncate-or-update.
	public virtual void TruncateBottom(float pct)
	{
		tlTruncate.y = 1f;
		if (pct < 0f)      brTruncate.y = 0f;
		else if (pct > 1f) brTruncate.y = 1f;
		else               brTruncate.y = pct;
		if (brTruncate.y >= 1f && tlTruncate.x >= 1f && brTruncate.x >= 1f)
		{
			Untruncate();
			return;
		}
		truncated = true;
		UpdateUVs();
		CalcSize();
	}

	// Source: Ghidra Untruncate.c RVA 0x01584F24
	// 1-1: tlTruncate = Vector2.one (0xec, 0xf0); brTruncate = Vector2.one (0xf4, 0xf8);
	//      truncated = false (0xfc); uvRect = frameInfo.uvs; SetBleedCompensation(); CalcSize().
	public virtual void Untruncate()
	{
		tlTruncate = Vector2.one;
		brTruncate = Vector2.one;
		truncated = false;
		uvRect = frameInfo.uvs;
		SetBleedCompensation(bleedCompensation);
		CalcSize();
	}

	// Source: Ghidra Unclip.c RVA 0x01584F54
	// 1-1: if (m_hidden) return; (deleted maybe — Ghidra checks 0x1da)
	//      leftClipPct = topClipPct = 1 (0x148, 0x150); rightClipPct = bottomClipPct = 1 (0x14c, 0x154);
	//      clipped = false (0x158); uvRect = frameInfo.uvs; SetBleedCompensation(); CalcSize().
	// Note: Ghidra uses 0x1da as the gate — `m_hidden`-like flag at offset 0x1da (not 0x1d9). We
	// use deleted as the closest matching field (deleted at 0x15d is similar gate semantic).
	public virtual void Unclip()
	{
		if (deleted) return;
		leftClipPct   = 1f;
		topClipPct    = 1f;
		rightClipPct  = 1f;
		bottomClipPct = 1f;
		clipped = false;
		uvRect = frameInfo.uvs;
		SetBleedCompensation(bleedCompensation);
		CalcSize();
	}

	// Source: Ghidra UpdateUVs.c RVA 0x01584F98 (123 lines)
	// Computes uvRect based on uvFrame, scaleFactor, topLeftOffset, bottomRightOffset, bleedCompensation,
	// truncated-or-clipped flags, and then calls spriteMesh virtual (vtable+0xb on SpriteMesh =
	// UpdateUVs on the mesh implementation).
	// TODO RVA 0x01584F98 — defer full geometric uvRect derivation pending field-name resolution.
	public virtual void UpdateUVs()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra SetMirror.c RVA 0x015851B8
	// 1-1: gets spriteMesh.vertices (virtual vtable+5 on SpriteMesh); fills 4 verts based on
	// uvRect and isMirror flag. Then calls spriteMesh virtual vtable+0xb (UpdateUVs).
	// TODO RVA 0x015851B8 — defer until SpriteMesh virtual slot mapping is resolved.
	public void SetMirror()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra TransformBillboarded.c RVA 0x0158533C — empty body (returns immediately).
	public void TransformBillboarded(Transform t) { }

	// Source: Ghidra SetColor.c RVA 0x01585340
	// 1-1: color = c (offsets 0x16c..0x178); if (spriteMesh != null) spriteMesh.UpdateColors(c).
	// Virtual call on spriteMesh at vtable+0xC slot (UpdateColors on ISpriteMesh).
	public virtual void SetColor(Color c)
	{
		color = c;
		if (m_spriteMesh == null) return;
		m_spriteMesh.UpdateColors(c);
	}

	// Source: Ghidra SetPixelToUV.c RVA 0x01585458
	// 1-1:
	//   float oldX = pixelsPerUV.x;  float oldY = pixelsPerUV.y;
	//   pixelsPerUV.x = texWidth;    pixelsPerUV.y = texHeight;
	//   if (oldX != 0 && oldY != 0 && uvRect.width != 0 && uvRect.height != 0) {
	//       sizeUnitsPerUV.x = (width  / (oldX * uvRect.width))  * texWidth;
	//       sizeUnitsPerUV.y = (height / (oldY * uvRect.height)) * texHeight;
	//   }
	public void SetPixelToUV(int texWidth, int texHeight)
	{
		float oldX = pixelsPerUV.x;
		float oldY = pixelsPerUV.y;
		pixelsPerUV.x = (float)texWidth;
		pixelsPerUV.y = (float)texHeight;
		if (oldX != 0f && oldY != 0f && uvRect.width != 0f && uvRect.height != 0f)
		{
			sizeUnitsPerUV.x = (width  / (oldX * uvRect.width))  * (float)texWidth;
			sizeUnitsPerUV.y = (height / (oldY * uvRect.height)) * (float)texHeight;
		}
	}

	// Source: Ghidra SetPixelToUV_1.c RVA 0x01581EC4
	// 1-1: if (tex == null/destroyed) return;
	//      SetPixelToUV(tex.width, tex.height);   // tex.width = virtual vtable+0x188, tex.height = +0x1a8
	public void SetPixelToUV(Texture tex)
	{
		if ((UnityEngine.Object)tex == null) return;
		SetPixelToUV(tex.width, tex.height);
	}

	public void CalcPixelToUV()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra SetTexture.c RVA 0x015857A8
	// 1-1:
	//   if (managed) return;
	//   if (_renderer == null/destroyed) return;
	//   Material mat = _renderer.material;
	//   mat.mainTexture = tex;
	//   SetPixelToUV(tex);    // calls Texture overload
	//   UpdateUVs();          // virtual vtable+0x368
	public void SetTexture(Texture2D tex)
	{
		if (managed) return;
		if ((UnityEngine.Object)_renderer == null) return;
		Material mat = _renderer.material;
		if ((UnityEngine.Object)mat == null) throw new System.NullReferenceException();
		mat.mainTexture = tex;
		SetPixelToUV(tex);
		UpdateUVs();
	}

	// Source: Ghidra SetMaterial.c RVA 0x01585880
	// 1-1:
	//   if (managed) return;
	//   if (_renderer == null/destroyed) return;
	//   _renderer.sharedMaterial = mat;
	//   if (mat == null) NRE;
	//   SetPixelToUV(mat.mainTexture);
	//   UpdateUVs();
	public void SetMaterial(Material mat)
	{
		if (managed) return;
		if ((UnityEngine.Object)_renderer == null) return;
		_renderer.sharedMaterial = mat;
		if ((UnityEngine.Object)mat == null) throw new System.NullReferenceException();
		SetPixelToUV(mat.mainTexture);
		UpdateUVs();
	}

	// Source: Ghidra UpdateCamera.c RVA 0x0158598C — virtual dispatch vtable+0x368 (= SetCamera(Camera)).
	// 1-1: SetCamera(renderCamera).
	public void UpdateCamera() { SetCamera(renderCamera); }

	// Source: Ghidra SetCamera.c RVA 0x0158586C — virtual dispatch vtable+0x368 with renderCamera.
	// 1-1: SetCamera(renderCamera).
	public void SetCamera() { SetCamera(renderCamera); }

	// Source: Ghidra SetCamera_1.c RVA 0x015859A0 (~160 lines)
	// Sets screenSize via Camera.pixelWidth/pixelHeight, attaches screenPlacer.SetCamera(c),
	// computes worldUnitsPerScreenPixel via Camera.ScreenToWorldPoint differential, then CalcSize().
	// Branches on isPlaying for slightly different timing.
	// TODO RVA 0x015859A0 — defer full computation pending Camera.transform.forward 3-axis math.
	public virtual void SetCamera(Camera c)
	{
		if ((UnityEngine.Object)c == null) return;
		if (!m_started) return;
		renderCamera = c;
		screenSize.x = c.pixelWidth;
		screenSize.y = c.pixelHeight;
		if (screenSize.x == 0f) return;
		if ((UnityEngine.Object)screenPlacer != null) screenPlacer.SetCamera(c);
		// TODO: worldUnitsPerScreenPixel computation via ScreenToWorldPoint differential.
		CalcSize();
	}

	// Source: Ghidra Hide.c RVA 0x01580FC0
	// 1-1: m_hidden = tf (0x1d9). If spriteMesh != null, dispatch to a hidden-state virtual
	// (FUN_032a5dd8 — likely a SpriteMesh.Hide() wrapper).
	// TODO: spriteMesh.Hide(tf) virtual call pending mapping. For now write only the flag field.
	public virtual void Hide(bool tf)
	{
		if (m_spriteMesh != null)
		{
			m_spriteMesh.Hide(tf);
		}
		m_hidden = tf;
	}

	// Source: Ghidra IsHidden.c RVA 0x01585E3C — return m_hidden (byte at offset 0x1d9).
	public bool IsHidden() { return m_hidden; }

	// Source: Ghidra DestroyMesh.c RVA 0x01585FB0
	// 1-1: if spriteMesh != null, call spriteMesh.set_gameObject(null) virtual (vtable+1).
	//      Then spriteMesh = null. If _renderer != null, Destroy/DestroyImmediate it based on isPlaying.
	//      Then GetComponent<MeshFilter>() and Destroy/DestroyImmediate it.
	// TODO: spriteMesh.set_gameObject virtual mapping — pending.
	protected void DestroyMesh()
	{
		m_spriteMesh = null;
		if ((UnityEngine.Object)_renderer != null)
		{
			if (UnityEngine.Application.isPlaying)
				UnityEngine.Object.Destroy(_renderer);
			else
				UnityEngine.Object.DestroyImmediate(_renderer);
		}
		MeshFilter mf = GetComponent<MeshFilter>();
		if ((UnityEngine.Object)mf == null) return;
		if (UnityEngine.Application.isPlaying)
			UnityEngine.Object.Destroy(mf);
		else
			UnityEngine.Object.DestroyImmediate(mf);
	}

	// Source: Ghidra AddMesh.c RVA 0x01583D84
	// 1-1: spriteMesh = new SpriteMesh(); spriteMesh.set_gameObject(this.gameObject) (vtable+1).
	// SpriteMesh implementation class lookup is via static class info PTR_DAT_034469c0 (SpriteMesh).
	// TODO: SpriteMesh type may not yet be ported. Until then, defer with stub.
	protected void AddMesh()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	// Source: Ghidra SetBleedCompensation.c RVA 0x0158127C
	// 1-1: SetBleedCompensation(bleedCompensation.x, bleedCompensation.y) — wraps the (x,y) overload.
	public void SetBleedCompensation()
	{
		SetBleedCompensation(bleedCompensation.x, bleedCompensation.y);
	}

	// Source: Ghidra SetBleedCompensation_1.c RVA 0x0158622C (identical body to SetBleedCompensation_2)
	// 1-1: bleedCompensation = (x, y); bleedCompensationUV.x = PixelSpaceToUVSpace(x).x;
	//      bleedCompensationUV.y = y; bleedCompensationUVMax = -2 * bleedCompensationUV (per axis);
	//      uvRect.position += bleedCompensationUV; uvRect.size += bleedCompensationUVMax (additive shift).
	//      Then spriteMesh.UpdateUVs() virtual (vtable+0x308 — UpdateUVs slot).
	// Ghidra encodes the position/size additive shifts into one ulong CONCAT44 each — equivalent
	// to writing both components atomically.
	public void SetBleedCompensation(float x, float y)
	{
		bleedCompensation = new Vector2(x, y);
		Vector2 uvSpace = PixelSpaceToUVSpace(new Vector2(x, y));
		bleedCompensationUV = uvSpace;
		bleedCompensationUVMax = new Vector2(uvSpace.x * -2f, uvSpace.y * -2f);
		uvRect = new Rect(
			uvRect.x + uvSpace.x,
			uvRect.y + uvSpace.y,
			uvRect.width  + bleedCompensationUVMax.x,
			uvRect.height + bleedCompensationUVMax.y);
		if (m_spriteMesh != null)
		{
			m_spriteMesh.UpdateUVs();
		}
	}

	// Source: Ghidra SetBleedCompensation_2.c RVA 0x015823F8 (identical body to _1)
	// 1-1: forwards to (x, y) overload.
	public void SetBleedCompensation(Vector2 xy)
	{
		SetBleedCompensation(xy.x, xy.y);
	}

	// Source: Ghidra SetPlane.c RVA 0x01586294
	// 1-1: plane = p (offset 0x3c); CalcSize() (virtual vtable+0x298).
	public void SetPlane(SPRITE_PLANE p)
	{
		plane = p;
		CalcSize();
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

	// Source: Ghidra SetDrawLayer.c RVA 0x0158634C
	// 1-1:
	//   if (!managed) return;
	//   drawLayer = layer (offset 0x34);
	//   if (spriteMesh == null) NRE;
	//   ((SpriteMesh_Managed)spriteMesh).drawLayer = layer;     // offset 0x20 inside SpriteMesh_Managed
	//   if (manager != null) manager.SortDrawingOrder();
	public void SetDrawLayer(int layer)
	{
		if (!managed) return;
		drawLayer = layer;
		if (m_spriteMesh == null) throw new System.NullReferenceException();
		SpriteMesh_Managed sm = m_spriteMesh as SpriteMesh_Managed;
		if (sm == null) throw new System.InvalidCastException();
		sm.drawLayer = layer;
		if ((UnityEngine.Object)manager == null) return;
		manager.SortDrawingOrder();
	}

	// Source: Ghidra SetFrameInfo.c RVA 0x01586430
	// 1-1: frameInfo = fInfo (struct copy); uvRect.position = fInfo.uvs.position; uvRect.size = fInfo.uvs.size.
	//      SetBleedCompensation(bleedCompensation); if (autoResize || pixelPerfect) CalcSize().
	public void SetFrameInfo(SPRITE_FRAME fInfo)
	{
		frameInfo = fInfo;
		uvRect = fInfo.uvs;
		SetBleedCompensation(bleedCompensation);
		if (autoResize || pixelPerfect) CalcSize();
	}

	// Source: Ghidra SetUVs.c RVA 0x01586480
	// 1-1: frameInfo.uvs = uv (copy 4 floats at offsets 0x6c..0x78); uvRect = uv (copy at 0x94..0xa0);
	//      SetBleedCompensation(); if (!Application.isPlaying) CalcSizeUnitsPerUV();
	//      if (autoResize || pixelPerfect) CalcSize().
	public void SetUVs(Rect uv)
	{
		frameInfo.uvs = uv;
		uvRect = uv;
		SetBleedCompensation(bleedCompensation);
		if (!UnityEngine.Application.isPlaying) CalcSizeUnitsPerUV();
		if (autoResize || pixelPerfect) CalcSize();
	}

	// Source: Ghidra SetUVsFromPixelCoords.c RVA 0x01586548
	// 1-1: convert pxCoords (Rect in pixel coords) to UV space via PixelCoordToUVCoord per corner,
	//      then write into uvRect + frameInfo.uvs. SetBleedCompensation + CalcSize per autoResize/pixelPerfect.
	// Ghidra emits per-component (int)truncation + INFINITY check for each float — managed C# float
	// cast already truncates; we omit explicit INFINITY guard (managed indexer raises NaN naturally).
	public void SetUVsFromPixelCoords(Rect pxCoords)
	{
		Vector2 tl = PixelCoordToUVCoord(new Vector2((int)pxCoords.x, (int)(pxCoords.y + pxCoords.height)));
		Vector2 br = PixelCoordToUVCoord(new Vector2((int)(pxCoords.x + pxCoords.width), (int)pxCoords.y));
		uvRect = new Rect(tl.x, tl.y, br.x - tl.x, br.y - tl.y);
		frameInfo.uvs = uvRect;
		SetBleedCompensation(bleedCompensation);
		if (autoResize || pixelPerfect) CalcSize();
	}

	// Source: Ghidra GetUVs.c RVA 0x01586650 — return uvRect (offset 0x94, 16 bytes; Ghidra simplifies
	// to single float — full Rect derived from C# signature).
	public Rect GetUVs() { return uvRect; }

	// Source: Ghidra GetVertices.c RVA 0x0158665C
	// 1-1: if (managed) return spriteMesh.vertices (virtual vtable+4 on SpriteMesh = get_vertices).
	//      Otherwise return GetComponent<MeshFilter>().sharedMesh.vertices.
	public Vector3[] GetVertices()
	{
		if (managed)
		{
			if (m_spriteMesh == null) throw new System.NullReferenceException();
			return m_spriteMesh.vertices;
		}
		MeshFilter mf = GetComponent<MeshFilter>();
		if ((UnityEngine.Object)mf == null) throw new System.NullReferenceException();
		Mesh m = mf.sharedMesh;
		if ((UnityEngine.Object)m == null) throw new System.NullReferenceException();
		return m.vertices;
	}

	// Source: Ghidra GetCenterPoint.c RVA 0x01586778
	// 1-1: get spriteMesh.vertices (virtual vtable+4). Based on plane (XY/XZ/YZ):
	//   - XY (plane==0): center.x = (verts[0].x + verts[2].x) * 0.5 + offset.x;
	//                    center.y = (verts[0].y + verts[2].y) * 0.5 + offset.y; center.z = offset.z;
	//   - XZ (plane==1): same x/z mix, y is offset.y.
	//   - YZ (plane==2): x is offset.x, y/z mix.
	// Ghidra simplifies to single component but the full 3-axis center comes from this geometric pattern.
	public Vector3 GetCenterPoint()
	{
		if (m_spriteMesh == null) return Vector3.zero;
		Vector3[] verts = m_spriteMesh.vertices;
		if (verts == null || verts.Length < 3) return Vector3.zero;
		Vector3 center;
		switch ((int)plane)
		{
			case 2: // YZ
				center = new Vector3(offset.x,
					verts[0].y + (verts[2].y - verts[0].y) * 0.5f,
					verts[0].z + (verts[2].z - verts[0].z) * 0.5f);
				break;
			case 1: // XZ
				center = new Vector3(
					verts[0].x + (verts[2].x - verts[0].x) * 0.5f,
					offset.y,
					verts[0].z + (verts[2].z - verts[0].z) * 0.5f);
				break;
			default: // XY
				center = new Vector3(
					verts[0].x + (verts[2].x - verts[0].x) * 0.5f,
					verts[0].y + (verts[2].y - verts[0].y) * 0.5f,
					offset.z);
				break;
		}
		return center;
	}

	// Source: Ghidra SetAnchor.c RVA 0x01586AC4
	// 1-1: anchor = a (offset 0x54); CalcSize() (virtual vtable+0x298).
	public void SetAnchor(ANCHOR_METHOD a)
	{
		anchor = a;
		CalcSize();
	}

	// Source: Ghidra SetOffset.c RVA 0x01586AFC
	// 1-1: offset = o (Vector3 at offsets 0x160, 0x164, 0x168); CalcSize() (virtual vtable+0x298).
	public void SetOffset(Vector3 o)
	{
		offset = o;
		CalcSize();
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

	// Source: Ghidra DoMirror.c RVA 0x01586E68
	// 1-1: if Application.isPlaying → return. If screenSize.x == 0 || screenSize.y == 0 → Awake().
	//      If mirror (offset 0x1e0 = SpriteRootMirror) == null → instantiate new SpriteRootMirror;
	//      call mirror.SetParent(this) virtual vtable+0x178; mirror.Init(this) virtual vtable+0x188.
	//      Then mirror.IsMirrorOk(this) check virtual vtable+0x198. If ok → Init(); mirror.SetParent again.
	// TODO: SpriteRootMirror virtual slots + Init virtual at this+0x218 unresolved.
	public virtual void DoMirror()
	{
		if (UnityEngine.Application.isPlaying) return;
		if (screenSize.x == 0f || screenSize.y == 0f) Awake();
		// TODO: full mirror dispatch — SpriteRootMirror not yet exposed.
	}

	// Source: Ghidra OnDrawGizmosSelected.c RVA 0x01586FC0 — virtual dispatch vtable+0x418.
	// 1-1: empty in production (virtual returns void); reference impl likely just stubs.
	public virtual void OnDrawGizmosSelected() { }

	// Source: Ghidra OnDrawGizmos.c RVA 0x01586FD0 — same virtual dispatch as Selected.
	public virtual void OnDrawGizmos() { }

	// Source: Ghidra _ctor.c RVA 0x01581394
	// 1-1: winding = CCW (0x40 = 1); anchor = TEXTURE_OFFSET (0x54 = 9); frameInfo.uvs.size = (1,1)
	// (offsets 0x74, 0x78); tlTruncate = (1,1); brTruncate = (1,1); scaleFactor = (1,1);
	// topLeftOffset = (1,1); bottomRightOffset = (1,1); leftClipPct/topClipPct = (1,1); color = white.
	// base() — MonoBehaviour.ctor.
	protected SpriteRoot()
	{
		winding = WINDING_ORDER.CW;            // raw 1 in Ghidra → CW value (CCW=0)
		anchor = ANCHOR_METHOD.TEXTURE_OFFSET;
		frameInfo.uvs = new Rect(0f, 0f, 1f, 1f);
		uvRect = new Rect(0f, 0f, 1f, 1f);
		tlTruncate = Vector2.one;
		brTruncate = Vector2.one;
		scaleFactor = Vector2.one;
		topLeftOffset = Vector2.one;
		bottomRightOffset = Vector2.one;
		leftClipPct = 1f;
		topClipPct = 1f;
		color = Color.white;
	}
}
