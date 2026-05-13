// Source: Ghidra work/06_ghidra/decompiled_full/TweenScale/ (9 .c files all ported 1-1)
// Source: dump.cs TypeDefIndex 261 — `TweenScale : UITweener`
// All 9 methods + ctor ported 1-1 from Ghidra (no chế cháo).

using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
    public Vector3 from;      // 0x80 (3 floats: 0x80/0x84/0x88)
    public Vector3 to;        // 0x8C (3 floats: 0x8C/0x90/0x94)
    private Transform mTrans; // 0x98

    // Source: Ghidra get_cachedTransform.c  RVA 0x19FC23C
    // Lazy-init mTrans = transform if null.
    public Transform cachedTransform
    {
        get
        {
            if (mTrans == null)
            {
                mTrans = this.transform;
            }
            return mTrans;
        }
    }

    // Source: Ghidra get_value.c  RVA 0x19FC2D0
    // Return cachedTransform.localScale.
    public Vector3 value
    {
        get
        {
            Transform t = cachedTransform;
            if (t == null) throw new System.NullReferenceException();
            return t.localScale;
        }
        // Source: Ghidra set_value.c  RVA 0x19FC2EC
        // Set cachedTransform.localScale = value.
        set
        {
            Transform t = cachedTransform;
            if (t == null) throw new System.NullReferenceException();
            t.localScale = value;
        }
    }

    // Source: Ghidra OnUpdate.c  RVA 0x19FC330
    // Lerp from→to by factor: value = from*(1-factor) + to*factor (component-wise).
    protected override void OnUpdate(float factor, bool isFinished)
    {
        float invFactor = 1.0f - factor;
        value = new Vector3(
            from.x * invFactor + to.x * factor,
            from.y * invFactor + to.y * factor,
            from.z * invFactor + to.z * factor);
    }

    // Source: Ghidra Begin.c  RVA 0x19FC368
    // Static factory: UITweener.Begin<TweenScale>(go, duration); set from=current value, to=scale.
    // If duration <= 0: immediately Sample(1, isFinished=true) and disable.
    public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
    {
        TweenScale t = UITweener.Begin<TweenScale>(go, duration);
        if (t == null) throw new System.NullReferenceException();
        t.from = t.value;
        t.to = scale;
        if (duration <= 0.0f)
        {
            t.Sample(1f, true);
            t.enabled = false;
        }
        return t;
    }

    // Source: Ghidra SetStartToCurrentValue.c  RVA 0x19FC420
    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue()
    {
        from = value;
    }

    // Source: Ghidra SetEndToCurrentValue.c  RVA 0x19FC43C
    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue()
    {
        to = value;
    }

    // Source: Ghidra SetCurrentValueToStart.c  RVA 0x19FC458
    [ContextMenu("Assume value of 'From'")]
    private void SetCurrentValueToStart()
    {
        value = from;
    }

    // Source: Ghidra SetCurrentValueToEnd.c  RVA 0x19FC464
    [ContextMenu("Assume value of 'To'")]
    private void SetCurrentValueToEnd()
    {
        value = to;
    }

    // RVA: 0x19FC470 — default ctor (inherits UITweener base).
    public TweenScale() { }
}
