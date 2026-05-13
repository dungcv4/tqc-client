// Source: Ghidra work/06_ghidra/decompiled_full/TweenAlpha/ (8 .c, all 1-1)
// Source: dump.cs TypeDefIndex 254 — TweenAlpha : UITweener
// Field offsets:
//   from@0x80, to@0x84, mCached@0x88, mRect@0x90, mCg@0x98, mMat@0xA0, mSr@0xA8

using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
    [Range(0f, 1f)] public float from;          // 0x80
    [Range(0f, 1f)] public float to;            // 0x84
    private bool mCached;                       // 0x88
    private RectTransform mRect;                // 0x90
    private CanvasGroup mCg;                    // 0x98
    private Material mMat;                      // 0xA0
    private SpriteRenderer mSr;                 // 0xA8

    // Source: Ghidra get_canvasGroup.c RVA 0x19FA150
    // 1-1: if !mCached → Cache(); return mCg.
    public CanvasGroup canvasGroup
    {
        get
        {
            if (!mCached) Cache();
            return mCg;
        }
    }

    // Source: Ghidra get_value.c RVA 0x19FA410 + set_value.c RVA 0x19FA520
    // 1-1: Cache() if !mCached.
    //   getter: if mRect != null && mCg != null → return mCg.alpha
    //           else if mSr != null → return mSr.color.a
    //           else if mMat != null → return mMat.color.a; default 1.0f
    //   setter: route the alpha update to whichever component is bound.
    public float value
    {
        get
        {
            if (!mCached) Cache();
            // Ghidra branch order: check mRect (must be null) → check mSr (must be null) → mMat path; else CanvasGroup path.
            if (mRect != null)
            {
                if (mCg == null) throw new System.NullReferenceException();
                return mCg.alpha;
            }
            if (mSr != null) return mSr.color.a;
            if (mMat != null) return mMat.color.a;
            return 1.0f;
        }
        set
        {
            if (!mCached) Cache();
            if (mRect != null)
            {
                if (mCg == null) throw new System.NullReferenceException();
                mCg.alpha = value;
                return;
            }
            if (mSr != null)
            {
                Color c = mSr.color;
                c.a = value;
                mSr.color = c;
                return;
            }
            if (mMat != null)
            {
                Color c = mMat.color;
                c.a = value;
                mMat.color = c;
                return;
            }
            // Ghidra: FUN_015cb8fc — NRE if nothing bound.
            throw new System.NullReferenceException();
        }
    }

    // Source: Ghidra Cache.c RVA 0x19FA174
    // 1-1:
    //   mCached = true;
    //   mRect = GetComponent<RectTransform>();
    //   mSr   = GetComponent<SpriteRenderer>();
    //   if mRect == null && mSr == null:
    //     Renderer r = GetComponent<Renderer>();
    //     if r != null: mMat = r.sharedMaterial;
    //     if mMat == null: mRect = GetComponentInChildren<RectTransform>();   // (override mRect)
    //   if mRect != null:
    //     mCg = GetComponent<CanvasGroup>();
    //     if mCg == null: mCg = gameObject.AddComponent<CanvasGroup>();
    private void Cache()
    {
        mCached = true;
        mRect = GetComponent<RectTransform>();
        mSr = GetComponent<SpriteRenderer>();
        if (mRect == null && mSr == null)
        {
            Renderer r = GetComponent<Renderer>();
            if (r != null) mMat = r.sharedMaterial;
            if (mMat == null) mRect = GetComponentInChildren<RectTransform>();
        }
        if (mRect != null)
        {
            mCg = GetComponent<CanvasGroup>();
            if (mCg == null)
            {
                if (gameObject == null) throw new System.NullReferenceException();
                mCg = gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    // Source: Ghidra OnUpdate.c RVA 0x19FA678
    // 1-1: if factor < 0: factor = 0; value = from + factor * (to - from).
    protected override void OnUpdate(float factor, bool isFinished)
    {
        if (factor < 0f) factor = 0f;
        value = from + factor * (to - from);
    }

    // Source: Ghidra Begin.c RVA 0x19FA6A0
    // 1-1: var c = UITweener.Begin<TweenAlpha>(go, duration);
    //      if c == null → NRE.
    //      c.from = c.value; c.to = alpha;
    //      if duration <= 0: c.Sample(1f, true); c.enabled = false;
    //      return c.
    public static TweenAlpha Begin(GameObject go, float duration, float alpha)
    {
        TweenAlpha c = UITweener.Begin<TweenAlpha>(go, duration);
        if (c == null) throw new System.NullReferenceException();
        c.from = c.value;
        c.to = alpha;
        if (duration <= 0f)
        {
            c.Sample(1f, true);
            c.enabled = false;
        }
        return c;
    }

    // Source: Ghidra SetStartToCurrentValue.c RVA 0x19FA8A8 — from = value.
    public override void SetStartToCurrentValue()
    {
        from = value;
    }

    // Source: Ghidra SetEndToCurrentValue.c RVA 0x19FA8C0 — to = value.
    public override void SetEndToCurrentValue()
    {
        to = value;
    }

    // Source: Ghidra .ctor.c RVA 0x19FA8D8
    // 1-1: NEON_fmov writes two adjacent floats (1.0f, 1.0f) at offset 0x80 = `from`, 0x84 = `to`.
    //      Then UITweener.ctor() (base call).
    public TweenAlpha() : base()
    {
        from = 1.0f;
        to = 1.0f;
    }
}
