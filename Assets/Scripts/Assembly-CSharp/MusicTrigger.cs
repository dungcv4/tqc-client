using Cpp2IlInjected;
using UnityEngine;

[AddComponentMenu("UJ RD1/Music Trigger")]
public class MusicTrigger : MonoBehaviour
{
	public int _musicID;

	public string _musicName;

	public float _volume;

	private void Start()
	{
		// TODO 1-1 port (Ghidra body deferred). Empty body to unblock boot.
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/MusicTrigger___ctor.c RVA 0x017BFA9C
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public MusicTrigger()
	{
	}
}
