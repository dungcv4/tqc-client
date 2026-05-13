using System.Reflection;
using Cpp2IlInjected;

namespace MarsSDK.Utils
{
	public sealed class ClipboardHelper
	{
		private static PropertyInfo _systemCopyBufferProperty;

		public static string clipBoard
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			set
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private static PropertyInfo GetSystemCopyBufferProperty()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public ClipboardHelper()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
