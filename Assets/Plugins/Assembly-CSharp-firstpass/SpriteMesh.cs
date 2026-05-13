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

	public virtual SpriteRoot sprite
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

	public virtual Texture texture
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual Material material
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

	public virtual Vector3[] vertices
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual Vector2[] uvs
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual Vector2[] uvs2
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public virtual bool UseUV2
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

	public virtual Mesh mesh
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

	public virtual void Init()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	protected void CreateMesh()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void UpdateVerts()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void UpdateUVs()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void UpdateColors(Color color)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Hide(bool tf)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual bool IsHidden()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void SetPersistent()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void SetWindingOrder(SpriteRoot.WINDING_ORDER winding)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public SpriteMesh()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
