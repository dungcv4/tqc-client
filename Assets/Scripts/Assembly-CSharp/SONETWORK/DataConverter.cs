using Cpp2IlInjected;

namespace SONETWORK
{
	// Source: dump.cs / Cpp2IL diffable signatures for SONETWORK.DataConverter.
	// Ghidra .c NOT pre-generated for these methods (too small / not in batch decompile output).
	// Bodies ported 1-1 from RAW ARM64 disassembly of libil2cpp.so:
	//
	//   RVA 0x1974280  DataConverter.writeUInt16(ushort v, byte[] dest, int offset):
	//     strb  w0, [x8, #0x20]    ; dest[offset]     = v & 0xff
	//     lsr   w9, w0, #8
	//     strb  w9, [x8, #0x20]    ; dest[offset+1]   = (v >> 8) & 0xff
	//     → manual little-endian byte shifts (NOT BitConverter.GetBytes — that's SToBA's pattern).
	//
	//   RVA 0x1974320  DataConverter.readUInt16(byte[] data, int offset):
	//     same shape inverse — reads low byte then high byte and ORs together.
	//
	//   RVA 0x197733c  DataConverter.writeInt32: 4× strb cascade with >>8/>>16/>>24 shifts.
	//   readInt32/Int64/UInt64/etc. follow identical pattern by extension.
	//
	// Float/Double: ARM64 inlines via FMOV+STR pairs (IEEE-754 bit pattern, host-endian).
	// Using BitConverter.GetBytes for these matches IL2CPP behavior on ARM64 (LE) and Mac/Win
	// editor (also LE). The IsLittleEndian guard preserves correctness on the unlikely big-endian
	// host. The original APK targets ARM (LE only); BE guard is defensive, not a divergence.
	//
	// Without this port, ALL ushort/uint protocol fields wrote as zeros at runtime, causing
	// every outbound packet (CHECKACC, GETCHARLIST, CHECKMARS, etc.) to be 12+ bytes of zeros
	// → mock_server.py logged proto=0 size=0 → login flow stuck in ConnectStep.SecretLogin_OK.
	public sealed class DataConverter
	{
		// ─── Read (little-endian) ──────────────────────────────────────────

		public static short readInt16(byte[] data, int offset)
		{
			return (short)((data[offset]) | (data[offset + 1] << 8));
		}

		public static ushort readUInt16(byte[] data, int offset)
		{
			return (ushort)((data[offset]) | (data[offset + 1] << 8));
		}

		public static int readInt32(byte[] data, int offset)
		{
			return (int)((uint)data[offset]
				| ((uint)data[offset + 1] << 8)
				| ((uint)data[offset + 2] << 16)
				| ((uint)data[offset + 3] << 24));
		}

		public static uint readUInt32(byte[] data, int offset)
		{
			return (uint)data[offset]
				| ((uint)data[offset + 1] << 8)
				| ((uint)data[offset + 2] << 16)
				| ((uint)data[offset + 3] << 24);
		}

		public static long readInt64(byte[] data, int offset)
		{
			return (long)((ulong)data[offset]
				| ((ulong)data[offset + 1] << 8)
				| ((ulong)data[offset + 2] << 16)
				| ((ulong)data[offset + 3] << 24)
				| ((ulong)data[offset + 4] << 32)
				| ((ulong)data[offset + 5] << 40)
				| ((ulong)data[offset + 6] << 48)
				| ((ulong)data[offset + 7] << 56));
		}

		public static ulong readUInt64(byte[] data, int offset)
		{
			return (ulong)data[offset]
				| ((ulong)data[offset + 1] << 8)
				| ((ulong)data[offset + 2] << 16)
				| ((ulong)data[offset + 3] << 24)
				| ((ulong)data[offset + 4] << 32)
				| ((ulong)data[offset + 5] << 40)
				| ((ulong)data[offset + 6] << 48)
				| ((ulong)data[offset + 7] << 56);
		}

		public static float readFloat(byte[] data, int offset)
		{
			// Use BitConverter for IEEE-754 bit pattern; bytes themselves are LE from caller.
			byte[] tmp = new byte[4];
			tmp[0] = data[offset];
			tmp[1] = data[offset + 1];
			tmp[2] = data[offset + 2];
			tmp[3] = data[offset + 3];
			if (!System.BitConverter.IsLittleEndian) System.Array.Reverse(tmp);
			return System.BitConverter.ToSingle(tmp, 0);
		}

		public static double readDouble(byte[] data, int offset)
		{
			byte[] tmp = new byte[8];
			for (int i = 0; i < 8; i++) tmp[i] = data[offset + i];
			if (!System.BitConverter.IsLittleEndian) System.Array.Reverse(tmp);
			return System.BitConverter.ToDouble(tmp, 0);
		}

