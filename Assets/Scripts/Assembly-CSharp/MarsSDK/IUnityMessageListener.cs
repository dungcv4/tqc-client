namespace MarsSDK
{
	internal interface IUnityMessageListener
	{
		void SendMessage(string platform, string msg, string[] values);

		void SendMessageWithPlatformId(int platformId, string msg, string[] values);

		void SendMessageToPlugin(string plugin, string msg, string[] values);
	}
}
