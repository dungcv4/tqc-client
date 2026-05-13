using System.IO;
using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	public class ZInputStream : BinaryReader
	{
		protected ZStream z;

		protected int bufsize;

		protected int flush;

		protected byte[] buf;

		protected byte[] buf1;

		protected bool compress;

		internal Stream in_Renamed;

		internal bool nomoreinput;

		public virtual int FlushMode
		{
			get
			{ return default; }
			set
			{ }
		}

		public virtual long TotalIn
		{
			get
			{ return default; }
		}

		public virtual long TotalOut
		{
			get
			{ return default; }
		}

		internal void InitBlock()
		{ }

		public ZInputStream(Stream in_Renamed) : base(in_Renamed)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		// Source: dump.cs ZInputStream(Stream,int) RVA 0x1971E50; BinaryReader has no (Stream,int) ctor
		// IL pattern: BinaryReader.ctor(this, in_Renamed) then store level → field
		public ZInputStream(Stream in_Renamed, int level) : base(in_Renamed)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override int Read()
		{ return default; }

		public int read(byte[] b, int off, int len)
		{ return default; }

		public long skip(long n)
		{ return default; }

		public override void Close()
		{ }
	}
}
