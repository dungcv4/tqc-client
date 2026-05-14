// Source: Ghidra work/06_ghidra/decompiled_full/ZLibUnCompressOperator/ (5 .c files, 1-1)
// Same shape as ZLibCompressOperator but uses inflate*** instead of deflate***.

using System.IO;
using ComponentAce.Compression.Libs.zlib;
using Cpp2IlInjected;

public sealed class ZLibUnCompressOperator
{
	private const int BUFFER_SIZE = 1024;

	private Stream _outStream;
	private ZStream _z;
	private byte[] _buf;

	// Source: Ghidra get_TotalIn.c
	public long TotalIn { get { if (_z == null) throw new System.NullReferenceException(); return _z.total_in; } }

	// Source: Ghidra get_TotalOut.c
	public long TotalOut { get { if (_z == null) throw new System.NullReferenceException(); return _z.total_out; } }

	// Source: Ghidra .ctor.c RVA 0x1a0c1d0
	public ZLibUnCompressOperator(Stream outStream)
	{
		_z = new ZStream();
		_buf = new byte[BUFFER_SIZE];
		_outStream = outStream;
		_z.inflateInit();
	}

	// Source: Ghidra Finalize.c — inflateEnd + free; base.Finalize.
	~ZLibUnCompressOperator()
	{
		if (_z != null)
		{
			_z.inflateEnd();
			_z.free();
			_z = null;
		}
	}

	// Source: Ghidra Write.c RVA 0x1a0c3ac — inflate-loop counterpart of compress Write.
	public bool Write(byte[] b1, int off, int len)
	{
		if (len == 0) return true;
		if (_z == null) throw new System.NullReferenceException();
		_z.next_in = b1;
		_z.next_in_index = off;
		_z.avail_in = len;
		_z.next_out = _buf;
		while (true)
		{
			_z.next_out_index = 0;
			_z.avail_out = BUFFER_SIZE;
			int err = _z.inflate(0);  // Z_NO_FLUSH
			if (err > 1) return err < 2;
			int writeLen = BUFFER_SIZE - _z.avail_out;
			if (writeLen > 0)
			{
				if (_outStream == null) throw new System.NullReferenceException();
				_outStream.Write(_buf, 0, writeLen);
			}
			if (_z.avail_in < 1 && _z.avail_out != 0) return err < 2;
			_z.next_out = _buf;
		}
	}

	// Source: Ghidra Finish.c RVA 0x1a0c4a8 — inflate(Z_FINISH=4) loop until done; inflateEnd + Flush.
	public bool Finish()
	{
		if (_z == null) throw new System.NullReferenceException();
		_z.next_in = null;
		_z.next_in_index = 0;
		_z.next_out = _buf;
		while (true)
		{
			_z.next_out_index = 0;
			_z.avail_out = BUFFER_SIZE;
			int err = _z.inflate(4);  // Z_FINISH
			if (err > 1) return false;
			int writeLen = BUFFER_SIZE - _z.avail_out;
			if (writeLen > 0)
			{
				if (_outStream == null) throw new System.NullReferenceException();
				_outStream.Write(_buf, 0, writeLen);
			}
			if (_z.avail_in < 1 && _z.avail_out != 0)
			{
				int rc = _z.inflateEnd();
				if (rc != 0) return false;
				if (_outStream != null) _outStream.Flush();
				return true;
			}
			_z.next_out = _buf;
		}
	}
}
