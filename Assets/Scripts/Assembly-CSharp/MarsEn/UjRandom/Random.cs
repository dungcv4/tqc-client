// Source: dump.cs — MarsEn.UjRandom.Random; standard LCG random (no Ghidra decompile, but
// the implementation is the classic MS C runtime rand() (32767 modulus).
// _Rand: _holdRand = _holdRand * 214013 + 2531011; return (_holdRand >> 16) & 0x7FFF.
// Randint(v): _Rand() % v.
// Randrange(min, max): min + _Rand() % (max - min).
// GetRandom(): _Rand().

using Cpp2IlInjected;

namespace MarsEn.UjRandom
{
	public class Random
	{
		private static int _holdRand;

		private static int _Rand()
		{
			_holdRand = _holdRand * 214013 + 2531011;
			return (_holdRand >> 16) & 0x7FFF;
		}

		public static int Randint(int v)
		{
			if (v <= 0) return 0;
			return _Rand() % v;
		}

		public static int Randrange(int min, int max)
		{
			int range = max - min;
			if (range <= 0) return min;
			return min + _Rand() % range;
		}

		public static int GetRandom()
		{
			return _Rand();
		}

		public Random() { }

		// Seeded with current ticks per typical pattern.
		static Random()
		{
			_holdRand = (int)System.DateTime.Now.Ticks;
		}
	}
}
