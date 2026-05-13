using System.IO;
using ComponentAce.Compression.Libs.zlib;
using Cpp2IlInjected;

public sealed class ZLibCompressOperator
{
	private const int BUFFER_SIZE = 1024;

	private Stream _outStream;

	private ZStream _z;

	private byte[] _buf;

	public long TotalIn
	{
		get
		{ return default; }
	}

	public long TotalOut
	{
		get
		{ return default; }
	}

	public ZLibCompressOperator(Stream outStream)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public ZLibCompressOperator(Stream outStream, int level)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	~ZLibCompressOperator()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool Write(byte[] b1, int off, int len)
	{ return default; }

	public bool Finish()
	{ return default; }
}
