using Cpp2IlInjected;
using UnityEngine;

public struct MapConnectionPoint
{
	public ushort map_code;

	public ushort target_map_code;

	public Vector2 map_pos;

	public Vector2 target_map_pos;

	public float distance()
	{ return default; }
}
