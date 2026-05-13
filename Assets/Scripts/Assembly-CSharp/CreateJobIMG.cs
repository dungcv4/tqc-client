using Cpp2IlInjected;
using UnityEngine;
using UnityEngine.UI;

public class CreateJobIMG : MonoBehaviour
{
	private static CreateJobIMG _ins;

	[SerializeField]
	private Sprite[] _jobImage;

	[SerializeField]
	private Sprite[] _headIcon;

	public static CreateJobIMG Ins
	{
		get
		{ return default; }
	}

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	public void setSprite(Image img, int idx)
	{ }

	public void setHeadIcon(Image img, int idx)
	{ }

	// Source: Ghidra work/06_ghidra/decompiled_rva/CreateJobIMG___ctor.c RVA 0x018F4DD4
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public CreateJobIMG()
	{
	}
}
