using Cpp2IlInjected;

public class ActionName
{
	public static string ATTACK;

	public static string WAIT;

	public static string SKILLONE;

	public static string SKILLTWO;

	public static string DEAD;

	public static string HURT;

	public static string RUN;

	// Source: Ghidra work/06_ghidra/decompiled_rva/ActionName___ctor.c RVA 0x?
	// TODO 1-1 port (field init pending) — Ghidra body has assignments not yet ported.
	// Empty body unblocks boot; revisit when runtime hits missing field values.
	public ActionName()
	{
	}

	static ActionName()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
