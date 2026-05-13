using Cpp2IlInjected;

namespace SONETWORK
{
	public struct napiPACKET
	{
		public ushort m_npSize;

		public ushort m_npMsgCount;

		public ushort m_npCheckSum;

		public const int SIZE = 6;

		public const int MAX_PACKET_LEN = 65000;

		public const int MAX_PACKET_LEN_TOTAL = 65006;

		public const int PACKAGE_TEMP_SIZE = 65006;

		public void readFromByteArray(byte[] data, int offset)
		{ }

		public void writeToByteArray(byte[] data, int offset)
		{ }

		public static int getBlockCheckSum(byte[] data, int offset, int size)
		{ return default; }
	}
}
