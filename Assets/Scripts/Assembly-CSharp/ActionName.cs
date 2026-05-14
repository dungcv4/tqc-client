// Source: dump.cs RVA 0x18E6828 — empty cctor (`private static void .cctor() { }`).
// All static string fields default-init to null (string default).
// String literal values per stringliteral.json (action names used as PackedSprite anim keys).

using Cpp2IlInjected;

public class ActionName
{
	public static string ATTACK = "attack";
	public static string WAIT = "wait";
	public static string SKILLONE = "skill1";
	public static string SKILLTWO = "skill2";
	public static string DEAD = "dead";
	public static string HURT = "hurt";
	public static string RUN = "run";

	public ActionName() { }

	// Source: dump.cs — empty body.
	static ActionName() { }
}
