// Source: Ghidra work/06_ghidra/decompiled_full/PropertyReference/ (16 .c)
// Generic property/field binding by name on a Component. Used by Tweens / serialized refs.
// Field offsets: mTarget@0x10 (Component), mName@0x18 (string), mField@0x20 (FieldInfo), mProperty@0x28 (PropertyInfo)

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
		get { return mTarget; }
		set
		{
			mTarget = value;
			mField = null;
			mProperty = null;
		}
	}

	public string name
	{
		get { return mName; }
		set
		{
			mName = value;
			mField = null;
			mProperty = null;
		}
	}

	// Source: Ghidra get_isValid.c — true when mTarget != null && mName non-empty.
	public bool isValid
	{
		get { return mTarget != null && !string.IsNullOrEmpty(mName); }
	}

	// Source: Ghidra get_isEnabled.c — isValid && Cache() && (mField OR mProperty resolved).
	public bool isEnabled
	{
		get
		{
			if (!isValid) return false;
			if (mField != null || mProperty != null) return true;
			return Cache();
		}
	}

	public PropertyReference() { }

	public PropertyReference(Component target, string fieldName)
	{
		mTarget = target;
		mName = fieldName;
	}

	// Source: Ghidra GetPropertyType.c — Cache, then return field/property type.
	public Type GetPropertyType()
	{
		if (!Cache()) return typeof(void);
		if (mField != null) return mField.FieldType;
		if (mProperty != null) return mProperty.PropertyType;
		return typeof(void);
	}

	// Source: Ghidra Equals.c — compare mTarget + mName equality.
	public override bool Equals(object obj)
	{
		PropertyReference other = obj as PropertyReference;
		if (other == null) return false;
		return mTarget == other.mTarget && mName == other.mName;
	}

	// Source: Ghidra GetHashCode.c — XOR hash of target + name; cached in s_Hash (no-op for us).
	public override int GetHashCode()
	{
		int h = s_Hash;
		if (mTarget != null) h = (mTarget.GetHashCode());
		if (mName != null) h ^= mName.GetHashCode();
		return h;
	}

	// Source: Ghidra Set(Component, string).c
	public void Set(Component target, string methodName)
	{
		mTarget = target;
		mName = methodName;
		mField = null;
		mProperty = null;
	}

	// Source: Ghidra Clear.c — null out target + name + cached.
	public void Clear()
	{
		mTarget = null;
		mName = null;
		mField = null;
		mProperty = null;
	}

	// Source: Ghidra Reset.c — null out cached only.
	public void Reset()
	{
		mField = null;
		mProperty = null;
	}

	public override string ToString()
	{
		return ToString(mTarget, mName);
	}

	public static string ToString(Component comp, string property)
	{
		if (comp == null) return "<null>";
		string type = comp.GetType().Name;
		if (string.IsNullOrEmpty(property)) return type;
		return type + "/" + property;
	}

	// Source: Ghidra Get.c — Cache; return field.GetValue OR property.GetValue.
	[DebuggerStepThrough]
	[DebuggerHidden]
	public object Get()
	{
		if (!Cache()) return null;
		if (mField != null) return mField.GetValue(mTarget);
		if (mProperty != null) return mProperty.GetValue(mTarget, null);
		return null;
	}

	// Source: Ghidra Set(object).c — Cache; field.SetValue OR property.SetValue with Convert if types differ.
	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{
		if (!Cache()) return false;
		if (mField != null)
		{
			if (Convert(ref value))
			{
				mField.SetValue(mTarget, value);
				return true;
			}
			return false;
		}
		if (mProperty != null)
		{
			if (Convert(ref value))
			{
				mProperty.SetValue(mTarget, value, null);
				return true;
			}
			return false;
		}
		return false;
	}

	// Source: Ghidra Cache.c — reflect mTarget.GetType().GetField(mName) → mField; else GetProperty → mProperty.
	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (mTarget == null || string.IsNullOrEmpty(mName)) return false;
		if (mField != null || mProperty != null) return true;
		Type t = mTarget.GetType();
		mField = t.GetField(mName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (mField != null) return true;
		mProperty = t.GetProperty(mName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		return mProperty != null;
	}

	// Source: Ghidra Convert(ref).c — match dest type via Convert(from, to).
	private bool Convert(ref object value)
	{
		Type dest = GetPropertyType();
		if (value == null) return dest.IsClass || Nullable.GetUnderlyingType(dest) != null;
		Type src = value.GetType();
		if (src == dest) return true;
		return Convert(ref value, src, dest);
	}

	// Source: Ghidra Convert(Type, Type).c — type compatibility check.
	public static bool Convert(Type from, Type to)
	{
		if (from == to) return true;
		if (to.IsAssignableFrom(from)) return true;
		if (from.IsPrimitive && to.IsPrimitive) return true;
		return false;
	}

	// Source: Ghidra Convert(object, Type).c
	public static bool Convert(object value, Type to)
	{
		if (value == null) return to.IsClass;
		return Convert(value.GetType(), to);
	}

	// Source: Ghidra Convert(ref, Type, Type).c — actually performs convert.
	public static bool Convert(ref object value, Type from, Type to)
	{
		if (from == to) return true;
		if (to.IsAssignableFrom(from)) return true;
		if (value == null) return false;
		try
		{
			value = System.Convert.ChangeType(value, to);
			return true;
		}
		catch { return false; }
	}

	static PropertyReference() { }
}
