// Source: Ghidra work/06_ghidra/decompiled_full/WndPressMethod/ (5 .c + 2 closures)
// Field offsets: _methodName@0x20, _comp@0x28, _useLongPress@0x30, _longPressPeriod@0x34,
//   _longPressWaiting@0x38, _useContinuePress@0x39, _continuePressing@0x3A, _continueCurrent@0x3C,
//   _continueFasterPeriod@0x40, _continueBetween@0x44, _continueBetweenLess@0x48, _continueNoWait@0x4C,
//   _autoClickFx@0x4D, _TweenScale@0x50, _DefaultLocalScale@0x58, isInitTweenScale@0x64,
//   wndClip@0x68, _wnd@0x70, _method@0x78, _methodParams@0x80
// String literal #3464 = "BtnPress_CallBack". Action_Type.Confirm = 1.

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UJ RD1/WndForm/Progs/Press Method")]
public class WndPressMethod : IWndComponent, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public string _methodName;
	public Component _comp;
	public bool _useLongPress;
	public float _longPressPeriod;
	private bool _longPressWaiting;
	public bool _useContinuePress;
	private bool _continuePressing;
	private float _continueCurrent;
	public int _continueFasterPeriod;
	public float _continueBetween;
	public float _continueBetweenLess;
	public bool _continueNoWait;
	public bool _autoClickFx;
	public TweenScale _TweenScale;
	private Vector3 _DefaultLocalScale;
	private bool isInitTweenScale;
	private WndAudioClip wndClip;
	private WndForm _wnd;
	private MethodInfo _method;
	private object[] _methodParams;

	// Source: Ghidra InitComponent.c RVA 0x195b2f8
	public override void InitComponent(WndForm wnd)
	{
		if (wnd != null && !string.IsNullOrEmpty(_methodName))
		{
			Type wndType = wnd.GetType();
			if (wndType != typeof(WndForm_Lua))
			{
				var types = new Type[] { typeof(Component), typeof(PointerEventData), typeof(Action_Type), typeof(int) };
				_method = wndType.GetMethod(_methodName, types);
				if (_method != null)
				{
					_wnd = wnd;
					_methodParams = new object[4];
					_methodParams[0] = _comp;
				}
			}
			else
			{
				var types = new Type[] { typeof(string), typeof(Component), typeof(PointerEventData), typeof(Action_Type), typeof(int) };
				_method = wndType.GetMethod("BtnPress_CallBack", types);
				if (_method != null)
				{
					_wnd = wnd;
					_methodParams = new object[5];
					_methodParams[0] = _methodName;
					_methodParams[1] = _comp;
				}
			}
		}

		if (_method == null) return;

		// TweenScale init (only if _autoClickFx)
		if (_autoClickFx)
		{
			if (!isInitTweenScale)
			{
				isInitTweenScale = true;
				_DefaultLocalScale = transform.localScale;
				if (_TweenScale == null)
				{
					_TweenScale = gameObject.AddComponent<TweenScale>();
					if (_TweenScale != null)
					{
						_TweenScale.style = (UITweener.Style)0;
						_TweenScale.duration = 0.1f;
						_TweenScale.from = _DefaultLocalScale;
						_TweenScale.to = _DefaultLocalScale * 0.95f;
					}
				}
				if (_TweenScale != null) _TweenScale.enabled = false;
			}
		}

		wndClip = GetComponent<WndAudioClip>();
	}

	public override void DinitComponent(WndForm wnd)
	{
		_wnd = null;
		_method = null;
		_methodParams = null;
	}

	private IEnumerator WaitLongPress(float waitTime, PointerEventData eventData)
	{
		float dueDate = Time.unscaledTime + waitTime;
		while (Time.unscaledTime < dueDate) yield return null;
		_longPressWaiting = false;
		InvokePress(eventData);
	}

	private IEnumerator ExecuteContinuePress(PointerEventData eventData)
	{
		_continuePressing = true;
		int count = 0;
		float dueDate = Time.unscaledTime;
		if (!_continueNoWait) dueDate += _continueBetween;
		while (_continuePressing)
		{
			if (Time.unscaledTime >= dueDate)
			{
				InvokePress(eventData);
				count++;
				float between = (count < _continueFasterPeriod) ? _continueBetween : _continueBetweenLess;
				dueDate = Time.unscaledTime + between;
			}
			yield return null;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;

		if (wndClip != null) wndClip.PlaySound();
		else if (SoundProxy.main != null) SoundProxy.main.PlaySoundByName("ClickUp", 1f, 1f, false);

		if (_autoClickFx && _TweenScale != null) _TweenScale.PlayForward();

		if (!_useLongPress)
		{
			if (!_useContinuePress)
			{
				InvokePress(eventData);
				return;
			}
			if (_continuePressing) { StopAllCoroutines(); _continuePressing = false; }
			StartCoroutine(ExecuteContinuePress(eventData));
		}
		else
		{
			if (_longPressWaiting) { Debug.LogError("WndPressMethod: long press already waiting"); return; }
			if (_continuePressing) { StopAllCoroutines(); _continuePressing = false; }
			_longPressWaiting = true;
			StartCoroutine(WaitLongPress(_longPressPeriod, eventData));
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (WndForm.WaitQuitApp()) return;
		if (_wnd == null) return;
		if (!_wnd.IsActive()) return;

		if (_autoClickFx && _TweenScale != null) _TweenScale.PlayReverse();

		if (_longPressWaiting)
		{
			StopAllCoroutines();
			_longPressWaiting = false;
			InvokePress(eventData);
		}
		if (_continuePressing)
		{
			StopAllCoroutines();
			_continuePressing = false;
		}
	}

	// Invoke method with Action_Type.Confirm (=1) — matches Ghidra local_3c[0]=1.
	private void InvokePress(PointerEventData eventData)
	{
		if (_method == null || _wnd == null || _methodParams == null) return;
		Type wndType = _wnd.GetType();
		bool isLuaWnd = (wndType == typeof(WndForm_Lua));
		if (!isLuaWnd)
		{
			if (_methodParams.Length < 4) return;
			_methodParams[1] = Action_Type.Confirm;
			_methodParams[2] = eventData;
			_methodParams[3] = 0;
		}
		else
		{
			if (_methodParams.Length < 5) return;
			_methodParams[2] = Action_Type.Confirm;
			_methodParams[3] = eventData;
			_methodParams[4] = 0;
		}
		_method.Invoke(_wnd, _methodParams);
	}

	private void OnEnable()
	{
		if (_TweenScale != null)
		{
			_TweenScale.Toggle();
			_TweenScale.enabled = false;
			_TweenScale.ResetToBeginning();
		}
	}

	public WndPressMethod() { }
}
