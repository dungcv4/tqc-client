using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class VJHandler : MonoBehaviour
{
	public Image joystick;

	public GameObject JoystickHandle;

	public Canvas JoystickConvas;

	private Image jsContainer;

	private Vector3 InputDirection;

	private bool stopProcess;

	private void Start()
	{ }

	private void Update()
	{ }

	public bool CheckIfJoystickSensor(Vector2 hitPos, Vector3 CenterPosition)
	{ return default; }

	public Vector2 GetClickPosition()
	{ return default; }

	public bool GetClickState(int state)
	{ return default; }

	public void JoystickOnDrag()
	{ }

	public void joystickOnEndDrag(int endType)
	{ }

	public void SendToEntityMove()
	{ }

	public VJHandler()
	{ }
}
