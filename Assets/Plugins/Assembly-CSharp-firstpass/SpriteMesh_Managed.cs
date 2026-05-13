using Cpp2IlInjected;
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

	public SpriteManager manager
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

	public SpriteMesh_Managed prev
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

	public SpriteMesh_Managed next
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

	public void SetBuffers(Vector3[] verts, Vector2[] uvs, Vector2[] uvs2, Color[] cols)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void Clear()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public virtual void Init()
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

	public SpriteMesh_Managed()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
