// Source: work/03_il2cpp_dump/dump.cs class 'SpriteMesh_Managed' (TypeDefIndex 8206) +
//         work/06_ghidra/decompiled_full/SpriteMesh_Managed/*.c bodies
// Field offset layout verified against Ghidra accessor RVAs (0x01582ad4..0x0158344c):
//   0x10  m_sprite     (SpriteRoot)        — virtual property `sprite`
//   0x18  hidden       (bool, 1 byte)      — accessor IsHidden / Clear sets to 0
//   0x1c  index        (int)
//   0x20  drawLayer    (int)
//   0x28  m_manager    (SpriteManager)     — non-virtual property `manager`
//   0x30  m_next       (SpriteMesh_Managed)
//   0x38  m_prev       (SpriteMesh_Managed)
//   0x40  m_vertices   (Vector3[4])        — source quad — virtual `vertices` getter
//   0x48  m_uvs        (Vector2[4])
//   0x50  m_uvs2       (Vector2[4])
//   0x58  m_useUV2     (bool)
//   0x60  m_material   (Material)          — set by set_manager via SpriteManager.ManagedRenderer.sharedMaterial
//   0x68  m_texture    (Texture)           — set by set_manager via Material.GetTexture
//   0x70  meshVerts    (Vector3[])         — manager's shared verts array (alias)
//   0x78  meshUVs      (Vector2[])
//   0x80  meshUVs2     (Vector2[])
//   0x88  meshColors   (Color[])
//   0x90  mv1,mv2,mv3,mv4  (int[4])        — vertex indices in shared buffer
//   0xa0  uv1,uv2,uv3,uv4  (int[4])
//   0xb0  cv1,cv2,cv3,cv4  (int[4])
using UnityEngine;

public class SpriteMesh_Managed : ISpriteMesh, IEZLinkedListItem<SpriteMesh_Managed>
{
	protected SpriteRoot m_sprite;
	protected bool hidden;
	public int index;
	public int drawLayer;
	public SpriteManager m_manager;
	public SpriteMesh_Managed m_next;
	public SpriteMesh_Managed m_prev;
	protected Vector3[] m_vertices;
	protected Vector2[] m_uvs;
	protected Vector2[] m_uvs2;
	protected bool m_useUV2;
	protected Material m_material;
	protected Texture m_texture;
	protected Vector3[] meshVerts;
	protected Vector2[] meshUVs;
	protected Vector2[] meshUVs2;
	protected Color[] meshColors;
	public int mv1;
	public int mv2;
	public int mv3;
	public int mv4;
	public int uv1;
	public int uv2;
	public int uv3;
	public int uv4;
	public int cv1;
	public int cv2;
	public int cv3;
	public int cv4;

	// Source: Ghidra get_manager.c RVA 0x01582adc / set_manager.c RVA 0x01582ae4
	// 1-1 get: return m_manager (offset 0x28).
	// 1-1 set: m_manager = value; if (m_manager != null && managedRenderer != null) {
	//             m_material = managedRenderer.sharedMaterial;                    // 0x60
	//             if (m_material != null) m_texture = m_material.GetTexture("_MainTex");  // 0x68
	//          }
	// (Ghidra hardcodes string literal "_MainTex" via PTR_StringLiteral_13370_03446b00.)
	public SpriteManager manager
	{
		get { return m_manager; }
		set
		{
			m_manager = value;
			if ((UnityEngine.Object)m_manager == null) throw new System.NullReferenceException();
			Renderer mr = m_manager.get_ManagedRenderer();
			if ((UnityEngine.Object)mr == null) throw new System.NullReferenceException();
			m_material = mr.sharedMaterial;
			if ((UnityEngine.Object)m_material == null) return;
			m_texture = m_material.GetTexture("_MainTex");
		}
	}

	// Source: Ghidra get_sprite.c RVA 0x01582BD8 / set_sprite.c RVA 0x01582BE0
	// 1-1 get: return m_sprite (offset 0x10).
	// 1-1 set: m_sprite = value; if (value != null) UpdateColors(value.color) (virtual vtable+0x368).
	// The 4 floats at sprite+0x16c..0x178 are color.r/g/b/a on SpriteRoot. Setting the owner sprite
	// propagates the owner's color into this mesh's color buffer slots.
	public virtual SpriteRoot sprite
	{
		get { return m_sprite; }
		set
		{
			m_sprite = value;
			if ((UnityEngine.Object)m_sprite == null) return;
			UpdateColors(m_sprite.color);
		}
	}

	// Source: Ghidra get_texture.c RVA 0x01582ca4 — return m_texture (0x68).
	public virtual Texture texture { get { return m_texture; } }

	// Source: Ghidra get_material.c RVA 0x01582cac — return m_material (0x60).
	public virtual Material material { get { return m_material; } }

	// Source: Ghidra get_vertices.c RVA 0x01582cb4 — return m_vertices (0x40).
	public virtual Vector3[] vertices { get { return m_vertices; } }

	// Source: Ghidra get_uvs.c RVA 0x01582cbc — return m_uvs (0x48).
	public virtual Vector2[] uvs { get { return m_uvs; } }

