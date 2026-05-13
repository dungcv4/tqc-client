using Cpp2IlInjected;

namespace SONETWORK
{
	public sealed class NETWORK_MSG
	{
		public const int NM_OK = 0;

		public const int NM_INVALID_SOCKET = -1;

		public const int NM_LOST_SOCKET = -2;

		public const int NM_SELECT_FAILED = -3;

		public const int NM_RECV_FAILED = -4;

		public const int NM_SEND_FAILED = -5;

		public const int NM_SERVER_CLOSED = -6;

		public const int NM_SEND_BUFFER_FULL = -7;

		public const int NM_CLOSE_CONNECT = -8;

		public const int NM_CONNECT_FAILED = -11;

		public const int NM_CONNECT_TIMEOUT = -12;

		public const int NM_LOST_CONNECT = -13;

		public const int NM_IDLE_CONNECT = -14;

		public const int NM_OUT_RECV_BUFFER = -15;

		public const int NM_OUT_SEND_BUFFER = -16;

		public const int NM_TIMEOUT = -17;

		public const int NM_NULL_PACKET = -18;

		public const int NM_INVALID_PROTOCOL_ID = -101;

		public const int NM_INVALID_PROTOCOL_DATA_SIZE = -102;

		public const int NM_INVALID_PROTOCOL_FORMAT = -103;

		public const int NM_INVALID_REPLY_FORMAT = -104;

		public const int NM_INVALID_REPLY = -105;

		public const int NM_INVALID_PACKAGE_DATA_SIZE = -106;

		public const int NM_INVALID_PACKAGE_SEQNO = -107;

		public const int NM_INVALID_PACKAGE_CHECKSUM = -108;

		public const int NM_INVALID_PACKAGE_DATA = -109;

		public const int NM_INVALID_PACKAGE_ZLIB = -110;

		public const int NM_INVALID_PROTOCOL_CHECKSUM = -111;

		public static string getMsgString(int msgID)
		{ return default; }

		public NETWORK_MSG()
		{ }
	}
}
