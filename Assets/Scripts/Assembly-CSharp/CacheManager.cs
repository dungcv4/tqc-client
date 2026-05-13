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

		public bool isDone
		{
			get
			{ return default; }
		}

		public float progress
		{
			get
			{ return default; }
		}

		public string error
		{
			get
			{ return default; }
		}

		public ShaderLoader(Queue<string> shaders)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool RunOnce(CacheManager instance)
		{ return default; }
	}

	private static CacheManager s_instance;

	public string[] _shaders;

	private Dictionary<string, Shader> _cacheShaders;

	private Queue<ICacheRunner> _runners;

	public static CacheManager Instance
	{
		get
		{ return default; }
	}

	private void Start()
	{ }

	private void Update()
	{ }

	private void OnDestroy()
	{ }

	public IUJAsyncOperation CacheShaders()
	{ return default; }

	public IUJAsyncOperation CacheShaders(Queue<string> shaders)
	{ return default; }

	public CacheManager()
	{ }
}
