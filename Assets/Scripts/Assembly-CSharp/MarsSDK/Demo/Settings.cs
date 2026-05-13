using System;
using Cpp2IlInjected;

namespace MarsSDK.Demo
{
	[Serializable]
	public class Settings
	{
		public string ProjectName;

		public string dev;

		public string prod;

		public Settings()
		{ }
	}
}
