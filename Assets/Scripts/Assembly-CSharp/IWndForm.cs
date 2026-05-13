using System.Collections;
using UnityEngine;

public interface IWndForm
{
	WndForm CreateWndForm(uint eWndFormID, ArrayList args = null, bool popup = false);

	void UpdateOrder(ref int depth);

	void TopNode(WndFormNode node);

	void RemoveNode(WndFormNode node);

	void CreateWndFormAsync(WndForm wnd, GameObject resObj, uint eWndFormID, ArrayList args, bool popup);
}
