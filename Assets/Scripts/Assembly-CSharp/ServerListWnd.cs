// Source: Ghidra work/06_ghidra/decompiled_full/ServerListWnd/ — simple server selection window.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class ServerListWnd : MonoBehaviour
{
	[SerializeField] private GameObject _thisGobj;
	[SerializeField] private Button _cancelBtn;
	[SerializeField] private Button _tmpServerBtn;
	[SerializeField] private RectTransform _serverBtnListRect;
	private int _serverID;

	public int serverID { get { return _serverID; } }

	private void Start()
	{
		if (_cancelBtn != null) _cancelBtn.onClick.AddListener(_onCancelBtnClick);
		setInitialServerList();
	}

	private void Update() { }

	private void setInitialServerList() { }

	private void _onServerBtnClick(int id)
	{
		_serverID = id;
		_setWndVisable(false);
	}

	private void _onCancelBtnClick()
	{
		_setWndVisable(false);
	}

	public void _setWndVisable(bool enable)
	{
		if (_thisGobj != null) _thisGobj.SetActive(enable);
	}

	public ServerListWnd() { }
}
