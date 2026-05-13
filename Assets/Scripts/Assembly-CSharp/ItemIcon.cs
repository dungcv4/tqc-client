using System.Collections;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public enum STATE
	{
		Unknown = 0,
		Empty = 1,
		Normal = 2,
		DISABLE = 3
	}

	public delegate void ItemIconClickEvent(ItemIcon item);

	public delegate void ItemIconDragEvent(ItemIcon item, PointerEventData eventData);

	public ItemIconClickEvent OnClickIcon;

	public ItemIconClickEvent OnEnterIcon;

	public ItemIconClickEvent OnExitIcon;

	public ItemIconDragEvent OnBeginDragIcon;

	public ItemIconDragEvent OnDragIcon;

	public ItemIconDragEvent OnEndDragIcon;

	public ItemIconDragEvent OnDropIcon;

	private TweenScale getTween;

	[SerializeField]
	private RawImage _itemIcon;

	[SerializeField]
	private Image _itemIconFrame;

	[SerializeField]
	private Text _itemCount;

	[SerializeField]
	private Image _lockImage;

	private ArrayList tipArgs;

	public int tipID;

	private int _itemNum;

	private int _itemID;

	private int _index;

	private Rect _adjustUV;

	private Rect _normalUV;

	private TextureLoadHandler _iconTexHandler;

	private string _textureName;

	[SerializeField]
	private RectTransform _rectTransform;

	private object _userData;

	public bool needSelectFx;

	public bool needAutoItemTip;

	public bool needLoliInfoWnd;

	public Image IconFrame
	{
		get
		{ return default; }
	}

	public RectTransform rectTransform
	{
		get
		{ return default; }
	}

	public int itemID
	{
		get
		{ return default; }
	}

	public int itemCount
	{
		get
		{ return default; }
	}

	public int index
	{
		get
		{ return default; }
		set
		{ }
	}

	public object userData
	{
		get
		{ return default; }
		set
		{ }
	}

	private void OnDestroy()
	{ }

	private void _setGrayMask(bool status)
	{ }

	private void _setIconEmpty()
	{ }

	public void SetData(int itemID, int itemCount, string _name)
	{ }

	public void _setData(int id, int count, string _name)
	{ }

	private void _setItemIcon()
	{ }

	public void SetLock(bool flag)
	{ }

	public void SetGrayMask(bool on, bool redNum = true)
	{ }

	public void OnPointerClick(PointerEventData eventData)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/ItemIcon___ctor.c RVA 0x018C7320
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public ItemIcon()
	{
	}
}
