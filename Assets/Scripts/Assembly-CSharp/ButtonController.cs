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

	private bool needInit
	{
		get
		{ return default; }
		set
		{ }
	}

	private bool isHoveredState
	{
		get
		{ return default; }
		set
		{ }
	}

	private bool isPressedState
	{
		get
		{ return default; }
		set
		{ }
	}

	public bool isChecked
	{
		get
		{ return default; }
	}

	public bool isPopup
	{
		get
		{ return default; }
	}

	public bool isPause
	{
		get
		{ return default; }
		set
		{ }
	}

	private bool isCheckedState
	{
		get
		{ return default; }
		set
		{ }
	}

	private bool isDisabledState
	{
		get
		{ return default; }
		set
		{ }
	}

	public override void InitComponent(WndForm wnd)
	{ }

	public override void DinitComponent(WndForm wnd)
	{ }

	private void Start()
	{ }

	private void InitButton()
	{ }

	private void UpdateLayout()
	{ }

	public void SetButtonEnabled(bool b)
	{ }

	public void SetButtonChecked(bool b)
	{ }

	public void OnPointerEnter(PointerEventData eventData)
	{ }

	public void OnPointerExit(PointerEventData eventData)
	{ }

	public void OnPointerDown(PointerEventData eventData)
	{ }

	public void OnPointerUp(PointerEventData eventData)
	{ }

	public void OnPointerClick(PointerEventData eventData)
	{ }

	private void Toggle()
	{ }

	public void OnSelect(BaseEventData eventData)
	{ }

	public void OnDeselect(BaseEventData eventData)
	{ }

	public ButtonController()
	{ }
}
