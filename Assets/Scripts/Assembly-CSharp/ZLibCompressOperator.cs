// Source: Ghidra work/06_ghidra/decompiled_full/ZLibCompressOperator/ (5 .c files, 1-1)
// Field offsets: _outStream@0x10 (Stream), _z@0x18 (ZStream), _buf@0x20 (byte[1024])
// DAT_008e3b48 = packed long with low32=0 (next_out_index), high32=0x400 (avail_out) — buffer reset.
// ZStream fields at offsets: next_in@0x10, next_in_index@0x18, avail_in@0x1c, total_in@0x20,
//   next_out@0x28, next_out_index@0x30, avail_out@0x34, total_out@0x38.

using System.IO;
using ComponentAce.Compression.Libs.zlib;
using Cpp2IlInjected;

public sealed class ZLibCompressOperator
{
	private const int BUFFER_SIZE = 1024;

	private Stream _outStream;
	private ZStream _z;
	private byte[] _buf;

	// Source: Ghidra get_TotalIn.c RVA 0x1a0bf84
	public long TotalIn { get { if (_z == null) throw new System.NullReferenceException(); return _z.total_in; } }

	// Source: Ghidra get_TotalOut.c RVA 0x1a0bfa0
	public long TotalOut { get { if (_z == null) throw new System.NullReferenceException(); return _z.total_out; } }

	// Source: Ghidra .ctor.c RVA 0x1a0bcfc (1-arg) — delegates to (outStream, Z_DEFAULT_COMPRESSION=-1).
	public ZLibCompressOperator(Stream outStream) : this(outStream, -1)
	{
	}

	// Source: Ghidra .ctor.c RVA 0x1a0bdd0
	// 1-1: _z = new ZStream(); _buf = new byte[1024]; base..ctor(); _outStream = outStream;
	//      _z.deflateInit(level);
	public ZLibCompressOperator(Stream outStream, int level)
	{
		_z = new ZStream();
		_buf = new byte[BUFFER_SIZE];
		_outStream = outStream;
		_z.deflateInit(level);
	}

	// Source: Ghidra Finalize.c RVA 0x1a0beb0 — deflateEnd + free; base.Finalize.
	~ZLibCompressOperator()
	{
		if (_z != null)
		{
			_z.deflateEnd();
			_z.free();
			_z = null;
		}
	}

	// Source: Ghidra Write.c RVA 0x1a0bfbc
	// 1-1 with deflate loop:
	//   if (len == 0) return true;
	//   _z.next_in = b1; _z.next_in_index = off; _z.avail_in = len;
	//   _z.next_out = _buf;
	//   do {
	//     _z.next_out_index = 0; _z.avail_out = BUFFER_SIZE;        (packed DAT_008e3b48)
	//     int err = _z.deflate(Z_NO_FLUSH=0);
	//     if (err > Z_STREAM_END=1) return err < 2;   (only OK==0 passes; >1 means error)
	//     int writeLen = BUFFER_SIZE - _z.avail_out;
	//     if (writeLen > 0) _outStream.Write(_buf, 0, writeLen);
	//     if (_z.avail_in < 1 && _z.avail_out != 0) return err < 2;
	//   } while (true);
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
			int err = _z.deflate(0);  // Z_NO_FLUSH
			if (err > 1) return err < 2;  // Per Ghidra: only OK(0)/STREAM_END(1) acceptable; >1 is error
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

	// Source: Ghidra Finish.c RVA 0x1a0c0b8
	// 1-1: _z.next_in = null; _z.next_in_index = 0; _z.next_out = _buf;
	//      do {
	//        _z.next_out_index = 0; _z.avail_out = BUFFER_SIZE;
	//        int err = _z.deflate(Z_FINISH=4);
	//        if (err > 1) return false;
	//        int writeLen = BUFFER_SIZE - _z.avail_out;
	//        if (writeLen > 0) _outStream.Write(_buf, 0, writeLen);
	//        if (_z.avail_in < 1 && _z.avail_out != 0) {
	//          int rc = _z.deflateEnd();
	//          if (rc != 0) return false;
	//          _outStream.Flush();
	//          return true;
	//        }
	//        _z.next_out = _buf;
	//      } while (true);
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
			int err = _z.deflate(4);  // Z_FINISH
			if (err > 1) return false;
			int writeLen = BUFFER_SIZE - _z.avail_out;
			if (writeLen > 0)
			{
				if (_outStream == null) throw new System.NullReferenceException();
				_outStream.Write(_buf, 0, writeLen);
			}
			if (_z.avail_in < 1 && _z.avail_out != 0)
			{
				int rc = _z.deflateEnd();
				if (rc != 0) return false;
				if (_outStream != null) _outStream.Flush();
				return true;
			}
			_z.next_out = _buf;
		}
	}
}
