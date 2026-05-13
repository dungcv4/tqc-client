using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class Tree
	{
		private const int MAX_BITS = 15;

		private const int BL_CODES = 19;

		private const int D_CODES = 30;

		private const int LITERALS = 256;

		private const int LENGTH_CODES = 29;

		private static readonly int L_CODES;

		private static readonly int HEAP_SIZE;

		internal const int MAX_BL_BITS = 7;

		internal const int END_BLOCK = 256;

		internal const int REP_3_6 = 16;

		internal const int REPZ_3_10 = 17;

		internal const int REPZ_11_138 = 18;

		internal static readonly int[] extra_lbits;

		internal static readonly int[] extra_dbits;

		internal static readonly int[] extra_blbits;

		internal static readonly byte[] bl_order;

		internal const int Buf_size = 16;

		internal const int DIST_CODE_LEN = 512;

		internal static readonly byte[] _dist_code;

		internal static readonly byte[] _length_code;

		internal static readonly int[] base_length;

		internal static readonly int[] base_dist;

		internal short[] dyn_tree;

		internal int max_code;

		internal StaticTree stat_desc;

		internal static int d_code(int dist)
		{ return default; }

		internal void gen_bitlen(Deflate s)
		{ }

		internal void build_tree(Deflate s)
		{ }

		internal static void gen_codes(short[] tree, int max_code, short[] bl_count)
		{ }

		internal static int bi_reverse(int code, int len)
		{ return default; }

		public Tree()
		{ }

		static Tree()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
