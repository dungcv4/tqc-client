using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	internal sealed class InfTree
	{
		private const int MANY = 1440;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		internal const int fixed_bl = 9;

		internal const int fixed_bd = 5;

		internal static readonly int[] fixed_tl;

		internal static readonly int[] fixed_td;

		internal static readonly int[] cplens;

		internal static readonly int[] cplext;

		internal static readonly int[] cpdist;

		internal static readonly int[] cpdext;

		internal const int BMAX = 15;

		internal static int huft_build(int[] b, int bindex, int n, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
		{ return default; }

		internal static int inflate_trees_bits(int[] c, int[] bb, int[] tb, int[] hp, ZStream z)
		{ return default; }

		internal static int inflate_trees_dynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp, ZStream z)
		{ return default; }

		internal static int inflate_trees_fixed(int[] bl, int[] bd, int[][] tl, int[][] td, ZStream z)
		{ return default; }

		public InfTree()
		{ }

		static InfTree()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
