using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	public sealed class ZStream
	{
		private const int MAX_WBITS = 15;

		private static readonly int DEF_WBITS;

		private const int Z_NO_FLUSH = 0;

		private const int Z_PARTIAL_FLUSH = 1;

		private const int Z_SYNC_FLUSH = 2;

		private const int Z_FULL_FLUSH = 3;

		private const int Z_FINISH = 4;

		private const int MAX_MEM_LEVEL = 9;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		public byte[] next_in;

		public int next_in_index;

		public int avail_in;

		public long total_in;

		public byte[] next_out;

		public int next_out_index;

		public int avail_out;

		public long total_out;

		public string msg;

		internal Deflate dstate;

		internal Inflate istate;

		internal int data_type;

		public long adler;

		internal Adler32 _adler;

		public int inflateInit()
		{ return default; }

		public int inflateInit(int w)
		{ return default; }

		public int inflate(int f)
		{ return default; }

		public int inflateEnd()
		{ return default; }

		public int inflateSync()
		{ return default; }

		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{ return default; }

		public int deflateInit(int level)
		{ return default; }

		public int deflateInit(int level, int bits)
		{ return default; }

		public int deflate(int flush)
		{ return default; }

		public int deflateEnd()
		{ return default; }

		public int deflateParams(int level, int strategy)
		{ return default; }

		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{ return default; }

		internal void flush_pending()
		{ }

		internal int read_buf(byte[] buf, int start, int size)
		{ return default; }

		public void free()
		{ }

		public ZStream()
		{ }

		static ZStream()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
