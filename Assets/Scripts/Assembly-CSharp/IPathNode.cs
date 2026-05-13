using System.Collections.Generic;
using UnityEngine;

public interface IPathNode<T>
{
	List<T> Connections { get; }

	Vector2 Position { get; }

	bool Invalid { get; }

	int invalidConNum { get; }

	float gn { get; set; }

	float fn { get; }

	float DistanceTo(T target);

	void sethn(T goal);
}
