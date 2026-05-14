using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

// Virtual Joystick handler. Attached to WndForm_Joystick prefab root.
// Reads touch/mouse input every frame, computes a normalized 2D direction from the
// cursor position relative to the joystick container, drives the thumb visual, and
// forwards movement to Lua's BaseEntity:joystickMove on the player entity.
//
// Source: Ghidra work/06_ghidra/decompiled_full/VJHandler/*.c  (libil2cpp.so)
//
// Field layout from il2cpp dump (TypeDefIndex 707):
//   +0x20 joystick       Image      — the JoystickHandle thumb (= Joystick_center prefab obj)
//   +0x28 JoystickHandle GameObject — the activation gate (= Joystick_bak prefab obj; lua's self.Joystick)
//   +0x30 JoystickConvas Canvas     — the WndForm_Joystick canvas
//   +0x38 jsContainer    Image      — JoystickHandle.GetComponent<Image>() (Joystick_bak's Image)
//   +0x40 InputDirection Vector3    — current normalized drag direction
//   +0x4c stopProcess    bool       — set true while dragging; reset in joystickOnEndDrag
public class VJHandler : MonoBehaviour
{
	public Image joystick;

	public GameObject JoystickHandle;

	public Canvas JoystickConvas;

	private Image jsContainer;

	private Vector3 InputDirection;

	private bool stopProcess;

	// Source: Ghidra Start.c RVA 0x18ceaf4
	//   if (JoystickHandle == null) NRE;
	//   jsContainer = JoystickHandle.GetComponent<Image>();
	//   InputDirection = Vector3.zero;
	//   stopProcess = false;
	private void Start()
	{
		if (JoystickHandle == null) throw new System.NullReferenceException();
		jsContainer = JoystickHandle.GetComponent<Image>();
		InputDirection = Vector3.zero;
		stopProcess = false;
	}

	// Source: Ghidra Update.c RVA 0x18ceb9c
	//   if (JoystickHandle == null) NRE;
	//   if (JoystickHandle.activeSelf) JoystickOnDrag();
	//   else if (stopProcess) joystickOnEndDrag(1);
	private void Update()
	{
		if (JoystickHandle == null) throw new System.NullReferenceException();
		if (JoystickHandle.activeSelf)
		{
			JoystickOnDrag();
		}
		else if (stopProcess)
		{
			joystickOnEndDrag(1);
		}
	}

	// Source: Ghidra CheckIfJoystickSensor.c RVA 0x18cf3b8
	//   return CenterPosition.y-90 <= hitPos.y && hitPos.y <= CenterPosition.y+90
	//       && hitPos.x <= CenterPosition.x+90 && CenterPosition.x-90 <= hitPos.x;
	public bool CheckIfJoystickSensor(Vector2 hitPos, Vector3 CenterPosition)
	{
		return CenterPosition.y + -90.0f <= hitPos.y
			&& hitPos.y <= CenterPosition.y + 90.0f
			&& hitPos.x <= CenterPosition.x + 90.0f
			&& CenterPosition.x + -90.0f <= hitPos.x;
	}

	// Source: Ghidra GetClickPosition.c RVA 0x18cf408
	//   if (Input.touchCount == 0) return Input.mousePosition;
	//   else return Input.GetTouch(0).position;
	public Vector2 GetClickPosition()
	{
		if (Input.touchCount == 0)
		{
			Vector3 mp = Input.mousePosition;
			return new Vector2(mp.x, mp.y);
		}
		Touch t = Input.GetTouch(0);
		return t.position;
	}

	// Source: Ghidra GetClickState.c RVA 0x18cf490
	//   if (Input.touchCount < 1) {
	//     switch(state) { 3:GetMouseButtonUp; 2:GetMouseButton; 1:GetMouseButtonDown; default:false }
	//   } else {
	//     Touch t = Input.GetTouch(0);
	//     switch(state) { 3:phase==Ended(3); 2:deltaTime>0; 1:phase==Began(0); default:false }
	//   }
	public bool GetClickState(int state)
	{
		if (Input.touchCount < 1)
		{
			if (state == 3) return Input.GetMouseButtonUp(0);
			if (state == 2) return Input.GetMouseButton(0);
			if (state == 1) return Input.GetMouseButtonDown(0);
			return false;
		}
		Touch t = Input.GetTouch(0);
		if (state == 3) return t.phase == TouchPhase.Ended;
		if (state == 2) return t.deltaTime > 0.0f;
		if (state == 1) return t.phase == TouchPhase.Began;
		return false;
	}

