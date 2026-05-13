using System.Collections.Generic;
using Cpp2IlInjected;

public static class AStarHelper
{
	public enum AStarResult
	{
		AR_SUCCESS = 0,
		AR_FAILED_TIMEOUT = 1,
		AR_FAILED_NOPATH = 2,
		AR_FAILED_ISBLOCK = 3
	}

	public static AStarResult Result;

	public static bool Invalid<T>(T inNode) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private static float Distance<T>(T start, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private static float HeuristicCostEstimate<T>(T start, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private static T LowestScore<T>(List<T> openset, Dictionary<T, float> scores) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static List<T> SimulateCalculate<T>(T start, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static List<T> Calculate<T>(T start, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private static T getshortestFN<T>(List<T> openset, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public static List<T> CalculateStraight<T>(T start, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private static void ReconstructPath<T>(Dictionary<T, T> came_from, T current_node, ref List<T> result) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	private static void ResetAllData<T>(List<T> opSet, List<T> clSet, T goal) where T : IPathNode<T>
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
