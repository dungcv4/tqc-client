// Source: work/03_il2cpp_dump/dump.cs class 'SpriteMesh' + work/06_ghidra/decompiled_full/SpriteMesh/*.c bodies.
// Field offset layout verified from Ghidra accessor RVAs (0x015816EC..0x01582954):
//   0x10 m_sprite (SpriteRoot)     0x18 meshFilter (MeshFilter)
//   0x20 meshRenderer (Renderer)   0x28 m_mesh (Mesh)
//   0x30 m_texture (Texture)       0x38 m_vertices (Vector3[4])
//   0x40 m_colors (Color[4])       0x48 m_uvs (Vector2[4])
//   0x50 m_uvs2 (Vector2[4])       0x58 m_faces (int[6])
//   0x60 m_useUV2 (bool)
using Cpp2IlInjected;
using UnityEngine;

public class SpriteMesh : ISpriteMesh
{
	protected SpriteRoot m_sprite;

	protected MeshFilter meshFilter;

	protected MeshRenderer meshRenderer;

	protected Mesh m_mesh;

	protected Texture m_texture;

	protected Vector3[] m_vertices;

	protected Color[] m_colors;

	protected Vector2[] m_uvs;

	protected Vector2[] m_uvs2;

	protected int[] m_faces;

	protected bool m_useUV2;

	// Source: Ghidra get_sprite.c RVA 0x015816EC — return m_sprite (offset 0x10).
	// Source: Ghidra set_sprite.c RVA 0x015816F4
	// 1-1: m_sprite = value; if (value == null/destroyed) return.
	//      if (m_sprite.spriteMesh != this) m_sprite.spriteMesh = this;   // SpriteRoot.set_spriteMesh
	//      meshFilter = m_sprite.gameObject.GetComponent<MeshFilter>();
	//      if (meshFilter == null) meshFilter = m_sprite.gameObject.AddComponent<MeshFilter>();
	//      meshRenderer = m_sprite.gameObject.GetComponent<MeshRenderer>();
	//      if (meshRenderer == null) meshRenderer = m_sprite.gameObject.AddComponent<MeshRenderer>();
	//      m_sprite._renderer = meshRenderer;
	//      m_mesh = meshFilter.sharedMesh;
	//      if (meshRenderer.sharedMaterial != null) {
	//          m_texture = meshRenderer.sharedMaterial.GetTexture("_MainTex");
	//      }
	public virtual SpriteRoot sprite
	{
		get { return m_sprite; }
		set
		{
			m_sprite = value;
			if ((UnityEngine.Object)m_sprite == null) return;
			if (m_sprite.spriteMesh != this)
			{
				m_sprite.spriteMesh = this;
			}
			GameObject go = m_sprite.gameObject;
			if ((UnityEngine.Object)go == null) throw new System.NullReferenceException();
			meshFilter = go.GetComponent<MeshFilter>();
			if ((UnityEngine.Object)meshFilter == null)
			{
				meshFilter = go.AddComponent<MeshFilter>();
			}
			meshRenderer = go.GetComponent<MeshRenderer>();
			if ((UnityEngine.Object)meshRenderer == null)
			{
				meshRenderer = go.AddComponent<MeshRenderer>();
			}
			m_sprite._renderer = meshRenderer;
			m_mesh = meshFilter.sharedMesh;
			if ((UnityEngine.Object)meshRenderer.sharedMaterial == null) return;
			m_texture = meshRenderer.sharedMaterial.GetTexture("_MainTex");
		}
	}

	// Source: Ghidra get_texture.c RVA 0x01581DA0 — return m_texture (offset 0x30).
	public virtual Texture texture { get { return m_texture; } }

	// Source: Ghidra get_material.c RVA 0x01581DA8 — if (meshRenderer == null) NRE; return meshRenderer.sharedMaterial.
	// Source: Ghidra set_material.c RVA 0x01581DC4
	// 1-1 set: meshRenderer.sharedMaterial = value; m_texture = sharedMaterial.mainTexture;
	//          if (m_sprite != null && m_texture != null) m_sprite.SetPixelToUV(m_texture).
	public virtual Material material
	{
		get
		{
			if ((UnityEngine.Object)meshRenderer == null) throw new System.NullReferenceException();
			return meshRenderer.sharedMaterial;
		}
		set
		{
			if ((UnityEngine.Object)meshRenderer == null) throw new System.NullReferenceException();
			meshRenderer.sharedMaterial = value;
			Material mat = meshRenderer.sharedMaterial;
			if ((UnityEngine.Object)mat == null) throw new System.NullReferenceException();
			m_texture = mat.mainTexture;
			if ((UnityEngine.Object)m_sprite == null) return;
			if ((UnityEngine.Object)m_texture == null) return;
			m_sprite.SetPixelToUV(m_texture);
		}
	}

