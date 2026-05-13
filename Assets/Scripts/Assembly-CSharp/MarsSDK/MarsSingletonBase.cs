using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	[Obsolete("MarsSingletonBase is deprecated. Use MarsMonoSingleton instead.", true)]
	public abstract class MarsSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{ return default; }
		}

		public static void Touch()
		{ }

		public static void Untouch()
		{ }

		public static bool IsTouched()
		{ return default; }

		protected MarsSingletonBase()
		{ }
	}
}
