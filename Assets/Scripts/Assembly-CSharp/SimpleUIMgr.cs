// Source: Ghidra work/06_ghidra/decompiled_full/SimpleUIMgr/ — IMGUI view registry (rarely used, mostly debug).

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class SimpleUIMgr : MonoSingleton<SimpleUIMgr>
{
	public delegate void OnGUIImplementation();

	protected string currentView;
	protected Dictionary<string, OnGUIImplementation> views;
	protected Rect rectInstructions;
	protected bool showInstructions;

	public void AddView(string key, OnGUIImplementation view)
	{
		if (views == null) views = new Dictionary<string, OnGUIImplementation>();
		views[key] = view;
	}

	public void SetView(string key) { currentView = key; }

	protected void UpdateView()
	{
		if (views == null || string.IsNullOrEmpty(currentView)) return;
		OnGUIImplementation v;
		if (views.TryGetValue(currentView, out v) && v != null) v();
	}

	private void Instructions() { }
	protected virtual void ViewInstructions() { }

	public SimpleUIMgr() { }
}