	// Source: Ghidra get_vertices.c RVA 0x01581F7C — return m_vertices (offset 0x38).
	public virtual Vector3[] vertices { get { return m_vertices; } }

	// Source: Ghidra get_uvs.c RVA 0x01581F84 — return m_uvs (offset 0x48).
	public virtual Vector2[] uvs { get { return m_uvs; } }

	// Source: Ghidra get_uvs2.c RVA 0x01581F8C — return m_uvs2 (offset 0x50).
	public virtual Vector2[] uvs2 { get { return m_uvs2; } }

	// Source: Ghidra get_UseUV2.c RVA 0x01581F94 / set_UseUV2.c RVA 0x01581F9C
	// 1-1: return/assign m_useUV2 (byte at 0x60, masked with 1).
	public virtual bool UseUV2
	{
		get { return m_useUV2; }
		set { m_useUV2 = value; }
	}

	// Source: Ghidra get_mesh.c RVA 0x01581FA8
	// 1-1: if (m_mesh == null) CreateMesh(); return m_mesh.
	// Source: Ghidra set_mesh.c RVA 0x0158210C — m_mesh = value (offset 0x28).
	public virtual Mesh mesh
	{
		get
		{
			if ((UnityEngine.Object)m_mesh == null) CreateMesh();
			return m_mesh;
		}
		set { m_mesh = value; }
	}

	// Source: Ghidra Init.c RVA 0x01582114
	// 1-1:
	//   if (m_mesh == null) CreateMesh();
	//   m_mesh.Clear(); m_mesh.vertices = m_vertices; m_mesh.uv = m_uvs;
	//   m_mesh.colors = m_colors; m_mesh.triangles = m_faces;
	//   if (m_sprite == null) NRE;
	//   SetWindingOrder(m_sprite.winding);     // vtable+0x388
	//   if (!m_sprite.uvsInitialized) { m_sprite.InitUVs(); m_sprite.uvsInitialized = true; }
	//   SpriteRoot.SetBleedCompensation(m_sprite, bleed.x, bleed.y);
	//   if (m_sprite.pixelPerfect) {
	//       if (m_texture == null && meshRenderer != null && meshRenderer.sharedMaterial != null)
	//           m_texture = meshRenderer.sharedMaterial.GetTexture("_MainTex");
	//       if (m_texture != null) m_sprite.SetPixelToUV(m_texture);
	//       Camera c = m_sprite.renderCamera; if (c == null) c = Camera.main;
	//       m_sprite.SetCamera(c);
	//   } else if (!m_sprite.<flag@0x1d8>) {
	//       m_sprite.SetSize(m_sprite.width, m_sprite.height);
	//   }
	//   m_mesh.RecalculateNormals();
	//   m_sprite.SetColor(m_sprite.color);
	public virtual void Init()
	{
		if ((UnityEngine.Object)m_mesh == null) CreateMesh();
		if ((UnityEngine.Object)m_mesh == null) throw new System.NullReferenceException();
		m_mesh.Clear();
		m_mesh.vertices  = m_vertices;
		m_mesh.uv        = m_uvs;
		m_mesh.colors    = m_colors;
		m_mesh.triangles = m_faces;
		if ((UnityEngine.Object)m_sprite == null) throw new System.NullReferenceException();
		SetWindingOrder(m_sprite.winding);
		if (!m_sprite.uvsInitialized)
		{
			m_sprite.InitUVs();
			m_sprite.uvsInitialized = true;
		}
		m_sprite.SetBleedCompensation(m_sprite.bleedCompensation.x, m_sprite.bleedCompensation.y);
		if (m_sprite.pixelPerfect)
		{
			if ((UnityEngine.Object)m_texture == null && (UnityEngine.Object)meshRenderer != null)
			{
				Material mat = meshRenderer.sharedMaterial;
				if ((UnityEngine.Object)mat != null)
				{
					m_texture = mat.GetTexture("_MainTex");
				}
			}
			if ((UnityEngine.Object)m_texture != null) m_sprite.SetPixelToUV(m_texture);
			Camera c = m_sprite.renderCamera;
			if ((UnityEngine.Object)c == null) c = Camera.main;
			m_sprite.SetCamera(c);
		}
		else
		{
			// 0x1d8 byte on SpriteRoot — unmapped field; fall through to SetSize (matches default Ghidra path).
			m_sprite.SetSize(m_sprite.width, m_sprite.height);
		}
		m_mesh.RecalculateNormals();
		m_sprite.SetColor(m_sprite.color);
	}