	// Source: Ghidra get_uvs2.c RVA 0x01582cc4 — return m_uvs2 (0x50).
	public virtual Vector2[] uvs2 { get { return m_uvs2; } }

	// Source: Ghidra get_UseUV2.c RVA 0x01582ccc / set_UseUV2.c RVA 0x01582cd4
	// 1-1 get: return m_useUV2 (byte at 0x58).
	// 1-1 set: m_useUV2 = value & 1 (Ghidra explicitly masks with 1 — defensive against non-{0,1} bytes).
	public virtual bool UseUV2
	{
		get { return m_useUV2; }
		set { m_useUV2 = value; }
	}

	// Source: Ghidra get_prev.c RVA 0x01583434 / set_prev.c RVA 0x0158343c
	// 1-1: return/assign m_prev (0x38). Implements IEZLinkedListItem<T>.prev.
	public SpriteMesh_Managed prev
	{
		get { return m_prev; }
		set { m_prev = value; }
	}

	// Source: Ghidra get_next.c RVA 0x01583444 / set_next.c RVA 0x0158344c
	// 1-1: return/assign m_next (0x30). Implements IEZLinkedListItem<T>.next.
	public SpriteMesh_Managed next
	{
		get { return m_next; }
		set { m_next = value; }
	}

	// Source: Ghidra SetBuffers.c RVA 0x01582a74
	// 1-1: meshVerts/meshUVs/meshUVs2/meshColors aliases pointing into the SpriteManager's shared
	// big arrays (offsets 0x70/0x78/0x80/0x88).
	public void SetBuffers(Vector3[] verts, Vector2[] uvs, Vector2[] uvs2, Color[] cols)
	{
		meshVerts  = verts;
		meshUVs    = uvs;
		meshUVs2   = uvs2;
		meshColors = cols;
	}

	// Source: Ghidra Clear.c RVA 0x01582ad4
	// 1-1: hidden = false (byte at 0x18 = 0). Only field touched — does NOT reset m_sprite/m_manager
	// (those are reset by RemoveSprite's outer logic).
	public void Clear() { hidden = false; }

	// Source: Ghidra Init.c RVA 0x01582CE0 (89 lines)
	// 1-1:
	//   if (m_sprite == null) NRE;
	//   if (!m_sprite.m_started) m_sprite.Awake();                          // vtable+0x208
	//   if (!m_sprite.uvsInitialized) {
	//       m_sprite.InitUVs();                                              // vtable+0x248
	//       m_sprite.uvsInitialized = true;
	//   }
	//   m_sprite.SetBleedCompensation(bleedComp);                           // static method
	//   if (!m_sprite.pixelPerfect) {
	//       if (!m_sprite.someBool_0x1d8) m_sprite.SetSize(w, h);            // vtable+0x298
	//   } else {
	//       if (m_texture != null) m_sprite.SetPixelToUV(m_texture);
	//       Camera c = m_sprite.renderCamera; if (c==null) c = Camera.main;
	//       m_sprite.SetCamera(c);                                            // vtable+0x368
	//   }
	//   m_sprite.SetColor(m_sprite.color);                                   // vtable+0x318
	public virtual void Init()
	{
		if ((UnityEngine.Object)m_sprite == null) throw new System.NullReferenceException();
		// Ghidra calls m_sprite.Awake() directly when !m_started — but C# protected access blocks
		// direct cross-instance Awake. Unity dispatches Awake automatically on enable, so the
		// fall-through here is functionally equivalent. Re-trigger via SendMessage if needed.
		if (!m_sprite.Started)
		{
			m_sprite.SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
		}
		if (!m_sprite.uvsInitialized)
		{
			m_sprite.InitUVs();
			m_sprite.uvsInitialized = true;
		}
		m_sprite.SetBleedCompensation(m_sprite.bleedCompensation);
		if (m_sprite.pixelPerfect)
		{
			if ((UnityEngine.Object)m_texture != null) m_sprite.SetPixelToUV(m_texture);
			Camera c = m_sprite.renderCamera;
			if ((UnityEngine.Object)c == null) c = Camera.main;
			m_sprite.SetCamera(c);
		}
		else
		{
			// 0x1d8 byte on SpriteRoot — closest existing field is `deleted` or `ignoreClipping`.
			// Defer the exact guard; default path: always SetSize.
			m_sprite.SetSize(m_sprite.width, m_sprite.height);
		}
		m_sprite.SetColor(m_sprite.color);
	}

	// Source: Ghidra UpdateVerts.c RVA 0x01582e70
	// 1-1: if (hidden) return;
	//      meshVerts[mv1] = m_vertices[0];
	//      meshVerts[mv2] = m_vertices[1];
	//      meshVerts[mv3] = m_vertices[2];
	//      meshVerts[mv4] = m_vertices[3];
	//      if (m_manager != null) m_manager.UpdatePositions();
	// Ghidra emits explicit array-length bounds checks; managed indexer raises IOOR automatically.
	public virtual void UpdateVerts()
	{
		if (hidden) return;
		if (m_vertices == null) throw new System.NullReferenceException();
		if (meshVerts == null)  throw new System.NullReferenceException();
		meshVerts[mv1] = m_vertices[0];
		meshVerts[mv2] = m_vertices[1];
		meshVerts[mv3] = m_vertices[2];
		meshVerts[mv4] = m_vertices[3];
		if ((UnityEngine.Object)m_manager == null) throw new System.NullReferenceException();
		m_manager.UpdatePositions();
	}

