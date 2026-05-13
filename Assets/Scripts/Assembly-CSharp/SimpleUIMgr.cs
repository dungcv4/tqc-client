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
	{ }

	public void SetView(string key)
	{ }

	protected void UpdateView()
	{ }

	private void Instructions()
	{ }

	protected virtual void ViewInstructions()
	{ }

	public SimpleUIMgr()
	{ }
}
