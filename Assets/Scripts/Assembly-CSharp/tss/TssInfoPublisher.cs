using System.Collections.Generic;
using System.Threading;
using Cpp2IlInjected;

namespace tss
{
	public class TssInfoPublisher
	{
		public const int TSS_INFO_TYPE_DETECT_RESULT = 1;

		public const int TSS_INFO_TYPE_HEARTBEAT = 2;

		private static TssInfoPublisher mInstance;

		private static readonly object mSingletonLock;

		private readonly object padlockReceiver;

		private static List<TssInfoReceiver> mReceivers;

		private static Thread mTssInfoPublisherThread;

		private static bool mTssInfoPublisherThreadStarted;

		private TssInfoPublisher()
		{ }

		public static TssInfoPublisher getInstance()
		{ return default; }

		public void registTssInfoReceiver(TssInfoReceiver receiver)
		{ }

		private void broadcastInfo(int id, string info)
		{ }

		private void recvDataThread()
		{ }

		private static int openPipe()
		{ return default; }

		private static void closePipe()
		{ }

		private static string recvPipe()
		{ return default; }

		static TssInfoPublisher()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
