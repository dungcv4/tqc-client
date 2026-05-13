using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	public sealed class Deflate
	{
		internal class Config
		{
			internal int good_length;

			internal int max_lazy;

			internal int nice_length;

			internal int max_chain;

			internal int func;

			internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private const int MAX_MEM_LEVEL = 9;

		private const int Z_DEFAULT_COMPRESSION = -1;

		private const int MAX_WBITS = 15;

		private const int DEF_MEM_LEVEL = 8;

		private const int STORED = 0;

		private const int FAST = 1;

		private const int SLOW = 2;

		private static Config[] config_table;

		private static readonly string[] z_errmsg;

		private const int NeedMore = 0;

		private const int BlockDone = 1;

		private const int FinishStarted = 2;

		private const int FinishDone = 3;

		private const int PRESET_DICT = 32;

		private const int Z_FILTERED = 1;

		private const int Z_HUFFMAN_ONLY = 2;

		private const int Z_DEFAULT_STRATEGY = 0;

		private const int Z_NO_FLUSH = 0;

		private const int Z_PARTIAL_FLUSH = 1;

		private const int Z_SYNC_FLUSH = 2;

		private const int Z_FULL_FLUSH = 3;

		private const int Z_FINISH = 4;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int INIT_STATE = 42;

		private const int BUSY_STATE = 113;

		private const int FINISH_STATE = 666;

		private const int Z_DEFLATED = 8;

		private const int STORED_BLOCK = 0;

		private const int STATIC_TREES = 1;

		private const int DYN_TREES = 2;

		private const int Z_BINARY = 0;

		private const int Z_ASCII = 1;

		private const int Z_UNKNOWN = 2;

		private const int Buf_size = 16;

		private const int REP_3_6 = 16;

		private const int REPZ_3_10 = 17;

		private const int REPZ_11_138 = 18;

		private const int MIN_MATCH = 3;

		private const int MAX_MATCH = 258;

		private static readonly int MIN_LOOKAHEAD;

		private const int MAX_BITS = 15;

		private const int D_CODES = 30;

		private const int BL_CODES = 19;

		private const int LENGTH_CODES = 29;

		private const int LITERALS = 256;

		private static readonly int L_CODES;

		private static readonly int HEAP_SIZE;

		private const int END_BLOCK = 256;

		internal ZStream strm;

		internal int status;

		internal byte[] pending_buf;

		internal int pending_buf_size;

		internal int pending_out;

		internal int pending;

		internal int noheader;

		internal byte data_type;

		internal byte method;

		internal int last_flush;

		internal int w_size;

		internal int w_bits;

		internal int w_mask;

		internal byte[] window;

		internal int window_size;

		internal short[] prev;

		internal short[] head;

		internal int ins_h;

		internal int hash_size;

		internal int hash_bits;

		internal int hash_mask;

		internal int hash_shift;

		internal int block_start;

		internal int match_length;

		internal int prev_match;

		internal int match_available;

		internal int strstart;

		internal int match_start;

		internal int lookahead;

		internal int prev_length;

		internal int max_chain_length;

		internal int max_lazy_match;

		internal int level;

		internal int strategy;

		internal int good_match;

		internal int nice_match;

		internal short[] dyn_ltree;

		internal short[] dyn_dtree;

		internal short[] bl_tree;

		internal Tree l_desc;

		internal Tree d_desc;

		internal Tree bl_desc;

		internal short[] bl_count;

		internal int[] heap;

		internal int heap_len;

		internal int heap_max;

		internal byte[] depth;

		internal int l_buf;

		internal int lit_bufsize;

		internal int last_lit;

		internal int d_buf;

		internal int opt_len;

		internal int static_len;

		internal int matches;

		internal int last_eob_len;

		internal short bi_buf;

		internal int bi_valid;

		internal Deflate()
		{ }

		internal void lm_init()
		{ }

		internal void tr_init()
		{ }

		internal void init_block()
		{ }

		internal void pqdownheap(short[] tree, int k)
		{ }

		internal static bool smaller(short[] tree, int n, int m, byte[] depth)
		{ return default; }

		internal void scan_tree(short[] tree, int max_code)
		{ }

		internal int build_bl_tree()
		{ return default; }

		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{ }

		internal void send_tree(short[] tree, int max_code)
		{ }

		internal void put_byte(byte[] p, int start, int len)
		{ }

		internal void put_byte(byte c)
		{ }

		internal void put_short(int w)
		{ }

		internal void putShortMSB(int b)
		{ }

		internal void send_code(int c, short[] tree)
		{ }

		internal void send_bits(int value_Renamed, int length)
		{ }

		internal void _tr_align()
		{ }

		internal bool _tr_tally(int dist, int lc)
		{ return default; }

		internal void compress_block(short[] ltree, short[] dtree)
		{ }

		internal void set_data_type()
		{ }

		internal void bi_flush()
		{ }

		internal void bi_windup()
		{ }

		internal void copy_block(int buf, int len, bool header)
		{ }

		internal void flush_block_only(bool eof)
		{ }

		internal int deflate_stored(int flush)
		{ return default; }

		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{ }

		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{ }

		internal void fill_window()
		{ }

		internal int deflate_fast(int flush)
		{ return default; }

		internal int deflate_slow(int flush)
		{ return default; }

		internal int longest_match(int cur_match)
		{ return default; }

		internal int deflateInit(ZStream strm, int level, int bits)
		{ return default; }

		internal int deflateInit(ZStream strm, int level)
		{ return default; }

		internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
		{ return default; }

		internal int deflateReset(ZStream strm)
		{ return default; }

		internal int deflateEnd()
		{ return default; }

		internal int deflateParams(ZStream strm, int _level, int _strategy)
		{ return default; }

		internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
		{ return default; }

		internal int deflate(ZStream strm, int flush)
		{ return default; }

		static Deflate()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
