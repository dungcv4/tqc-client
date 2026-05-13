using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	public class Mars : MonoBehaviour, IUnityMessageListener
	{
		private static Mars mInstance;

		public const string NETWORK_ERROR_EXCEPTION = "1";

		public const string NETWORK_ERROR_CODE = "2";

		public const string NETWORK_NOT_FOUND = "-1";

		public const string NETWORK_CONNECT_HOST_FAIL = "-100";

		private static string[] DELIMITER_CHARS;

		private static string[] DELIMITER_CHARS_V;

		private static AndroidJavaObject activity;

		private static AndroidJavaObject mUI;

		public static Mars Instance()
		{ return default; }

		public static AndroidJavaObject GetUnityActivety()
		{ return default; }

		public Mars()
		{ }

		private void Awake()
		{ }

		private void Start()
		{ }

		public void MsgReceiver(string receivermsg)
		{ }

		public void RecvMsgWithPlatformId(string receivermsg)
		{ }

		public void ExtensionPluginMsgReceiver(string receivermsg)
		{ }

		public void InitUnityPlugin(string serviceURL)
		{ }

		public void SendMessage(string platform, string msg, string[] values)
		{ }

		public void SendMessageToPlugin(string plugin, string msg, string[] values)
		{ }

		public void SendMessageWithPlatformId(int platformId, string msg, string[] values)
		{ }

		static Mars()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
