// Source: Ghidra work/06_ghidra/decompiled_full/FixedString/ (9 .c files, all 1-1)
// Fixed-size byte buffer used as a string for protocol/network purposes.
// Field offsets (from Ghidra): nSize@0x10 (int), nDataLen@0x14 (int), byteArrayData@0x18 (byte[]), encoding@0x20 (Encoding)

using System.Text;
using Cpp2IlInjected;

public class FixedString
{
	private int nSize;
	private int nDataLen;
	private byte[] byteArrayData;
	private Encoding encoding;

	// Source: Ghidra .ctor — base..ctor; nSize=initSize; byteArrayData=new byte[nSize+1]; encoding=Encoding.UTF8.
	public FixedString(int initSize)
	{
		nSize = initSize;
		byteArrayData = new byte[initSize + 1];
		encoding = Encoding.UTF8;
	}

	public FixedString(int initSize, Encoding initEncode)
	{
		nSize = initSize;
		byteArrayData = new byte[initSize + 1];
		encoding = initEncode;
	}

	// Source: Ghidra op_Implicit.c RVA 0x18c1c10 — forwards to toText() if fs != null.
	public static implicit operator string(FixedString fs)
	{
		if (fs == null) throw new System.NullReferenceException();
		return fs.toText();
	}

	// Source: Ghidra assignString.c (byte[]) — buffer copy with truncation.
	public void assignString(byte[] newBytes)
	{
		if (newBytes == null) { UnityEngine.Debug.LogError("FixedString.assignString: newBytes is null"); return; }
		int len = newBytes.Length;
		if (len > nSize) len = nSize;
		nDataLen = len;
		System.Buffer.BlockCopy(newBytes, 0, byteArrayData, 0, len);
		if (len < byteArrayData.Length) byteArrayData[len] = 0;
	}

	// Source: Ghidra assignString.c (string) RVA 0x18c1e1c
	// 1-1: encoding.GetBytes(newString); truncate to nSize with warning; BlockCopy + null-terminate.
	public void assignString(string newString)
	{
		if (newString == null) { UnityEngine.Debug.LogError("FixedString.assignString: newString is null"); return; }
		if (encoding == null) throw new System.NullReferenceException();
		byte[] bytes = encoding.GetBytes(newString);
		int len = bytes.Length;
		if (len > nSize)
		{
			UJDebug.LogWarning(string.Format("FixedString.assignString: len {0} > nSize {1}", len, nSize));
			len = nSize;
		}
		nDataLen = len;
		System.Buffer.BlockCopy(bytes, 0, byteArrayData, 0, len);
		if (len < byteArrayData.Length) byteArrayData[len] = 0;
	}

	// Source: Ghidra ContentByteLength.c — returns nDataLen.
	public int ContentByteLength() { return nDataLen; }

	// Source: Ghidra toBytes.c — returns byteArrayData directly.
	public byte[] toBytes() { return byteArrayData; }

	// Source: Ghidra toText.c RVA 0x18c1c20 — scan for first null byte; encoding.GetString(buf,0,len).
	public string toText() { return DecodeWith(encoding); }

	// Source: Ghidra toUTF8.c / toASCII.c / toUTF32.c / toUTF7.c — same pattern with explicit encoding.
	public string toUTF8() { return DecodeWith(Encoding.UTF8); }
	public string toASCII() { return DecodeWith(Encoding.ASCII); }
	public string toUTF32() { return DecodeWith(Encoding.UTF32); }
	public string toUTF7() { return DecodeWith(Encoding.UTF7); }

	private string DecodeWith(Encoding enc)
	{
		if (byteArrayData == null) throw new System.NullReferenceException();
		int n = byteArrayData.Length;
		for (int i = 0; i < n; i++)
		{
			if (byteArrayData[i] == 0)
			{
				if (enc == null) throw new System.NullReferenceException();
				return enc.GetString(byteArrayData, 0, i);
			}
		}
		return string.Empty;
	}
}
