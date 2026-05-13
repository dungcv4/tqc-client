using Cpp2IlInjected;

namespace SONETWORK
{
	// Source: dump.cs / Cpp2IL Diffable (work/06_cpp2il/cs_out/.../napiPACKET.cs):
	//   struct fields: m_npSize@0, m_npMsgCount@2, m_npCheckSum@4 (all ushort)
	//   const SIZE=6, MAX_PACKET_LEN=65000, MAX_PACKET_LEN_TOTAL=65006, PACKAGE_TEMP_SIZE=65006
	//   3 methods: readFromByteArray, writeToByteArray, getBlockCheckSum
	// Ghidra: methods NOT decompiled to .c (likely inlined or too short). Structurally parallel
	// to SONETWORK.proto_COMM (which IS Ghidra-ported 1-1) — 3 ushort fields written via
	// DataConverter at offsets 0/2/4. proto_COMM differs by trailing blockEncrypt — napi header
	// does NOT encrypt (SangoPacketWriter.writeProtocol doesn't call blockEncrypt on the napi
	// header bytes after writeToByteArray; only proto_COMM header is encrypted by ConnectProxy.write).
	public struct napiPACKET
	{
		public ushort m_npSize;       // 0x0

		public ushort m_npMsgCount;   // 0x2

		public ushort m_npCheckSum;   // 0x4

		public const int SIZE = 6;

		public const int MAX_PACKET_LEN = 65000;

		public const int MAX_PACKET_LEN_TOTAL = 65006;

		public const int PACKAGE_TEMP_SIZE = 65006;

		// Source: structurally 1-1 with SONETWORK.proto_COMM.readFromByteArray
		// (work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/readFromByteArray.c RVA 0x1974614)
		// minus the blockEncrypt step (napi header is not XOR-scrambled — see writeProtocol comment).
		public void readFromByteArray(byte[] data, int offset)
		{
			this.m_npSize     = DataConverter.readUInt16(data, offset);
			this.m_npMsgCount = DataConverter.readUInt16(data, offset + 2);
			this.m_npCheckSum = DataConverter.readUInt16(data, offset + 4);
		}

		// Source: structurally 1-1 with SONETWORK.proto_COMM.writeToByteArray
		// (work/06_ghidra/decompiled_full/SONETWORK.proto_COMM/writeToByteArray.c RVA 0x1974844)
		// minus the blockEncrypt step. Without this port, the 6-byte napi header preceding every
		// outgoing packet was sent as zeros → server saw proto=0 size=0 → CHECKACC/GETCHARLIST etc.
		// were silently dropped server-side and the login flow hung in ConnectStep.SecretLogin_OK.
		public void writeToByteArray(byte[] data, int offset)
		{
			DataConverter.writeUInt16(this.m_npSize,     data, offset);
			DataConverter.writeUInt16(this.m_npMsgCount, data, offset + 2);
			DataConverter.writeUInt16(this.m_npCheckSum, data, offset + 4);
		}

		// Source: ARM64 disasm libil2cpp.so RVA 0x19744b0 + subroutine 0x19745a0 (= readInt32 inline).
		// Algorithm — weighted overlapping int32 accumulation:
		//   if size < 1 return 0
		//   sum = 0; mul = 1
		//   if size >= 4:
		//     for iter = 0; iter < size - 3; iter++:
		//       val = readInt32(data, offset + iter)   ; OVERLAPPING (offset increments by 1)
		//       sum += val * mul
		//       mul += 3
		//   // trailing handler: allocate 4-byte tmp, copy <4 remaining bytes (zero-padded),
		//   // val = readInt32(tmp, 0); sum += val * ((size*3) - 8)
		//   ; return sum
		// (The (size*3)-8 multiplier matches the loop's final mul value continuity.)
		public static int getBlockCheckSum(byte[] data, int offset, int size)
		{
			if (size < 1) return 0;
			int sum = 0;
			int mul = 1;
			int iter = 0;
			if (size >= 4)
			{
				int loopCount = size - 3;
				for (iter = 0; iter < loopCount; iter++)
				{
					int val = DataConverter.readInt32(data, offset + iter);
					sum = sum + val * mul;
					mul += 3;
				}
			}
			// Trailing bytes (size - iter < 4): pack into a zero-padded 4-byte buffer.
			// When size < 4, iter=0 and 'size' trailing bytes (1..3) get copied; remainder zeros.
			// When size >= 4, iter=size-3, leaving 3 bytes (offsets size-3, size-2, size-1)
			// — but wait, that 'trailing' would re-read overlap. ARM64 disasm shows the trailing
			// path is taken when size < 4 OR specifically at end. Re-reading:
			//   - if size < 4: skip loop, w24 := 1 (the final multiplier)
			//   - if size >= 4: loop runs, w24 := (size*3) - 8 (loop's last mul - increment)
			//   - both paths fall through to: alloc 4-byte tmp; Array.Copy(data, offset+iter, tmp, 0, ?);
			//     val = readInt32(tmp, 0); sum += val * w24
			// The trailing copy length appears to be `size - iter` capped at 4. When iter=size-3,
			// length = 3; when iter=0 (size<4 path), length = size.
			int tailMul = (size >= 4) ? ((size * 3) - 8) : 1;
			int tailLen = size - iter;
			if (tailLen > 4) tailLen = 4;
			if (tailLen < 0) tailLen = 0;
			byte[] tmp = new byte[4];
			if (tailLen > 0)
			{
				System.Array.Copy(data, offset + iter, tmp, 0, tailLen);
			}
			int tailVal = DataConverter.readInt32(tmp, 0);
			sum = sum + tailVal * tailMul;
			return sum;
		}
	}
}
