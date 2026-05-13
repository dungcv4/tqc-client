using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class InfCodes
	{
		private static readonly int[] inflate_mask;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int START = 0;

		private const int LEN = 1;

		private const int LENEXT = 2;

		private const int DIST = 3;

		private const int DISTEXT = 4;

		private const int COPY = 5;

		private const int LIT = 6;

		private const int WASH = 7;

		private const int END = 8;

		private const int BADCODE = 9;

		internal int mode;

		internal int len;

		internal int[] tree;

		internal int tree_index;

		internal int need;

		internal int lit;

		internal int get_Renamed;

		internal int dist;

		internal byte lbits;

		internal byte dbits;

		internal int[] ltree;

		internal int ltree_index;

		internal int[] dtree;

		internal int dtree_index;

		internal InfCodes(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, ZStream z)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal InfCodes(int bl, int bd, int[] tl, int[] td, ZStream z)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		internal int proc(InfBlocks s, ZStream z, int r)
		{ return default; }

		internal void free(ZStream z)
		{ }

		internal int inflate_fast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InfBlocks s, ZStream z)
		{ return default; }

		static InfCodes()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
