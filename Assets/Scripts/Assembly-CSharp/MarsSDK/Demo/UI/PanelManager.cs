using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsSDK.Demo.UI
{
	public class PanelManager
	{
		private static PanelManager _instance;

		private Dictionary<string, PanelBase> _panelDict;

		public static PanelManager Instance
		{
			get
			{ return default; }
		}

		public void RegisterPanel(string resourcesPath, PanelBase panel)
		{ }

		public PanelBase GetPanel(string resourcesPath)
		{ return default; }

		public T GetPanel<T>(string resourcesPath) where T : PanelBase
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void ShowPanel<T>(string resourcesPath, string msg) where T : PanelBase
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void HidePanel<T>(string resourcesPath) where T : PanelBase
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void HideAllPanel()
		{ }

		public PanelManager()
		{ }
	}
}
