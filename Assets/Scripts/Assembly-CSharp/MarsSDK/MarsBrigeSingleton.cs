using System;
using Cpp2IlInjected;

namespace MarsSDK
{
	public abstract class MarsBrigeSingleton<T> : MarsMessageProcess where T : new()
	{
		public delegate void dEventProcess();

		public delegate void dEventProcessWithArgs(string[] args);

		public delegate void dEventProcessWithStatus(int status, string[] args);

		private static T _instance;

		public static T Instance
		{
			get
			{ return default; }
		}

		public static void Touch()
		{ }

		protected MarsBrigeSingleton(EOperationAgent marsAgent) : base(marsAgent)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetMessageUserType()
		{ return default; }
	}
}
