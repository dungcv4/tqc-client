using Cpp2IlInjected;

namespace MarsEn.UjRandom
{
	public class Random
	{
		private static int _holdRand;

		private static int _Rand()
		{ return default; }

		public static int Randint(int v)
		{ return default; }

		public static int Randrange(int min, int max)
		{ return default; }

		public static int GetRandom()
		{ return default; }

		public Random()
		{ }

		static Random()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
