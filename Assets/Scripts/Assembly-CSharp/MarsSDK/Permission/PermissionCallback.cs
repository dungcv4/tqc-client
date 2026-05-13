using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace MarsSDK.Permission
{
	public class PermissionCallback
	{
		public delegate void PermissionDelegate(string[] permission);

		private event PermissionDelegate _eventConfirm
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private event PermissionDelegate _eventCancel
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private event PermissionDelegate _eventDanied
		{
			[CompilerGenerated]
			add
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
			[CompilerGenerated]
			remove
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public PermissionCallback(PermissionDelegate confirm = null, PermissionDelegate cancel = null, PermissionDelegate denied = null)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void OnConfirm(string[] permissions)
		{ }

		public void OnCancel(string[] permissions)
		{ }

		public void OnDenied(string[] permissions)
		{ }
	}
}
