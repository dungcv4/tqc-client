using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventCallBack
{
	private WndForm_Lua _parent;

	private string _sLuaMethod;

	public EventCallBack(WndForm_Lua parent, string sLuaMethod)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public void OnEvent(bool bOn)
	{ }

	public void OnEvent(int iIdx)
	{ }

	public void OnEvent(string value)
	{ }

	public void OnEvent(BaseEventData eventData)
	{ }

	public void OnEvent(Vector2 pos)
	{ }

	public void OnEvent(float value)
	{ }
}