		// ─── Write (little-endian) ─────────────────────────────────────────

		public static void writeInt16(short v, byte[] dest, int offset)
		{
			dest[offset]     = (byte)(v & 0xff);
			dest[offset + 1] = (byte)((v >> 8) & 0xff);
		}

		public static void writeUInt16(ushort v, byte[] dest, int offset)
		{
			dest[offset]     = (byte)(v & 0xff);
			dest[offset + 1] = (byte)((v >> 8) & 0xff);
		}

		public static void writeInt32(int v, byte[] dest, int offset)
		{
			dest[offset]     = (byte)(v & 0xff);
			dest[offset + 1] = (byte)((v >> 8) & 0xff);
			dest[offset + 2] = (byte)((v >> 16) & 0xff);
			dest[offset + 3] = (byte)((v >> 24) & 0xff);
		}

		public static void writeUInt32(uint v, byte[] dest, int offset)
		{
			dest[offset]     = (byte)(v & 0xff);
			dest[offset + 1] = (byte)((v >> 8) & 0xff);
			dest[offset + 2] = (byte)((v >> 16) & 0xff);
			dest[offset + 3] = (byte)((v >> 24) & 0xff);
		}

		public static void writeInt64(long v, byte[] dest, int offset)
		{
			dest[offset]     = (byte)(v & 0xff);
			dest[offset + 1] = (byte)((v >> 8) & 0xff);
			dest[offset + 2] = (byte)((v >> 16) & 0xff);
			dest[offset + 3] = (byte)((v >> 24) & 0xff);
			dest[offset + 4] = (byte)((v >> 32) & 0xff);
			dest[offset + 5] = (byte)((v >> 40) & 0xff);
			dest[offset + 6] = (byte)((v >> 48) & 0xff);
			dest[offset + 7] = (byte)((v >> 56) & 0xff);
		}

		public static void writeUInt64(ulong v, byte[] dest, int offset)
		{
			dest[offset]     = (byte)(v & 0xff);
			dest[offset + 1] = (byte)((v >> 8) & 0xff);
			dest[offset + 2] = (byte)((v >> 16) & 0xff);
			dest[offset + 3] = (byte)((v >> 24) & 0xff);
			dest[offset + 4] = (byte)((v >> 32) & 0xff);
			dest[offset + 5] = (byte)((v >> 40) & 0xff);
			dest[offset + 6] = (byte)((v >> 48) & 0xff);
			dest[offset + 7] = (byte)((v >> 56) & 0xff);
		}

		public static void writeFloat(float v, byte[] dest, int offset)
		{
			byte[] tmp = System.BitConverter.GetBytes(v);
			if (!System.BitConverter.IsLittleEndian) System.Array.Reverse(tmp);
			dest[offset]     = tmp[0];
			dest[offset + 1] = tmp[1];
			dest[offset + 2] = tmp[2];
			dest[offset + 3] = tmp[3];
		}

		public static void writeDouble(double v, byte[] dest, int offset)
		{
			byte[] tmp = System.BitConverter.GetBytes(v);
			if (!System.BitConverter.IsLittleEndian) System.Array.Reverse(tmp);
			for (int i = 0; i < 8; i++) dest[offset + i] = tmp[i];
		}

		// ─── convertBytes (allocate fresh byte[]) ──────────────────────────

		public static byte[] convertBytes(short v)
		{
			byte[] o = new byte[2];
			writeInt16(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(ushort v)
		{
			byte[] o = new byte[2];
			writeUInt16(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(int v)
		{
			byte[] o = new byte[4];
			writeInt32(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(uint v)
		{
			byte[] o = new byte[4];
			writeUInt32(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(long v)
		{
			byte[] o = new byte[8];
			writeInt64(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(ulong v)
		{
			byte[] o = new byte[8];
			writeUInt64(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(float v)
		{
			byte[] o = new byte[4];
			writeFloat(v, o, 0);
			return o;
		}

		public static byte[] convertBytes(double v)
		{
			byte[] o = new byte[8];
			writeDouble(v, o, 0);
			return o;
		}

		// ─── String helpers (lower-priority, ported when needed) ───────────

		public static string hexStr(byte[] data, int offset, int size)
		{
			return hexStr(data, offset, size, "X2");
		}

		public static string hexStr(byte[] data, int offset, int size, string format)
		{
			var sb = new System.Text.StringBuilder(size * 2);
			for (int i = 0; i < size; i++) sb.Append(data[offset + i].ToString(format));
			return sb.ToString();
		}

		public static string generateMD5Digest(byte[] data)
		{
			using (var md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] hash = md5.ComputeHash(data);
				return hexStr(hash, 0, hash.Length, "x2");
			}
		}

		public DataConverter()
		{ }
	}
}
