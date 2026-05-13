using System.IO;
using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	public class ZOutputStream : Stream
	{
		protected internal ZStream z;

		protected internal int bufsize;

		protected internal int flush_Renamed_Field;

		protected internal byte[] buf;

		protected internal byte[] buf1;

		protected internal bool compress;

		private Stream out_Renamed;

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

		public override bool CanRead
		{
			get
			{ return default; }
		}

		public override bool CanSeek
		{
			get
			{ return default; }
		}

		public override bool CanWrite
		{
			get
			{ return default; }
		}

		public override long Length
		{
			get
			{ return default; }
		}

		public override long Position
		{
			get
			{ return default; }
			set
			{ }
		}

		private void InitBlock()
		{ }

		public ZOutputStream(Stream out_Renamed)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public ZOutputStream(Stream out_Renamed, int level)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void WriteByte(int b)
		{ }

		public override void WriteByte(byte b)
		{ }

		public override void Write(byte[] b1, int off, int len)
		{ }

		public virtual void finish()
		{ }

		public virtual void end()
		{ }

		public override void Close()
		{ }

		public override void Flush()
		{ }

		public override int Read(byte[] buffer, int offset, int count)
		{ return default; }

		public override void SetLength(long value)
		{ }

		public override long Seek(long offset, SeekOrigin origin)
		{ return default; }
	}
}
