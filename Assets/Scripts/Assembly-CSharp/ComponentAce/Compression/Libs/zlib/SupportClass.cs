using System.IO;
using Cpp2IlInjected;

namespace ComponentAce.Compression.Libs.zlib
{
	public class SupportClass
	{
		public static long Identity(long literal)
		{ return default; }

		public static ulong Identity(ulong literal)
		{ return default; }

		public static float Identity(float literal)
		{ return default; }

		public static double Identity(double literal)
		{ return default; }

		public static int URShift(int number, int bits)
		{ return default; }

		public static int URShift(int number, long bits)
		{ return default; }

		public static long URShift(long number, int bits)
		{ return default; }

		public static long URShift(long number, long bits)
		{ return default; }

		public static int ReadInput(Stream sourceStream, byte[] target, int start, int count)
		{ return default; }

		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{ return default; }

		public static byte[] ToByteArray(string sourceString)
		{ return default; }

		public static char[] ToCharArray(byte[] byteArray)
		{ return default; }

		public SupportClass()
		{ }
	}
}
