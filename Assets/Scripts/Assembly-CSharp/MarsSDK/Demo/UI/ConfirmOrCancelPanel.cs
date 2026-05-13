using System;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;

namespace MarsSDK.Demo.UI
{
	public class ConfirmOrCancelPanel : PanelBase
	{
		private static ConfirmOrCancelPanel _instance;

		public static ConfirmOrCancelPanel Instance
		{
			get
			{ return default; }
		}

		public static event Action _eventConfirm
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

		public ConfirmOrCancelPanel()
		{ }

		public void Show(string msg, Action doActionsAfterConfirm = null, Action doActionsAfterCancel = null)
		{ }

		public void Confirm()
		{ }

		public void Cancel()
		{ }
	}
}
