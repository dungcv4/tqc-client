namespace SONETWORK
{
	public interface IProtocolHandler
	{
		object vProtocolObject();

		bool vHandleMessage(ConnectProxy pProxy, uint UID, ref object obj);
	}
}
