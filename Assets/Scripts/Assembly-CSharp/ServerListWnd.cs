using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class ServerListWnd : MonoBehaviour
{
	[SerializeField]
	private GameObject _thisGobj;

	[SerializeField]
	private Button _cancelBtn;

	[SerializeField]
	private Button _tmpServerBtn;

	[SerializeField]
	private RectTransform _serverBtnListRect;

	private int _serverID;

	public int serverID
	{
		get
		{ return default; }
	}

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	private void setInitialServerList()
	{ }

	private void _onServerBtnClick(int id)
	{ }

	private void _onCancelBtnClick()
	{ }

	public void _setWndVisable(bool enable)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/ServerListWnd___ctor.c RVA 0x018F50D4
	// 1-1: just base.ctor — no field init.
	public ServerListWnd()
	{
	}
}
