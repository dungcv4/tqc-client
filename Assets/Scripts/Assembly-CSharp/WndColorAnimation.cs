using System;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/Color Animation")]
public class WndColorAnimation : WndAnimation
{
	[Serializable]
	public class Node
	{
		[SerializeField]
		private Color _color;

		[SerializeField]
		private float _duration;

		public Color color
		{
			get
			{ return default; }
			set
			{ }
		}

		public float duration
		{
			get
			{ return default; }
			set
			{ }
		}

		public Node()
		{ }
	}

	[SerializeField]
	private Graphic _uiWidget;

	[SerializeField]
	private Node[] _pathNodes;

	private int _curFrame;

	public Node[] pathNodes
	{
		get
		{ return default; }
		set
		{ }
	}

	public Graphic uiWidget
	{
		get
		{ return default; }
		set
		{ }
	}

	private void Start()
	{ }

	private void Update()
	{ }

	private void InitAnimation()
	{ }

	public override void PlayAnimation()
	{ }

	public override void StopAnimation()
	{ }

	public WndColorAnimation()
	{ }
}
