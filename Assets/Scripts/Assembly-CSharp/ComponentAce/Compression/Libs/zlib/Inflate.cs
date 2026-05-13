using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class Inflate
	{
		private const int MAX_WBITS = 15;

		private const int PRESET_DICT = 32;

		internal const int Z_NO_FLUSH = 0;

		internal const int Z_PARTIAL_FLUSH = 1;

		internal const int Z_SYNC_FLUSH = 2;

		internal const int Z_FULL_FLUSH = 3;

		internal const int Z_FINISH = 4;

		private const int Z_DEFLATED = 8;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int METHOD = 0;

		private const int FLAG = 1;

		private const int DICT4 = 2;

		private const int DICT3 = 3;

		private const int DICT2 = 4;

		private const int DICT1 = 5;

		private const int DICT0 = 6;

		private const int BLOCKS = 7;

		private const int CHECK4 = 8;

		private const int CHECK3 = 9;

		private const int CHECK2 = 10;

		private const int CHECK1 = 11;

		private const int DONE = 12;

		private const int BAD = 13;

		internal int mode;

		internal int method;

		internal long[] was;

		internal long need;

		internal int marker;

		internal int nowrap;

		internal int wbits;

		internal InfBlocks blocks;

		private static byte[] mark;

		internal int inflateReset(ZStream z)
		{ return default; }

		internal int inflateEnd(ZStream z)
		{ return default; }

		internal int inflateInit(ZStream z, int w)
		{ return default; }

		internal int inflate(ZStream z, int f)
		{ return default; }

		internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
		{ return default; }

		internal int inflateSync(ZStream z)
		{ return default; }

		internal int inflateSyncPoint(ZStream z)
		{ return default; }

		public Inflate()
		{ }

		static Inflate()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
