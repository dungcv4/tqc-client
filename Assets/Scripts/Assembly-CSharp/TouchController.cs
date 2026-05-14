// Source: Ghidra work/06_ghidra/decompiled_full/TouchController/ — basic touch handler for WndForm.
// Plays audio on press, optionally focuses the wnd. Drag handler intentionally empty per Ghidra.

using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Events/Touch Controller")]
public class TouchController : IWndComponent, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	private WndForm _wnd;

	public bool _canFocus;
	public bool _autoDrag;
	public WndAudioClip _audioPress;

	public override void InitComponent(WndForm wnd)
	{
		_wnd = wnd;
	}

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;
		if (_audioPress != null) _audioPress.PlaySound();
	}

	public void OnDrag(PointerEventData eventData) { }

	public TouchController() { }
}
