// Source: standard A* pathfinding implementation matching IPathNode<T> interface contract.
// Original APK has the same body per il2cpp dump (AStarHelper$$Calculate<object>, etc.) — generic instantiations
// dispatch to identical IL. Reconstructed from algorithm + IPathNode<T> fields (gn/fn/sethn/DistanceTo/Connections/Invalid).

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
		if (inNode == null) return true;
		return inNode.Invalid;
	}

	private static float Distance<T>(T start, T goal) where T : IPathNode<T>
	{
		if (start == null || goal == null) return float.MaxValue;
		return start.DistanceTo(goal);
	}

	private static float HeuristicCostEstimate<T>(T start, T goal) where T : IPathNode<T>
	{
		return Distance(start, goal);
	}

	private static T LowestScore<T>(List<T> openset, Dictionary<T, float> scores) where T : IPathNode<T>
	{
		T best = default(T);
		float bestScore = float.MaxValue;
		foreach (T n in openset)
		{
			float s;
			if (scores.TryGetValue(n, out s) && s < bestScore)
			{
				bestScore = s;
				best = n;
			}
		}
		return best;
	}

	// Same algorithm as Calculate but does not commit gn/fn to nodes (simulation pass).
	public static List<T> SimulateCalculate<T>(T start, T goal) where T : IPathNode<T>
	{
		Result = AStarResult.AR_FAILED_NOPATH;
		if (Invalid(start) || Invalid(goal))
		{
			Result = AStarResult.AR_FAILED_ISBLOCK;
			return null;
		}

		var closedset = new HashSet<T>();
		var openset = new List<T> { start };
		var came_from = new Dictionary<T, T>();
		var g_score = new Dictionary<T, float> { [start] = 0f };
		var f_score = new Dictionary<T, float> { [start] = HeuristicCostEstimate(start, goal) };

		while (openset.Count > 0)
		{
			T current = LowestScore(openset, f_score);
			if (current == null) break;
			if (current.Equals(goal))
			{
				var path = new List<T>();
				ReconstructPath(came_from, current, ref path);
				Result = AStarResult.AR_SUCCESS;
				return path;
			}
			openset.Remove(current);
			closedset.Add(current);

			if (current.Connections == null) continue;
			foreach (T neighbor in current.Connections)
			{
				if (Invalid(neighbor) || closedset.Contains(neighbor)) continue;
				float tentative_g = g_score[current] + Distance(current, neighbor);
				bool inOpen = openset.Contains(neighbor);
				float existing;
				if (!inOpen || (g_score.TryGetValue(neighbor, out existing) && tentative_g < existing))
				{
					came_from[neighbor] = current;
					g_score[neighbor] = tentative_g;
					f_score[neighbor] = tentative_g + HeuristicCostEstimate(neighbor, goal);
					if (!inOpen) openset.Add(neighbor);
				}
			}
		}
		return null;
	}

	public static List<T> Calculate<T>(T start, T goal) where T : IPathNode<T>
	{
		Result = AStarResult.AR_FAILED_NOPATH;
		if (Invalid(start) || Invalid(goal))
		{
			Result = AStarResult.AR_FAILED_ISBLOCK;
			return null;
		}

		var closedset = new List<T>();
		var openset = new List<T> { start };
		var came_from = new Dictionary<T, T>();
		start.gn = 0f;
		start.sethn(goal);

		while (openset.Count > 0)
		{
			T current = getshortestFN(openset, goal);
			if (current == null) break;
			if (current.Equals(goal))
			{
				var path = new List<T>();
				ReconstructPath(came_from, current, ref path);
				ResetAllData(openset, closedset, goal);
				Result = AStarResult.AR_SUCCESS;
				return path;
			}
			openset.Remove(current);
			closedset.Add(current);

			if (current.Connections == null) continue;
			foreach (T neighbor in current.Connections)
			{
				if (Invalid(neighbor) || closedset.Contains(neighbor)) continue;
				float tentative_g = current.gn + Distance(current, neighbor);
				bool inOpen = openset.Contains(neighbor);
				if (!inOpen || tentative_g < neighbor.gn)
				{
					came_from[neighbor] = current;
					neighbor.gn = tentative_g;
					neighbor.sethn(goal);
					if (!inOpen) openset.Add(neighbor);
				}
			}
		}
		ResetAllData(openset, closedset, goal);
		return null;
	}

	private static T getshortestFN<T>(List<T> openset, T goal) where T : IPathNode<T>
	{
		T best = default(T);
		float bestFN = float.MaxValue;
		foreach (T n in openset)
		{
			if (n.fn < bestFN)
			{
				bestFN = n.fn;
				best = n;
			}
		}
		return best;
	}

	public static List<T> CalculateStraight<T>(T start, T goal) where T : IPathNode<T>
	{
		// Line-of-sight straight path: walks from start toward goal node-by-node via Connections,
		// picking the connection nearest the goal each step. Closure variable replicates
		// _003C_003Ec__DisplayClass9_0<T> closure in original.
		Result = AStarResult.AR_FAILED_NOPATH;
		if (Invalid(start) || Invalid(goal))
		{
			Result = AStarResult.AR_FAILED_ISBLOCK;
			return null;
		}
		var path = new List<T> { start };
		var visited = new HashSet<T> { start };
		T current = start;
		while (!current.Equals(goal))
		{
			if (current.Connections == null) break;
			T best = default(T);
			float bestDist = float.MaxValue;
			T captured_goal = goal;
			foreach (T n in current.Connections)
			{
				if (Invalid(n) || visited.Contains(n)) continue;
				float d = n.DistanceTo(captured_goal);
				if (d < bestDist) { bestDist = d; best = n; }
			}
			if (best == null) break;
			path.Add(best);
			visited.Add(best);
			current = best;
		}
		if (current.Equals(goal))
		{
			Result = AStarResult.AR_SUCCESS;
			return path;
		}
		return null;
	}

	private static void ReconstructPath<T>(Dictionary<T, T> came_from, T current_node, ref List<T> result) where T : IPathNode<T>
	{
		T prev;
		if (came_from.TryGetValue(current_node, out prev))
		{
			ReconstructPath(came_from, prev, ref result);
		}
		result.Add(current_node);
	}

	private static void ResetAllData<T>(List<T> opSet, List<T> clSet, T goal) where T : IPathNode<T>
	{
		// Original iterates both sets and resets gn/hn — but since IPathNode doesn't expose hn setter,
		// only gn is reset. sethn(goal) computes hn fresh next call anyway.
		if (opSet != null)
		{
			foreach (T n in opSet) if (n != null) n.gn = 0f;
		}
		if (clSet != null)
		{
			foreach (T n in clSet) if (n != null) n.gn = 0f;
		}
	}
}
