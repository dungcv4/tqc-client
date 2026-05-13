using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Demo.UI
{
	public abstract class PanelBase : MonoBehaviour
	{
		private static GameObject _panel;

		private int _default_size;

		private string _resourcesPath;

		public void InitPanel(string resourcesPath)
		{ }

		public GameObject GetPanelInstance()
		{ return default; }

		public bool isShowing()
		{ return default; }

		public void Hide()
		{ }

		public void SetTextSize(int size)
		{ }

		protected virtual void V_ViewStart(string[] args)
		{ }

		protected virtual void V_ViewDistroy(string[] args)
		{ }

		protected virtual void V_ViewActive(bool isActive, string[] args)
		{ }

		protected virtual void V_ViewDirty(string[] args)
		{ }

		public void ViewStart(string[] args)
		{ }

		public void ViewDistroy(string[] args)
		{ }

		public void ViewActive(bool isActive, string[] args)
		{ }

		public void ViewDirty(string[] args)
		{ }

		protected PanelBase()
		{ }
	}
}
