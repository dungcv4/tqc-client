using System.Collections.Generic;
using Cpp2IlInjected;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Effects/Letter Spacing", 16)]
	[RequireComponent(typeof(Text))]
	public class LetterSpacing : BaseMeshEffect, ILayoutElement
	{
		[SerializeField]
		private float m_spacing;

		public float spacing
		{
			get
			{ return default; }
			set
			{ }
		}

		private Text text
		{
			get
			{ return default; }
		}

		public float minWidth
		{
			get
			{ return default; }
		}

		public float preferredWidth
		{
			get
			{ return default; }
		}

		public float flexibleWidth
		{
			get
			{ return default; }
		}

		public float minHeight
		{
			get
			{ return default; }
		}

		public float preferredHeight
		{
			get
			{ return default; }
		}

		public float flexibleHeight
		{
			get
			{ return default; }
		}

		public int layoutPriority
		{
			get
			{ return default; }
		}

		protected LetterSpacing()
		{ }

		public void CalculateLayoutInputHorizontal()
		{ }

		public void CalculateLayoutInputVertical()
		{ }

		private string[] GetLines()
		{ return default; }

		public override void ModifyMesh(VertexHelper vh)
		{ }

		public void ModifyVertices(List<UIVertex> verts)
		{ }
	}
}
