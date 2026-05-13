using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	public class BridgeController : MonoBehaviour
	{
		private static AndroidJavaClass mJc;

		private static AndroidJavaObject mJo;

		public static AndroidJavaClass GetBridgeControllerClass()
		{ return default; }

		public static AndroidJavaObject GetBridgeControllerInstance()
		{ return default; }

		public static AndroidJavaObject GetNetworkStateBridge()
		{ return default; }

		public static AndroidJavaObject GetDeleteAccountBridge()
		{ return default; }

		public BridgeController()
		{ }
	}
}
