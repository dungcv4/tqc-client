using System.Text;
using Cpp2IlInjected;

public class FixedString
{
	private int nSize;

	private int nDataLen;

	private byte[] byteArrayData;

	private Encoding encoding;

	public FixedString(int initSize)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public FixedString(int initSize, Encoding initEncode)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static implicit operator string(FixedString fs)
	{ return default; }

	public void assignString(byte[] newBytes)
	{ }

	public void assignString(string newString)
	{ }

	public int ContentByteLength()
	{ return default; }

	public byte[] toBytes()
	{ return default; }

	public string toText()
	{ return default; }

	public string toUTF8()
	{ return default; }

	public string toASCII()
	{ return default; }

	public string toUTF32()
	{ return default; }

	public string toUTF7()
	{ return default; }
}
