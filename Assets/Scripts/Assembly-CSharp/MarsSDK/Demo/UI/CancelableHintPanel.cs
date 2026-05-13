using System;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace MarsSDK.Demo.UI
{
	public class CancelableHintPanel : PanelBase
	{
		private static CancelableHintPanel _instance;

		public static CancelableHintPanel Instance
		{
			get
			{ return default; }
		}

		public static event Action _eventCancel
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

		public CancelableHintPanel()
		{ }

		public void Show(string msg, Action doActionsAfterCancel = null)
		{ }

		public void Cancel()
		{ }
	}
}
