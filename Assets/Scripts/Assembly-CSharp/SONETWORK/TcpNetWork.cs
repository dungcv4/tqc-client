using System.Collections.Generic;
using Cpp2IlInjected;

namespace SONETWORK
{
	internal sealed class TcpNetWork
	{
		private static TcpNetWork s_instance;

		private static double s_lastTime;

		private static uint s_tickMask;

		private int m_proxiesPerTick;

		private Queue<ConnectProxy> m_connections;

		private uint m_lastProxyID;

		private Queue<uint> m_freeProxyIDs;

		private TcpNetWork()
		{ }

		~TcpNetWork()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private uint getFreeProxyID()
		{ return default; }

		private void freeProxyID(uint proxyID)
		{ }

		private void closeAll()
		{ }

		public static ConnectProxy createConnection(string host, int port, bool idleEnabled, float inactiveTime)
		{ return default; }

		public static void run(float dTime)
		{ }

		public static double lastTime()
		{ return default; }

		public static uint tickMask()
		{ return default; }

		public static int proxiesPerTick()
		{ return default; }

		public static void proxiesPerTick(int n)
		{ }

		static TcpNetWork()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
