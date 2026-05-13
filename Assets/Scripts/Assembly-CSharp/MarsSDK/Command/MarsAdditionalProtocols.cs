using System;
using System.Collections.Generic;
using Cpp2IlInjected;
using MarsSDK.Command.Extended;

namespace MarsSDK.Command
{
	public class MarsAdditionalProtocols
	{
		private static Dictionary<eNetCmd_Extended, Action<string[]>> replyAction;

		public static bool HasAdditionReplyRegister(eNetCmd_Extended cmd_reply)
		{ return default; }

		public static void OnAdditionReplyRegister(eNetCmd_Extended cmd, Action<string[]> action)
		{ }

		public static void ReplyDispatcher(int cmd_reply, string[] args)
		{ }

		public MarsAdditionalProtocols()
		{ }

		static MarsAdditionalProtocols()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