	// Source: Ghidra CreateMesh.c RVA 0x0158201C
	// 1-1: Mesh m = new Mesh(); if (meshFilter != null) meshFilter.sharedMesh = m;
	//      m_mesh = meshFilter.sharedMesh; m_mesh.MarkDynamic();
	//      if (m_sprite != null && m_sprite.persistent) DontDestroyOnLoad(m_mesh).
	protected void CreateMesh()
	{
		Mesh m = new Mesh();
		if ((UnityEngine.Object)meshFilter == null) throw new System.NullReferenceException();
		meshFilter.sharedMesh = m;
		m_mesh = meshFilter.sharedMesh;
		if ((UnityEngine.Object)m_mesh == null) throw new System.NullReferenceException();
		m_mesh.MarkDynamic();
		if ((UnityEngine.Object)m_sprite == null) throw new System.NullReferenceException();
		if (!m_sprite.persistent) return;
		UnityEngine.Object.DontDestroyOnLoad(m_mesh);
	}

	// Source: Ghidra UpdateVerts.c RVA 0x01582454
	// 1-1: if (m_mesh == null) CreateMesh(); m_mesh.vertices = m_vertices; m_mesh.RecalculateBounds().
	public virtual void UpdateVerts()
	{
		if ((UnityEngine.Object)m_mesh == null) CreateMesh();
		if ((UnityEngine.Object)m_mesh == null) throw new System.NullReferenceException();
		m_mesh.vertices = m_vertices;
		m_mesh.RecalculateBounds();
	}

	// Source: Ghidra UpdateUVs.c RVA 0x015824E8
	// 1-1: if (m_mesh == null) return; m_mesh.uv = m_uvs; if (m_useUV2) m_mesh.uv2 = m_uvs2.
	public virtual void UpdateUVs()
	{
		if ((UnityEngine.Object)m_mesh == null) return;
		m_mesh.uv = m_uvs;
		if (!m_useUV2) return;
		m_mesh.uv2 = m_uvs2;
	}

	// Source: Ghidra UpdateColors.c RVA 0x0158258C
	// 1-1: m_colors[0..3] = color; if (m_mesh != null) m_mesh.colors = m_colors.
	public virtual void UpdateColors(Color color)
	{
		if (m_colors == null || m_colors.Length < 4) throw new System.NullReferenceException();
		m_colors[0] = color;
		m_colors[1] = color;
		m_colors[2] = color;
		m_colors[3] = color;
		if ((UnityEngine.Object)m_mesh == null) return;
		m_mesh.colors = m_colors;
	}

	// Source: Ghidra Hide.c RVA 0x015826AC
	// 1-1: if (meshRenderer == null) return; meshRenderer.enabled = !tf.
	public virtual void Hide(bool tf)
	{
		if ((UnityEngine.Object)meshRenderer == null) return;
		meshRenderer.enabled = !tf;
	}

	// Source: Ghidra IsHidden.c RVA 0x01582748
	// 1-1: if (meshRenderer == null) return true; else return !meshRenderer.enabled.
	public virtual bool IsHidden()
	{
		if ((UnityEngine.Object)meshRenderer == null) return true;
		return !meshRenderer.enabled;
	}

	// Source: Ghidra SetPersistent.c RVA 0x015827D4
	// 1-1: if (m_mesh != null) DontDestroyOnLoad(m_mesh).
	public void SetPersistent()
	{
		if ((UnityEngine.Object)m_mesh == null) return;
		UnityEngine.Object.DontDestroyOnLoad(m_mesh);
	}

	// Source: Ghidra SetWindingOrder.c RVA 0x01582864
	// 1-1: write m_faces[0..5] triangle index sequence per winding, then m_mesh.triangles = m_faces.
	// Ghidra simplified to CW-only path (constants [0, 3, 1, 3, 2, 1]). CCW path is implied by the
	// `winding` parameter; reconstructed from SpriteManager.SortDrawingOrder CCW logic
	// (m_faces[mv1, mv2, mv4, mv4, mv2, mv3] = unit-quad-CCW = [0, 1, 3, 3, 1, 2]).
	public virtual void SetWindingOrder(SpriteRoot.WINDING_ORDER winding)
	{
		if (m_faces == null) throw new System.NullReferenceException();
		if (winding == SpriteRoot.WINDING_ORDER.CCW)
		{
			m_faces[0] = 0; m_faces[1] = 1; m_faces[2] = 3;
			m_faces[3] = 3; m_faces[4] = 1; m_faces[5] = 2;
		}
		else
		{
			m_faces[0] = 0; m_faces[1] = 3; m_faces[2] = 1;
			m_faces[3] = 3; m_faces[4] = 2; m_faces[5] = 1;
		}
		if ((UnityEngine.Object)m_mesh == null) return;
		m_mesh.triangles = m_faces;
	}

	// Source: Ghidra _ctor.c RVA 0x01582954
	// 1-1: m_vertices = new Vector3[4]; m_colors = new Color[4]; m_uvs = new Vector2[4];
	//      m_uvs2 = new Vector2[4]; m_faces = new int[6]; base() = System.Object..ctor.
	public SpriteMesh()
	{
		m_vertices = new Vector3[4];
		m_colors   = new Color[4];
		m_uvs      = new Vector2[4];
		m_uvs2     = new Vector2[4];
		m_faces    = new int[6];
	}
}
