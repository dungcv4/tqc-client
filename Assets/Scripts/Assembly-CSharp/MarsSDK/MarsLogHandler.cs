using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	internal class MarsLogHandler : ILogHandler
	{
		void ILogHandler.LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void ILogHandler.LogException(Exception exception, UnityEngine.Object context)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public MarsLogHandler()
		{ }
	}
}
