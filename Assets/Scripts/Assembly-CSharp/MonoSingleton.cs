using Cpp2IlInjected;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T m_Instance;

	public static T instance
	{
		get
		{ return default; }
	}

	private void Awake()
	{ }

	protected virtual void Init()
	{ }

	private void OnApplicationQuit()
	{ }

	protected MonoSingleton()
	{ }
}
