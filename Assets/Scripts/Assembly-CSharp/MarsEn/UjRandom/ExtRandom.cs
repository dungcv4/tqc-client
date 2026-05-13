using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsEn.UjRandom
{
	public class ExtRandom<T>
	{
		public static bool SplitChance()
		{ return default; }

		public static bool Chance(int nProbabilityFactor, int nProbabilitySpace)
		{ return default; }

		public static T Choice(T[] array)
		{ return default; }

		public static T Choice(List<T> list)
		{ return default; }

		public static T WeightedChoice(T[] array, int[] nWeights)
		{ return default; }

		public static T WeightedChoice(List<T> list, int[] nWeights)
		{ return default; }

		public static T[] Shuffle(T[] array)
		{ return default; }

		public static List<T> Shuffle(List<T> list)
		{ return default; }

		public ExtRandom()
		{ }
	}
}
