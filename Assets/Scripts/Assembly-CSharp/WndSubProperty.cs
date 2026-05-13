using System;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Progs/SubProperty Assign")]
public class WndSubProperty : MonoBehaviour
{
	[Serializable]
	public class PropertyDecl
	{
		public string _name;

		public int _index;

		public UnityEngine.Object _object;

		public PropertyDecl()
		{ }
	}

	[SerializeField]
	[HideInInspector]
	public PropertyDecl[] _properties;

	public WndSubProperty()
	{ }
}
