// Source: Ghidra work/06_ghidra/decompiled_full/ItemIcon/ — generic item slot UI (icon + frame + count + lock).

using System.Collections;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public enum STATE { Unknown = 0, Empty = 1, Normal = 2, DISABLE = 3 }

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
	[SerializeField] private RawImage _itemIcon;
	[SerializeField] private Image _itemIconFrame;
	[SerializeField] private Text _itemCount;
	[SerializeField] private Image _lockImage;

	private ArrayList tipArgs;
	public int tipID;

	private int _itemNum;
	private int _itemID;
	private int _index;
	private Rect _adjustUV;
	private Rect _normalUV;
	private TextureLoadHandler _iconTexHandler;
	private string _textureName;
	[SerializeField] private RectTransform _rectTransform;
	private object _userData;

	public bool needSelectFx;
	public bool needAutoItemTip;
	public bool needLoliInfoWnd;

	public Image IconFrame { get { return _itemIconFrame; } }
	public RectTransform rectTransform { get { return _rectTransform != null ? _rectTransform : (_rectTransform = GetComponent<RectTransform>()); } }
	public int itemID { get { return _itemID; } }
	public int itemCount { get { return _itemNum; } }
	public int index { get { return _index; } set { _index = value; } }
	public object userData { get { return _userData; } set { _userData = value; } }

	private void OnDestroy()
	{
		OnClickIcon = null;
		OnEnterIcon = null;
		OnExitIcon = null;
		OnBeginDragIcon = null;
		OnDragIcon = null;
		OnEndDragIcon = null;
		OnDropIcon = null;
	}

	private void _setGrayMask(bool status)
	{
		if (_itemIcon != null) _itemIcon.color = status ? Color.gray : Color.white;
	}

	private void _setIconEmpty()
	{
		_itemID = 0;
		_itemNum = 0;
		if (_itemIcon != null) _itemIcon.enabled = false;
		if (_itemCount != null) _itemCount.text = "";
	}

	public void SetData(int itemID, int itemCount, string _name)
	{
		_setData(itemID, itemCount, _name);
	}

	public void _setData(int id, int count, string _name)
	{
		_itemID = id;
		_itemNum = count;
		_textureName = _name;
		if (id == 0) { _setIconEmpty(); return; }
		_setItemIcon();
		if (_itemCount != null) _itemCount.text = count > 1 ? count.ToString() : "";
	}

	private void _setItemIcon()
	{
		if (_itemIcon == null) return;
		_itemIcon.enabled = true;
		// Texture load via TextureLoadHandler — atlas-backed icon resolution.
		// Editor stub: just enable the icon component; texture binding handled by external loader.
	}

	public void SetLock(bool flag)
	{
		if (_lockImage != null) _lockImage.gameObject.SetActive(flag);
	}

	public void SetGrayMask(bool on, bool redNum = true)
	{
		_setGrayMask(on);
		if (redNum && _itemCount != null) _itemCount.color = on ? Color.red : Color.white;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClickIcon != null) OnClickIcon(this);
	}

	public ItemIcon() { }
}
