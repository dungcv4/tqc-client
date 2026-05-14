// Source: Ghidra work/06_ghidra/decompiled_full/UIParticleSystem/ — renders ParticleSystem particles via Canvas (UI overlay).

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class UIParticleSystem : MaskableGraphic
{
	public Texture particleTexture;
	public Sprite particleSprite;

	private Transform _transform;
	private ParticleSystem _particleSystem;
	private ParticleSystem.Particle[] _particles;
	private UIVertex[] _quad;
	private Vector4 _uv;
	private ParticleSystem.TextureSheetAnimationModule _textureSheetAnimation;
	private int _textureSheetAnimationFrames;
	private Vector2 _textureSheedAnimationFrameSize;

	public override Texture mainTexture
	{
		get
		{
			if (particleSprite != null) return particleSprite.texture;
			if (particleTexture != null) return particleTexture;
			return base.mainTexture;
		}
	}

	protected bool Initialize()
	{
		if (_transform == null) _transform = transform;
		if (_particleSystem == null)
		{
			_particleSystem = GetComponent<ParticleSystem>();
			if (_particleSystem == null) return false;
			var main = _particleSystem.main;
			main.scalingMode = ParticleSystemScalingMode.Hierarchy;
			_quad = new UIVertex[4];
			if (particleSprite != null)
			{
				_uv = UnityEngine.Sprites.DataUtility.GetOuterUV(particleSprite);
			}
			else
			{
				_uv = new Vector4(0, 0, 1, 1);
			}
			_particles = null;
		}
		if (_particles == null)
		{
			int maxParticles = _particleSystem.main.maxParticles;
			_particles = new ParticleSystem.Particle[maxParticles];
		}
		return true;
	}

	protected override void Awake()
	{
		base.Awake();
		Initialize();
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		if (!Initialize()) return;
		int count = _particleSystem.GetParticles(_particles);
		for (int i = 0; i < count; i++)
		{
			var p = _particles[i];
			Vector3 pos = p.position;
			Color32 c = p.GetCurrentColor(_particleSystem);
			float size = p.GetCurrentSize(_particleSystem) * 0.5f;
			Vector3 right = Vector3.right * size;
			Vector3 up = Vector3.up * size;
			_quad[0].position = pos - right - up; _quad[0].color = c; _quad[0].uv0 = new Vector2(_uv.x, _uv.y);
			_quad[1].position = pos - right + up; _quad[1].color = c; _quad[1].uv0 = new Vector2(_uv.x, _uv.w);
			_quad[2].position = pos + right + up; _quad[2].color = c; _quad[2].uv0 = new Vector2(_uv.z, _uv.w);
			_quad[3].position = pos + right - up; _quad[3].color = c; _quad[3].uv0 = new Vector2(_uv.z, _uv.y);
			vh.AddUIVertexQuad(_quad);
		}
	}

	private void Update()
	{
		if (Application.isPlaying)
		{
			SetVerticesDirty();
		}
	}

	public UIParticleSystem() { }
}
