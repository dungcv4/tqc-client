using UnityEngine;

internal interface MapToolObject
{
	void createGridModel(GameObject parent, int x, int y);

	void onMouseClick();

	void destroy();
}
