// Source: Ghidra work/06_ghidra/decompiled_full/Global/HexToInt.c RVA 0x15b2c74
//       + Swap<__Il2CppFullySharedGenericType>.c RVA 0x1bfdf7c
// Static cctor — initializes GRAY_MASK. Ghidra didn't emit cctor.c; reconstructed default Color value.

using Cpp2IlInjected;
using UnityEngine;

public static class Global
{
	public const int AP_MAX = 999;
	public const int MONEY_MAX = 2000000000;
	public const int DIAMOND_MAX = 2000000000;
	public const int EXP_MAX = 2000000000;
	public const int ITEM_MAX_COUNT = 999;
	public const byte PLAYER_NAME_LENGTH = 8;
	public const byte PLAYER_ARMY_TEAM_MAX = 5;
	public const byte PLAYER_TEAM_MEMBER_NUM = 4;
	public const byte LOLI_FRAGMENT_NEED_NUM = 50;
	public const byte MESSAGE_CHAR_LENGTH = 40;

	public static Color GRAY_MASK;

	// Source: Ghidra HexToInt.c RVA 0x15b2c74
	// 1-1: '0'..'9' → 0..9; 'A'..'F' → 10..15; 'a'..'f' → 10..15; else 0.
	public static int HexToInt(char c)
	{
		ushort u = (ushort)c;
		if ((ushort)(u - 0x30) < 10) return u - 0x30;
		if ((ushort)(u - 0x41) < 6) return u - 0x37;
		if ((ushort)(u - 0x61) < 6) return u - 0x57;
		return 0;
	}

	// Source: Ghidra Swap<__Il2CppFullySharedGenericType>.c — standard generic swap.
	public static void Swap<T>(ref T lhs, ref T rhs)
	{
		T tmp = lhs;
		lhs = rhs;
		rhs = tmp;
	}

	// Source: Ghidra .cctor (not emitted) — GRAY_MASK is a serialized field-init constant.
	// Standard grayscale-mask color (Unity gray ≈ 0.5).
	static Global()
	{
		GRAY_MASK = new Color(0.5f, 0.5f, 0.5f, 1f);
	}
}
