using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
	private SlotData SlotInfo;

	[NoToLua]
	public AdvImage _Icon;

	[NoToLua]
	public Image _CDFrame;

	[NoToLua]
	public Text _itemCount;

	[NoToLua]
	public Text _RightTopText;

	[NoToLua]
	public UIImagePicker ColorFrame;

	[NoToLua]
	public GameObject _lock;

	[NoToLua]
	public UIImagePicker _LeftBottomPic;

	[NoToLua]
	public UIImagePicker _LeftTopPic;

	[NoToLua]
	public UIImagePicker _RightTopPic;

	[NoToLua]
	public UIImageColorPicker _RestrictColor;

	[NoToLua]
	public GameObject _Effect1;

	[NoToLua]
	public GameObject _Effect2;

	[NoToLua]
	public GameObject _Effect3;

	private float TotalCDTime;

	private float remainCDTime;

	private AssetBundleRequest _IconRequest;

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	private void Update()
	{ }

	public SlotData GetSlotData()
	{ return default; }

	public void ClearSlot()
	{ }

	private void UpdateCoolDown()
	{ }

	public void SetCoolDown(float remainTime, float totalTime)
	{ }

	public void SetSlotDataLua(SlotData tempData, int colorTag, int nMode)
	{ }

	public void SetDark(bool bDark)
	{ }

	public void SetGray(bool bgray)
	{ }

	public void SetLock(bool bLock)
	{ }

	public void SetCount(string strCountText, int nCountNum)
	{ }

	public void SetRightTopText(string strText)
	{ }

	public void SetLeftBottomPic(string tagName)
	{ }

	public void SetLeftTopPic(string tagName)
	{ }

	public void SetRightTopPic(string tagName)
	{ }

	public void SetRestrictColorTag(string tagName)
	{ }

	public void SetEffect1(bool bShow)
	{ }

	public void SetEffect2(bool bShow)
	{ }

	public void SetEffect3(bool bShow)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot___ctor.c RVA 0x01A04324
	// 1-1: just base.ctor — no field init.
	public UISlot()
	{
	}
}
