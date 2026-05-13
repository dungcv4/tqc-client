using Cpp2IlInjected;

namespace SONETWORK
{
	public sealed class ProtocolInterface
	{
		private IProtocolHandler[] m_protocols;

		public void addProtocol(ushort protoc, IProtocolHandler handler)
		{ }

		public int message(ConnectProxy proxy, ref proto_COMM header, byte[] data)
		{ return default; }

		public ProtocolInterface()
		{ }
	}
}
