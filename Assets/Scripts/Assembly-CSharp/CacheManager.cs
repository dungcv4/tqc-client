// Source: Ghidra work/06_ghidra/decompiled_full/CacheManager/ — shader pre-cache singleton.

using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/Cache Manager")]
public class CacheManager : MonoBehaviour
{
	private interface ICacheRunner
	{
		bool RunOnce(CacheManager instance);
	}

	private class ShaderLoader : IUJAsyncOperation, ICacheRunner
	{
		private Queue<string> _shaders;
		private float _invCount;
		private float _fProgress;
		private bool _done;

		public bool isDone { get { return _done; } }
		public float progress { get { return _fProgress; } }
		public string error { get { return null; } }

		public ShaderLoader(Queue<string> shaders)
		{
			_shaders = shaders;
			_invCount = (shaders != null && shaders.Count > 0) ? 1f / (float)shaders.Count : 0f;
		}

		public bool RunOnce(CacheManager instance)
		{
			if (_shaders == null || _shaders.Count == 0) { _done = true; return true; }
			string name = _shaders.Dequeue();
			Shader s = Shader.Find(name);
			if (s != null && instance._cacheShaders != null) instance._cacheShaders[name] = s;
			_fProgress = 1f - (float)_shaders.Count * _invCount;
			if (_shaders.Count == 0) { _done = true; _fProgress = 1f; return true; }
			return false;
		}
	}

	private static CacheManager s_instance;

	public string[] _shaders;
	private Dictionary<string, Shader> _cacheShaders;
	private Queue<ICacheRunner> _runners;

	public static CacheManager Instance
	{
		get { return s_instance; }
	}

	private void Start()
	{
		s_instance = this;
		_cacheShaders = new Dictionary<string, Shader>();
		_runners = new Queue<ICacheRunner>();
		if (_shaders != null && _shaders.Length > 0)
		{
			Queue<string> q = new Queue<string>(_shaders);
			_runners.Enqueue(new ShaderLoader(q));
		}
	}

	private void Update()
	{
		if (_runners == null || _runners.Count == 0) return;
		var r = _runners.Peek();
		if (r != null && r.RunOnce(this)) _runners.Dequeue();
	}

	private void OnDestroy()
	{
		if (s_instance == this) s_instance = null;
	}

	public IUJAsyncOperation CacheShaders()
	{
		if (_shaders == null || _shaders.Length == 0) return null;
		return CacheShaders(new Queue<string>(_shaders));
	}

	public IUJAsyncOperation CacheShaders(Queue<string> shaders)
	{
		ShaderLoader l = new ShaderLoader(shaders);
		if (_runners == null) _runners = new Queue<ICacheRunner>();
		_runners.Enqueue(l);
		return l;
	}

	public CacheManager() { }
}
