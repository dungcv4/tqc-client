using System;
using System.Runtime.InteropServices;
using Cpp2IlInjected;

public static class TssSdk
{
	[StructLayout(LayoutKind.Sequential)]
	public class AntiDataInfo
	{
		public ushort anti_data_len;

		public IntPtr anti_data;

		public AntiDataInfo()
		{ }
	}

	public static bool Is64bit()
	{ return default; }

	public static bool Is32bit()
	{ return default; }
}
