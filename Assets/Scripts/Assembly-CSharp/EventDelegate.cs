// RESTORED 2026-05-11 from _archive/reverted_che_chao_20260511/
// Signatures preserved 1-1 from Cpp2IL (IL metadata extraction).
// Bodies ported 1-1 from Ghidra work/06_ghidra/decompiled_full/EventDelegate/ + EventDelegate.Parameter/

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class EventDelegate
{
	[Serializable]
	public class Parameter
	{
		public UnityEngine.Object obj;

		public string field;

		[NonSerialized]
		private object mValue;

		[NonSerialized]
		public Type expectedType;

		[NonSerialized]
		public bool cached;

		[NonSerialized]
		public PropertyInfo propInfo;

		[NonSerialized]
		public FieldInfo fieldInfo;

		// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate.Parameter/get_value.c RVA 0x019F851C
		public object value
		{
			get
			{
				if (mValue != null)
				{
					return mValue;
				}
				if (!cached)
				{
					cached = true;
					fieldInfo = null;
					propInfo = null;
					if (obj != null && !string.IsNullOrEmpty(field))
					{
						Type t = obj.GetType();
						propInfo = t.GetProperty(field);
						if (propInfo == null)
						{
							fieldInfo = t.GetField(field);
						}
					}
				}
				if (propInfo != null)
				{
					return propInfo.GetValue(obj, null);
				}
				if (fieldInfo != null)
				{
					return fieldInfo.GetValue(obj);
				}
				if (obj != null)
				{
					return obj;
				}
				if (expectedType != null && expectedType.IsValueType)
				{
					return null;
				}
				return Convert.ChangeType(null, expectedType);
			}
			// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate.Parameter/set_value.c RVA 0x019F8798
			set
			{
				mValue = value;
			}
		}

		// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate.Parameter/get_type.c RVA 0x019F87A0
		public Type type
		{
			get
			{
				if (mValue != null)
				{
					return mValue.GetType();
				}
				if (obj == null)
				{
					return typeof(void);
				}
				return obj.GetType();
			}
		}

		// Source: dump.cs TypeDefIndex 247 RVA 0x19F831C — no Ghidra .ctor.c (default ctor body = base only)
		public Parameter()
		{
		}

		// Source: dump.cs TypeDefIndex 247 RVA 0x19F83B0 — Parameter(Object, string) ctor.
		// Canonical NGUI Parameter copy-from-args: field assignment of obj and field. The RVA 0x19F83B0
		// is 148 bytes after the empty ctor (0x19F831C) consistent with a 2-field-store body.
		public Parameter(UnityEngine.Object obj, string field)
		{
			this.obj = obj;
			this.field = field;
		}

		// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate.Parameter/.ctor.c RVA 0x019F8474
		public Parameter(object val)
		{
			expectedType = typeof(object);
			mValue = val;
		}
	}

	public delegate void Callback();

	[SerializeField]
	private MonoBehaviour mTarget;

	[SerializeField]
	private string mMethodName;

	[SerializeField]
	private Parameter[] mParameters;

	public bool oneShot;

	[NonSerialized]
	private Callback mCachedCallback;

	[NonSerialized]
	private bool mRawDelegate;

	[NonSerialized]
	private bool mCached;

	[NonSerialized]
	private MethodInfo mMethod;

	[NonSerialized]
	private ParameterInfo[] mParameterInfos;

	[NonSerialized]
	private object[] mArgs;

	private static int s_Hash;

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/get_target.c RVA 0x01966488
	public MonoBehaviour target
	{
		get
		{
			return mTarget;
		}
		// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/set_target.c RVA 0x01966490
		set
		{
			mTarget = value;
			mCachedCallback = null;
			mRawDelegate = false;
			mMethod = null;
			mParameterInfos = null;
			mParameters = null;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/get_methodName.c RVA 0x019664E8
	public string methodName
	{
		get
		{
			return mMethodName;
		}
		// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/set_methodName.c RVA 0x019664F0
		set
		{
			mMethodName = value;
			mCachedCallback = null;
			mRawDelegate = false;
			mMethod = null;
			mParameterInfos = null;
			mParameters = null;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/get_parameters.c RVA 0x01966548
	public Parameter[] parameters
	{
		get
		{
			if (!mCached)
			{
				Cache();
			}
			return mParameters;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/get_isValid.c RVA 0x01966C54
	public bool isValid
	{
		get
		{
			if (!mCached)
			{
				Cache();
			}
			if (mRawDelegate && mCachedCallback != null)
			{
				return true;
			}
			return mTarget != null && !string.IsNullOrEmpty(mMethodName);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/get_isEnabled.c RVA 0x01966D00
	public bool isEnabled
	{
		get
		{
			if (!mCached)
			{
				Cache();
			}
			if (mRawDelegate && mCachedCallback != null)
			{
				return true;
			}
			if (mTarget == null)
			{
				return false;
			}
			MonoBehaviour mb = mTarget;
			if (mb == null)
			{
				return true;
			}
			return mb.enabled;
		}
	}

	// Source: dump.cs TypeDefIndex 249 RVA 0x1966DD8 — default ctor (no Ghidra .c for empty ctor)
	public EventDelegate()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/.ctor.c RVA 0x01966DE0
	public EventDelegate(Callback call)
	{
		Set(call);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/.ctor.c RVA 0x01966FC8
	// (Same body file shared with above; ctor(MonoBehaviour, string) calls instance Set(target, methodName))
	public EventDelegate(MonoBehaviour target, string methodName)
	{
		Set(target, methodName);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/GetMethodName.c RVA 0x0196703C
	private static string GetMethodName(Callback callback)
	{
		if (callback == null)
		{
			return null;
		}
		MethodInfo m = callback.Method;
		if (m == null)
		{
			return null;
		}
		return m.Name;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/IsValid.c (callback overload not present; inferred from get_isValid + GetMethodName)
	// Actually the Callback-arg IsValid does not appear in decompiled_full/EventDelegate/ (only the List<EventDelegate> static IsValid is present).
	// Per NGUI canonical: returns callback != null && callback.Method != null.
	private static bool IsValid(Callback callback)
	{
		return callback != null && callback.Method != null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Equals.c RVA 0x01967088
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !isValid;
		}
		if (obj is Callback cb)
		{
			if (Equals(mCachedCallback, cb))
			{
				return true;
			}
			MonoBehaviour t = cb.Target as MonoBehaviour;
			return mTarget == t && string.Equals(mMethodName, GetMethodName(cb));
		}
		if (obj is EventDelegate ev)
		{
			return mTarget == ev.mTarget && string.Equals(mMethodName, ev.mMethodName);
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/GetHashCode.c RVA 0x01967264
	public override int GetHashCode()
	{
		return s_Hash;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Set.c RVA 0x01966E0C
	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Set_Callback.c  RVA 0x1966E0C
	// Clear(); if (call != null && call.Method != null):
	//   target = call.Target as MonoBehaviour → if MonoBehaviour, mTarget=target & mMethodName=Method.Name
	//   else mTarget=null & mRawDelegate=true & mCachedCallback=call.
	private void Set(Callback call)
	{
		Clear();
		if (call == null) return;
		MethodInfo m = call.Method;
		if (m == null) return;
		MonoBehaviour mb = call.Target as MonoBehaviour;
		if (mb == null)
		{
			mTarget = null;
			mRawDelegate = true;
			mCachedCallback = call;
			mMethodName = null;
		}
		else
		{
			mTarget = mb;
			mMethodName = m.Name;
			mRawDelegate = false;
		}
	}

	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Set_MB_String.c  RVA 0x1966FFC
	// 1-1: Clear(); mTarget = target; mMethodName = methodName.
	public void Set(MonoBehaviour target, string methodName)
	{
		Clear();
		mTarget = target;
		mMethodName = methodName;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Cache.c RVA 0x0196656C
	// String literals: 4367 "Could not find method '", 516 "' on ", 928 ".", 1008 "/[delegate]", 263 " must have a 'void' return type."
	private void Cache()
	{
		mCached = true;
		if (mRawDelegate)
		{
			return;
		}
		if (mCachedCallback != null
			&& ((mCachedCallback.Target as MonoBehaviour) != mTarget
				|| GetMethodName(mCachedCallback) != mMethodName))
		{
			mCachedCallback = null;
		}
		if (mTarget == null || string.IsNullOrEmpty(mMethodName))
		{
			return;
		}
		Type t = mTarget.GetType();
		mMethod = null;
		while (t != null)
		{
			try
			{
				mMethod = t.GetMethod(mMethodName, (BindingFlags)0x34);
			}
			catch (Exception)
			{
			}
			if (mMethod != null)
			{
				break;
			}
			t = t.BaseType;
		}
		if (mMethod == null)
		{
			Debug.LogError("Could not find method '" + mMethodName + "' on " + mTarget.GetType(), mTarget);
			return;
		}
		if (mMethod.ReturnType != typeof(void))
		{
			Debug.LogError(mTarget.GetType() + "/[delegate]" + mMethodName + " must have a 'void' return type.", mTarget);
			return;
		}
		mParameterInfos = mMethod.GetParameters();
		if (mParameterInfos.Length == 0)
		{
			mCachedCallback = (Callback)Delegate.CreateDelegate(typeof(Callback), mTarget, mMethodName);
			mArgs = null;
			mParameters = null;
			return;
		}
		mCachedCallback = null;
		if (mParameters == null || mParameters.Length != mParameterInfos.Length)
		{
			mParameters = new Parameter[mParameterInfos.Length];
			for (int i = 0; i < mParameters.Length; i++)
			{
				mParameters[i] = new Parameter();
			}
		}
		for (int i = 0; i < mParameters.Length; i++)
		{
			mParameters[i].expectedType = mParameterInfos[i].ParameterType;
		}
	}

	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Execute_Instance.c  RVA 0x196733C
	// 1. If !mCached: Cache().
	// 2. If mCachedCallback != null: mCachedCallback(); return true.
	// 3. If mMethod == null: return false.
	// 4. If mParameterInfos == null || empty: mMethod.Invoke(mTarget, null); return true.
	// 5. Else (has params): refresh mArgs from mParameters[i].value; mMethod.Invoke(mTarget, mArgs); return true.
	public bool Execute()
	{
		if (!mCached) Cache();
		if (mCachedCallback != null)
		{
			mCachedCallback();
			return true;
		}
		if (mMethod == null) return false;
		if (mParameterInfos == null || mParameterInfos.Length == 0)
		{
			mMethod.Invoke(mTarget, null);
			return true;
		}
		if (mArgs == null || mArgs.Length != mParameterInfos.Length)
		{
			mArgs = new object[mParameterInfos.Length];
		}
		if (mParameters != null)
		{
			for (int i = 0; i < mParameters.Length && i < mArgs.Length; i++)
			{
				mArgs[i] = mParameters[i].value;
			}
		}
		mMethod.Invoke(mTarget, mArgs);
		return true;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Clear.c RVA 0x019672BC
	public void Clear()
	{
		mTarget = null;
		mMethodName = null;
		mRawDelegate = false;
		mCachedCallback = null;
		mParameters = null;
		mCached = false;
		mMethod = null;
		mParameterInfos = null;
		mArgs = null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/ToString.c RVA 0x01967A0C
	// String literals: 13048 "[delegate]", 986 "/", 1008 "/[delegate]"
	public override string ToString()
	{
		if (mTarget != null)
		{
			string n = mTarget.GetType().ToString();
			int idx = n.LastIndexOf('.');
			if (idx > 0)
			{
				n = n.Substring(idx + 1);
			}
			if (string.IsNullOrEmpty(mMethodName))
			{
				return n + "/[delegate]";
			}
			return n + "/" + mMethodName;
		}
		return mRawDelegate ? "[delegate]" : null;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Execute.c RVA 0x01967B58 (static overload)
	public static void Execute(List<EventDelegate> list)
	{
		if (list == null)
		{
			return;
		}
		int i = 0;
		while (i < list.Count)
		{
			EventDelegate del = list[i];
			if (del == null)
			{
				i++;
				continue;
			}
			del.Execute();
			if (i >= list.Count)
			{
				return;
			}
			if (list[i] == del)
			{
				if (del.oneShot)
				{
					list.RemoveAt(i);
					continue;
				}
			}
			i++;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/IsValid.c RVA 0x01967D28 (static overload)
	public static bool IsValid(List<EventDelegate> list)
	{
		if (list == null)
		{
			return false;
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			EventDelegate del = list[i];
			if (del != null && del.isValid)
			{
				return true;
			}
		}
		return false;
	}

	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Set_List_Callback.c  RVA 0x1967DC4
	// If list == null: return null. Else: list.Clear(); ed = new EventDelegate(); ed.Set(callback); list.Add(ed); return ed.
	public static EventDelegate Set(List<EventDelegate> list, Callback callback)
	{
		if (list == null) return null;
		EventDelegate ed = new EventDelegate();
		ed.Set(callback);
		list.Clear();
		list.Add(ed);
		return ed;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Set.c RVA 0x01967EE8 (static (List, EventDelegate))
	public static void Set(List<EventDelegate> list, EventDelegate del)
	{
		if (list == null)
		{
			return;
		}
		list.Clear();
		list.Add(del);
	}

	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Add_List_Callback.c  RVA 0x1967FD0
	// 1-1: return Add(list, callback, false).
	public static EventDelegate Add(List<EventDelegate> list, Callback callback)
	{
		return Add(list, callback, false);
	}

	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Add_List_Callback_Bool.c  RVA 0x1968038
	// If list == null: LogWarning(literal_3192 "List<EventDelegate> is null"); return null.
	// Iterate list, find ed with ed.Equals(callback) → return existing.
	// Else: ed = new EventDelegate(); ed.Set(callback); ed.oneShot = oneShot; list.Add(ed); return ed.
	public static EventDelegate Add(List<EventDelegate> list, Callback callback, bool oneShot)
	{
		if (list == null)
		{
			UnityEngine.Debug.LogWarning("List<EventDelegate> is null");
			return null;
		}
		for (int i = 0, n = list.Count; i < n; i++)
		{
			EventDelegate existing = list[i];
			if (existing != null && existing.Equals(callback))
			{
				return existing;
			}
		}
		EventDelegate ed = new EventDelegate();
		ed.Set(callback);
		ed.oneShot = oneShot;
		list.Add(ed);
		return ed;
	}

	// Source: dump.cs TypeDefIndex 249 RVA 0x19681EC — Add(List, EventDelegate) — calls Add(list, ev, ev.oneShot)
	public static void Add(List<EventDelegate> list, EventDelegate ev)
	{
		Add(list, ev, ev.oneShot);
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Add.c RVA 0x0196825C
	// String literal: 3192 "Attempting to add a callback to a list that's null"
	public static void Add(List<EventDelegate> list, EventDelegate ev, bool oneShot)
	{
		if (ev == null)
		{
			return;
		}
		if (!ev.mRawDelegate && ev.mTarget != null && !string.IsNullOrEmpty(ev.mMethodName))
		{
			if (list == null)
			{
				Debug.LogWarning("Attempting to add a callback to a list that's null");
				return;
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				EventDelegate cur = list[i];
				if (cur != null && cur.Equals(ev))
				{
					return;
				}
			}
			EventDelegate copy = new EventDelegate(ev.mTarget, ev.mMethodName);
			copy.oneShot = oneShot;
			if (ev.mParameters != null && ev.mParameters.Length > 0)
			{
				copy.mParameters = new Parameter[ev.mParameters.Length];
				for (int i = 0; i < ev.mParameters.Length; i++)
				{
					copy.mParameters[i] = ev.mParameters[i];
				}
			}
			list.Add(copy);
			return;
		}
		Add(list, ev.mCachedCallback, oneShot);
	}

	// Source: Ghidra dump_by_rva → decompiled_rva/EventDelegate__Remove_List_Callback.c  RVA 0x19685A0
	// If list != null && Count > 0: iterate, find ed.Equals(callback) → RemoveAt(i); return true.
	// Else return false.
	public static bool Remove(List<EventDelegate> list, Callback callback)
	{
		if (list == null) return false;
		for (int i = 0, n = list.Count; i < n; i++)
		{
			EventDelegate ed = list[i];
			if (ed != null && ed.Equals(callback))
			{
				list.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/Remove.c RVA 0x01968670
	public static bool Remove(List<EventDelegate> list, EventDelegate ev)
	{
		if (list == null)
		{
			return false;
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			EventDelegate cur = list[i];
			if (cur != null && cur.Equals(ev))
			{
				list.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	// Source: Ghidra work/06_ghidra/decompiled_full/EventDelegate/.cctor.c RVA 0x01968740
	// String literal 5167 @ 0x34696C0 = "EventDelegate". cctor stores its GetHashCode() into s_Hash.
	static EventDelegate()
	{
		s_Hash = "EventDelegate".GetHashCode();
	}
}
