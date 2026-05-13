using Cpp2IlInjected;

namespace MarsSDK.NetworkState
{
	public class NetworkStateManager : MarsBrigeSingleton<NetworkStateManager>
	{
		// Source: dump.cs EOperationAgent.NetworkState  // matches class name
		public delegate void NetworkChangeCallback(string status);

		public static NetworkChangeCallback onNetworkChange;

		public NetworkStateManager() : base(EOperationAgent.NetworkState)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		protected override void V_doMessageProcess(string action, string[] args)
		{ }

		public static bool IsWiFiConnect()
		{ return default; }

		public static bool IsVPN()
		{ return default; }

		public static bool IsNetworkConnect()
		{ return default; }

		public static string GetNetworkType()
		{ return default; }

		public static string GetCellularType()
		{ return default; }
	}
}
