using System.Text;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Utils
{
	public sealed class UjUtils
	{
		public enum eMemFree
		{
			gc = 1,
			unloadUnusedAssets = 2,
			all = 3
		}

		private static StringBuilder _cvSb;

		public static void MemFree(eMemFree type)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static string EncodeJsString(string s)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static string ConvertHexStringToUnicode(string textStore)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void ActivateRecursively(GameObject go, bool state)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void Deactivate(Transform t)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void Activate(Transform t)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void SetActiveChildren(GameObject go, bool state)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void SetActiveSelf(GameObject go, bool state)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static bool GetActive(GameObject go)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static string GetTargetLocalPath(string path)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public UjUtils()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		static UjUtils()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
