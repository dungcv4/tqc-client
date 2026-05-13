using System;
using System.Diagnostics;
using System.Reflection;
using Cpp2IlInjected;
using UnityEngine;

[Serializable]
public class PropertyReference
{
	[SerializeField]
	private Component mTarget;

	[SerializeField]
	private string mName;

	private FieldInfo mField;

	private PropertyInfo mProperty;

	private static int s_Hash;

	public Component target
	{
		get
		{ return default; }
		set
		{ }
	}

	public string name
	{
		get
		{ return default; }
		set
		{ }
	}

	public bool isValid
	{
		get
		{ return default; }
	}

	public bool isEnabled
	{
		get
		{ return default; }
	}

	public PropertyReference()
	{ }

	public PropertyReference(Component target, string fieldName)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public Type GetPropertyType()
	{ return default; }

	public override bool Equals(object obj)
	{ return default; }

	public override int GetHashCode()
	{ return default; }

	public void Set(Component target, string methodName)
	{ }

	public void Clear()
	{ }

	public void Reset()
	{ }

	public override string ToString()
	{ return default; }

	public static string ToString(Component comp, string property)
	{ return default; }

	[DebuggerStepThrough]
	[DebuggerHidden]
	public object Get()
	{ return default; }

	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{ return default; }

	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{ return default; }

	private bool Convert(ref object value)
	{ return default; }

	public static bool Convert(Type from, Type to)
	{ return default; }

	public static bool Convert(object value, Type to)
	{ return default; }

	public static bool Convert(ref object value, Type from, Type to)
	{ return default; }

	static PropertyReference()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
