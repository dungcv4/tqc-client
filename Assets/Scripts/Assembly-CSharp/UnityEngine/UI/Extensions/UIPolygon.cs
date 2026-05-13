using Cpp2IlInjected;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Primitives/UI Polygon")]
	public class UIPolygon : MaskableGraphic
	{
		[SerializeField]
		private Texture m_Texture;

		public bool fill;

		public float thickness;

		[Range(3f, 360f)]
		public int sides;

		[Range(0f, 360f)]
		public float rotation;

		[Range(0f, 1f)]
		public float[] VerticesDistances;

		private float size;

		public override Texture mainTexture
		{
			get
			{ return default; }
		}

		public Texture texture
		{
			get
			{ return default; }
			set
			{ }
		}

		public void DrawPolygon(int _sides)
		{ }

		public void DrawPolygon(int _sides, float[] _VerticesDistances)
		{ }

		public void DrawPolygon(int _sides, float[] _VerticesDistances, float _rotation)
		{ }

		public void Redraw()
		{ }

		private void Update()
		{ }

		protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
		{ return default; }

		protected override void OnPopulateMesh(VertexHelper vh)
		{ }

		protected override void OnEnable()
		{ }

		public UIPolygon()
		{ }
	}
}
