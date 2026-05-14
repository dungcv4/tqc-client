using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Events/Button Controller")]
public class ButtonController : IWndComponent, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	private const uint BS_HOVER = 1u;

	private const uint BS_PRESS = 2u;

	private const uint BS_CHECK = 4u;

	private const uint BS_DISABLE = 8u;

	private const uint BS_PAUSE = 64u;

	private const uint BS_POPUP = 128u;

	private const uint BS_INIT = 256u;

	private WndForm _wnd;

	private uint _state;

	private WndAnimation[] _preAnim;

	private Color _orgTextColor;

	private Vector2 _orgTextOffsetMax;

	private Vector2 _orgTextOffsetMin;

	private RectTransform _textRectTrans;

	public Text _text;

	public Button _btn;

	public Color _grayTextColor;

	public Vector2 _pressTextOffset;

	public int _controlID;

	public bool _canFocus;

	public WndAudioClip _audioPress;

	public WndClickMethod _onClick;

	public WndAnimation[] _animNormal;

	public WndAnimation[] _animHover;

	public WndAnimation[] _animPress;

	public WndAnimation[] _animClick;

	public WndAnimation[] _animDisable;

	public WndAnimation[] _animNormalCheck;

	public WndAnimation[] _animHoverCheck;

	public WndAnimation[] _animPressCheck;

	public WndAnimation[] _animClickCheck;

	public WndAnimation[] _animDisableCheck;

	public bool _forceStopToggleAnim;

	public WndToggle _wndToggle;

	// Source: Ghidra work/06_ghidra/decompiled_full/ButtonController/ — multi-state button with per-state animations.

	private bool needInit
	{
		get { return (_state & BS_INIT) == 0; }
		set { if (value) _state &= ~BS_INIT; else _state |= BS_INIT; }
	}

	private bool isHoveredState
	{
		get { return (_state & BS_HOVER) != 0; }
		set { if (value) _state |= BS_HOVER; else _state &= ~BS_HOVER; }
	}

	private bool isPressedState
	{
		get { return (_state & BS_PRESS) != 0; }
		set { if (value) _state |= BS_PRESS; else _state &= ~BS_PRESS; }
	}

	public bool isChecked { get { return (_state & BS_CHECK) != 0; } }
	public bool isPopup { get { return (_state & BS_POPUP) != 0; } }

	public bool isPause
	{
		get { return (_state & BS_PAUSE) != 0; }
		set { if (value) _state |= BS_PAUSE; else _state &= ~BS_PAUSE; }
	}

	private bool isCheckedState
	{
		get { return (_state & BS_CHECK) != 0; }
		set { if (value) _state |= BS_CHECK; else _state &= ~BS_CHECK; }
	}

	private bool isDisabledState
	{
		get { return (_state & BS_DISABLE) != 0; }
		set { if (value) _state |= BS_DISABLE; else _state &= ~BS_DISABLE; }
	}

	public override void InitComponent(WndForm wnd)
	{
		_wnd = wnd;
		if (_onClick != null) _onClick.InitComponent(wnd);
		needInit = true;
	}

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		if (_onClick != null) _onClick.DinitComponent(wnd);
	}

	private void Start()
	{
		InitButton();
	}

	private void InitButton()
	{
		if (!needInit) return;
		needInit = false;
		if (_text != null)
		{
			_orgTextColor = _text.color;
			_textRectTrans = _text.GetComponent<RectTransform>();
			if (_textRectTrans != null)
			{
				_orgTextOffsetMin = _textRectTrans.offsetMin;
				_orgTextOffsetMax = _textRectTrans.offsetMax;
			}
		}
	}

	private void UpdateLayout()
	{
		// Apply text offset when pressed.
		if (_textRectTrans == null) return;
		Vector2 ofs = isPressedState ? _pressTextOffset : Vector2.zero;
		_textRectTrans.offsetMin = _orgTextOffsetMin + ofs;
		_textRectTrans.offsetMax = _orgTextOffsetMax + ofs;
	}

	public void SetButtonEnabled(bool b)
	{
		isDisabledState = !b;
		if (_btn != null) _btn.interactable = b;
		if (_text != null) _text.color = b ? _orgTextColor : _grayTextColor;
		PlayAnimSet(b ? (isCheckedState ? _animNormalCheck : _animNormal) : (isCheckedState ? _animDisableCheck : _animDisable));
	}

	public void SetButtonChecked(bool b)
	{
		isCheckedState = b;
		PlayAnimSet(b ? _animNormalCheck : _animNormal);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHoveredState = true;
		PlayAnimSet(isCheckedState ? _animHoverCheck : _animHover);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHoveredState = false;
		PlayAnimSet(isCheckedState ? _animNormalCheck : _animNormal);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPressedState = true;
		UpdateLayout();
		PlayAnimSet(isCheckedState ? _animPressCheck : _animPress);
		if (_audioPress != null) _audioPress.PlaySound();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPressedState = false;
		UpdateLayout();
		PlayAnimSet(isHoveredState ? (isCheckedState ? _animHoverCheck : _animHover) : (isCheckedState ? _animNormalCheck : _animNormal));
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (isDisabledState) return;
		if (_wndToggle != null) _wndToggle.Toggle(_controlID);
		if (_onClick != null) _onClick.OnPointerClick(eventData);
		PlayAnimSet(isCheckedState ? _animClickCheck : _animClick);
	}

	private void Toggle()
	{
		isCheckedState = !isCheckedState;
		PlayAnimSet(isCheckedState ? _animNormalCheck : _animNormal);
	}

	public void OnSelect(BaseEventData eventData) { }
	public void OnDeselect(BaseEventData eventData) { }

	private void PlayAnimSet(WndAnimation[] set)
	{
		if (_preAnim != null)
		{
			foreach (var a in _preAnim) if (a != null) a.StopAnimation();
		}
		_preAnim = set;
		if (set != null)
		{
			foreach (var a in set) if (a != null) a.PlayAnimation();
		}
	}

	public ButtonController()
	{ }
}