	// Source: Ghidra JoystickOnDrag.c RVA 0x18cebe8
	//   stopProcess = true;
	//   if (GetClickState(3)) { InputDirection = Vector3.zero; joystickOnEndDrag(0); return; }
	//   if (!Input.GetMouseButton(0)) { joystickOnEndDrag(1); return; }
	//   InputDirection = Vector3.zero;
	//   if (jsContainer == null) NRE;
	//   RectTransformUtility.ScreenPointToLocalPointInRectangle(jsContainer.rectTransform,
	//       GetClickPosition(), WndRoot.uiCamera, out localPoint);
	//   sz = jsContainer.rectTransform.sizeDelta;
	//   pivot = jsContainer.rectTransform.pivot;
	//   pivotShiftX = (pivot.x != 1) ? -1 : 1; pivotShiftY = (pivot.y != 1) ? -1 : 1;
	//   InputDirection.x = 2*localPoint.x/sz.x + pivotShiftX;
	//   InputDirection.y = 2*localPoint.y/sz.y + pivotShiftY;
	//   InputDirection.z = 0;
	//   mag = sqrt(InputDirection.sqrMagnitude);
	//   if (mag > 1) { if (mag <= 1e-5) InputDirection = Vector3.zero; else InputDirection /= mag; }
	//   joystick.rectTransform.anchoredPosition = new Vector2(InputDirection.x*sz.x/6, InputDirection.y*sz.y/6);
	//   if ((InputDirection - Vector3.zero).sqrMagnitude < 1e-10) return;  // tiny drag — animate thumb only
	//   SendToEntityMove();
	public void JoystickOnDrag()
	{
		stopProcess = true;

		if (GetClickState(3))
		{
			InputDirection = Vector3.zero;
			joystickOnEndDrag(0);
			return;
		}
		if (!Input.GetMouseButton(0))
		{
			joystickOnEndDrag(1);
			return;
		}

		InputDirection = Vector3.zero;
		if (jsContainer == null) throw new System.NullReferenceException();

		RectTransform contRT = jsContainer.rectTransform;
		Vector2 screenPos = GetClickPosition();
		Camera uiCam = WndRoot.uiCamera;
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(contRT, screenPos, uiCam, out localPoint);

		if (jsContainer == null) return;
		contRT = jsContainer.rectTransform;
		if (contRT == null) return;

		Vector2 contSize = contRT.sizeDelta;
		float normX = localPoint.x / contSize.x;
		float normY = localPoint.y / contSize.y;

		Vector2 contPivot = contRT.pivot;
		float pivotShiftX = (contPivot.x != 1.0f) ? -1.0f : 1.0f;
		float pivotShiftY = (contPivot.y != 1.0f) ? -1.0f : 1.0f;

		InputDirection.z = 0.0f;
		InputDirection.x = normX + normX + pivotShiftX;
		InputDirection.y = normY + normY + pivotShiftY;

		// Source: DAT_0091c16c = 1e-5 (file offset 0x81c16c in libil2cpp.so).
		float mag = Mathf.Sqrt(InputDirection.x * InputDirection.x
							 + InputDirection.y * InputDirection.y
							 + InputDirection.z * InputDirection.z);
		if (mag > 1.0f)
		{
			if (mag <= 1e-5f)
			{
				InputDirection = Vector3.zero;
			}
			else
			{
				InputDirection.x = InputDirection.x / mag;
				InputDirection.y = InputDirection.y / mag;
				InputDirection.z = InputDirection.z / mag;
			}
		}

		if (joystick == null) return;
		RectTransform thumbRT = joystick.rectTransform;
		if (jsContainer == null) return;
		Vector2 size2 = jsContainer.rectTransform.sizeDelta;
		if (thumbRT == null) return;
		thumbRT.anchoredPosition = new Vector2(InputDirection.x * (size2.x / 6.0f),
											   InputDirection.y * (size2.y / 6.0f));

		// Source: DAT_0091c044 = 1e-10 (threshold for sending move).
		Vector3 delta = InputDirection - Vector3.zero;
		if (delta.x * delta.x + delta.y * delta.y + delta.z * delta.z < 1e-10f) return;

		SendToEntityMove();
	}

	// Source: Ghidra joystickOnEndDrag.c RVA 0x18cf084
	//   InputDirection = Vector3.zero;
	//   joystick.rectTransform.anchoredPosition = Vector2.zero;
	//   JoystickHandle.SetActive(false);
	//   var mgr    = LuaCall<object>("MapInfoMgr","Instance",[]);          if (mgr==null) return;
	//   var player = LuaCall<object>("MapInfoMgr","getPlayerEntity",[mgr]); if (player==null) return;
	//   stopProcess = false;
	//   LuaCall("BaseEntity","joystickMoveStop",[player, endType]);
	public void joystickOnEndDrag(int endType)
	{
		InputDirection = Vector3.zero;

		if (joystick == null) throw new System.NullReferenceException();
		RectTransform thumbRT = joystick.rectTransform;
		if (thumbRT == null) throw new System.NullReferenceException();
		thumbRT.anchoredPosition = Vector2.zero;

		if (JoystickHandle == null) throw new System.NullReferenceException();
		JoystickHandle.SetActive(false);

		object mgr = LuaFramework.Util.CallMethod<object>("MapInfoMgr", "Instance", new object[0]);
		if (mgr == null) return;
		object player = LuaFramework.Util.CallMethod<object>("MapInfoMgr", "getPlayerEntity", new object[] { mgr });
		if (player == null) return;

		stopProcess = false;

		LuaFramework.Util.CallMethod("BaseEntity", "joystickMoveStop", new object[] { player, endType });
	}

	// Source: Ghidra SendToEntityMove.c RVA 0x18cf59c
	//   var mgr    = LuaCall<object>("MapInfoMgr","Instance",[]);          if (mgr==null) return;
	//   var player = LuaCall<object>("MapInfoMgr","getPlayerEntity",[mgr]); if (player==null) return;
	//   LuaCall("BaseEntity","joystickMove",[player, InputDirection.x, InputDirection.y]);
	public void SendToEntityMove()
	{
		object mgr = LuaFramework.Util.CallMethod<object>("MapInfoMgr", "Instance", new object[0]);
		if (mgr == null) return;
		object player = LuaFramework.Util.CallMethod<object>("MapInfoMgr", "getPlayerEntity", new object[] { mgr });
		if (player == null) return;
		LuaFramework.Util.CallMethod("BaseEntity", "joystickMove", new object[] { player, InputDirection.x, InputDirection.y });
	}

	public VJHandler()
	{
	}
}