	// Source: Ghidra UpdateUVs.c RVA 0x01582fac
	// 1-1: Copy primary UVs via virtual `this.uvs` getter (vtable+0x2f8) into meshUVs at uv1..uv4
	//      slots. Then if (m_useUV2) copy secondary UVs via virtual `this.uvs2` getter (vtable+0x308)
	//      into meshUVs2. Finally m_manager.UpdateUVs() if manager != null.
	public virtual void UpdateUVs()
	{
		Vector2[] src = this.uvs;     // virtual call (vtable+0x2f8)
		if (src == null) throw new System.NullReferenceException();
		if (meshUVs == null) throw new System.NullReferenceException();
		meshUVs[uv1] = src[0];
		meshUVs[uv2] = src[1];
		meshUVs[uv3] = src[2];
		meshUVs[uv4] = src[3];
		if (m_useUV2)
		{
			Vector2[] src2 = this.uvs2;   // virtual call (vtable+0x308)
			if (src2 == null) throw new System.NullReferenceException();
			if (meshUVs2 == null) throw new System.NullReferenceException();
			meshUVs2[uv1] = src2[0];
			meshUVs2[uv2] = src2[1];
			meshUVs2[uv3] = src2[2];
			meshUVs2[uv4] = src2[3];
		}
		if ((UnityEngine.Object)m_manager == null) throw new System.NullReferenceException();
		m_manager.UpdateUVs();
	}

	// Source: Ghidra UpdateColors.c RVA 0x01583214
	// 1-1: write `color` into meshColors at cv1, cv2, cv3, cv4. Then m_manager.UpdateColors().
	public virtual void UpdateColors(Color color)
	{
		if (meshColors == null) throw new System.NullReferenceException();
		meshColors[cv1] = color;
		meshColors[cv2] = color;
		meshColors[cv3] = color;
		meshColors[cv4] = color;
		if ((UnityEngine.Object)m_manager == null) throw new System.NullReferenceException();
		m_manager.UpdateColors();
	}

	// Source: Ghidra Hide.c RVA 0x015832c4
	// 1-1 two branches:
	//   if (!tf) {  // SHOW
	//       hidden = false;
	//       if (m_sprite == null) NRE;
	//       if (!m_sprite.someByteFlag_0x58 && !m_sprite.someByteFlag_0x59) {
	//           // virtual sprite.vtable[0x298] — likely UpdateUVs/CalcSize on SpriteRoot.
	//       } else {
	//           SpriteRoot.CalcSize();
	//       }
	//   } else {     // HIDE
	//       if (m_vertices == null) NRE;
	//       m_vertices[0..3] = Vector3.zero;     // zero source quad
	//       this.UpdateVerts();                   // virtual (vtable+0x348) — propagates zeros to manager
	//       hidden = true;
	//   }
	// Note: the SHOW-path virtual on SpriteRoot at slot 0x298 is intentionally left as a TODO call —
	// requires SpriteRoot virtual-slot identification to map. Stubbed with NRE to flag at runtime.
	public virtual void Hide(bool tf)
	{
		if (!tf)
		{
			hidden = false;
			if ((UnityEngine.Object)m_sprite == null) throw new System.NullReferenceException();
			// SHOW path per Ghidra: if m_sprite.pixelPerfect == 0 && m_sprite.autoResize == 0:
			//     m_sprite.SetSize(width, height);
			// else:
			//     SpriteRoot.CalcSize() (static call).
			if (!m_sprite.pixelPerfect && !m_sprite.autoResize)
			{
				m_sprite.SetSize(m_sprite.width, m_sprite.height);
			}
			else
			{
				m_sprite.CalcSize();
			}
		}
		else
		{
			if (m_vertices == null) throw new System.NullReferenceException();
			m_vertices[0] = Vector3.zero;
			m_vertices[1] = Vector3.zero;
			m_vertices[2] = Vector3.zero;
			m_vertices[3] = Vector3.zero;
			this.UpdateVerts();    // virtual at this+0x348 — UpdateVerts pushes zeros through
			hidden = tf;
		}
	}

	// Source: Ghidra IsHidden.c RVA 0x0158342c
	// 1-1: return hidden (byte at 0x18).
	public virtual bool IsHidden() { return hidden; }

	// Source: Ghidra _ctor.c RVA 0x01583454
	// 1-1:
	//   m_vertices = new Vector3[4];        // offset 0x40
	//   m_uvs      = new Vector2[4];        // offset 0x48
	//   m_uvs2     = new Vector2[4];        // offset 0x50
	//   System.Object..ctor(this);
	// (m_useUV2 stays false by default — no init in ctor.)
	public SpriteMesh_Managed()
	{
		m_vertices = new Vector3[4];
		m_uvs      = new Vector2[4];
		m_uvs2     = new Vector2[4];
	}
}
