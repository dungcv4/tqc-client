// Source: Ghidra work/06_ghidra/decompiled_full/UITweener/ (22 .c) + dump.cs TDI 268.
// All methods ported 1-1 from Ghidra pseudo-C.

using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

public abstract class UITweener : MonoBehaviour
{
    public static UITweener current;
    [HideInInspector] public UITweener.Method method;   // 0x20
    [HideInInspector] public UITweener.Style style;     // 0x24
    [HideInInspector] public AnimationCurve animationCurve;  // 0x28
    [HideInInspector] public bool ignoreTimeScale;      // 0x30
    [HideInInspector] public float delay;               // 0x34
    [HideInInspector] public float duration;            // 0x38
    [HideInInspector] public bool steeperCurves;        // 0x3C
    [HideInInspector] public int tweenGroup;            // 0x40
    [HideInInspector] public List<EventDelegate> onFinished;  // 0x48
    [HideInInspector] public GameObject eventReceiver;  // 0x50
    [HideInInspector] public string callWhenFinished;   // 0x58
    private bool mStarted;            // 0x60
    private float mStartTime;         // 0x64
    private float mDuration;          // 0x68 (cached for amountPerDelta recompute)
    private float mAmountPerDelta;    // 0x6C
    private float mFactor;            // 0x70
    private List<EventDelegate> mTemp; // 0x78

    // Cached amountPerDelta — Ghidra inlines this pattern in many methods.
    // RVA 0x19FD334 — get_amountPerDelta is this exact pattern as method.
    private float ComputeAmountPerDelta()
    {
        if (mDuration == duration) return mAmountPerDelta;
        float rate = 1.0f / duration;
        if (duration <= 0.0f) rate = 1000.0f;
        float val = Math.Abs(rate);
        if (mAmountPerDelta < 0.0f) val = -Math.Abs(rate);
        mDuration = duration;
        mAmountPerDelta = val;
        return val;
    }

    // Source: Ghidra get_amountPerDelta.c  RVA 0x19FD334
    public float get_amountPerDelta() => ComputeAmountPerDelta();

    // Source: Ghidra get_tweenFactor.c  RVA 0x19FD380
    public float get_tweenFactor() => mFactor;

    // Source: Ghidra set_tweenFactor.c  RVA 0x19FD388
    // Clamps value to [0,1], stores in mFactor.
    public void set_tweenFactor(float value)
    {
        bool negative = value < 0.0f;
        if (value > 1.0f) value = 1.0f;
        if (negative) value = 0.0f;
        mFactor = value;
    }

    // Source: Ghidra get_direction.c  RVA 0x19FD3A4
    // Returns Direction.Forward (1) if mAmountPerDelta >= 0, else Reverse (-1).
    public Direction get_direction()
    {
        float rate = ComputeAmountPerDelta();
        return rate >= 0.0f ? Direction.Forward : Direction.Reverse;
    }

    // Source: Ghidra Reset.c  RVA 0x19FD3FC
    // If !mStarted: calls SetStartToCurrentValue() then SetEndToCurrentValue() (virtual dispatch).
    private void Reset()
    {
        if (mStarted) return;
        SetStartToCurrentValue();
        SetEndToCurrentValue();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/UITweener/Start.c  RVA 0x19FD434
    // Start and Update decompile identically and live 4 bytes apart (0x19FD434 vs 0x19FD438),
    // i.e. Start() is a single-instruction tail jump to Update(). Modeled as delegation.
    protected virtual void Start()
    {
        Update();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/UITweener/Update.c  RVA 0x19FD438
    // Per-frame state machine: delay countdown → recompute amountPerDelta → advance mFactor
    // → handle Style (Once/Loop/PingPong) → Sample(factor, isFinished) → on finish dispatch
    // onFinished list + eventReceiver SendMessage. Static `current` is set/cleared around dispatch.
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        float now = Time.time;
        if (!mStarted)
        {
            mStarted = true;
            mStartTime = now + delay;
        }
        if (now < mStartTime) return;

        // Inlined ComputeAmountPerDelta (Ghidra inlines the same pattern as get_amountPerDelta).
        float rate;
        if (mDuration == duration)
        {
            rate = mAmountPerDelta;
        }
        else
        {
            float inv = 1.0f / duration;
            if (duration <= 0.0f) inv = 1000.0f;
            float abs = Mathf.Abs(inv);
            if (mAmountPerDelta < 0.0f) abs = -Mathf.Abs(inv);
            mDuration = duration;
            mAmountPerDelta = abs;
            rate = abs;
        }

        int styleI = (int)style;
        mFactor += deltaTime * rate;

        if (styleI == 0) // Once
        {
            if (duration == 0.0f || mFactor > 1.0f || mFactor < 0.0f)
            {
                bool wasNeg = mFactor < 0.0f;
                if (mFactor > 1.0f) mFactor = 1.0f;
                if (wasNeg) mFactor = 0.0f;
                Sample(mFactor, true);
                if (duration == 0.0f
                    || (mFactor == 1.0f && mAmountPerDelta > 0.0f)
                    || (mFactor == 0.0f && mAmountPerDelta < 0.0f))
                {
                    this.enabled = false;
                }

                // Re-entry guard: if current is already set, do not dispatch onFinished
                // (mirrors `if ((uVar6 & 1) == 0) return;` after op_Equality check on current).
                if (UITweener.current != null) return;
                UITweener.current = this;

                if (onFinished != null)
                {
                    mTemp = onFinished;
                    onFinished = new List<EventDelegate>();
                    EventDelegate.Execute(mTemp);
                    if (mTemp != null)
                    {
                        for (int i = 0; i < mTemp.Count; i++)
                        {
                            EventDelegate ed = mTemp[i];
                            if (ed != null && !ed.oneShot)
                            {
                                EventDelegate.Add(onFinished, ed);
                            }
                        }
                        mTemp = null;
                    }
                }

                if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                {
                    eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
                }
                UITweener.current = null;
                return;
            }
        }
        else if (styleI == 2) // PingPong
        {
            if (mFactor > 1.0f)
            {
                mFactor = ((int)mFactor - mFactor) + 1.0f;
            }
            else if (mFactor < 0.0f)
            {
                mFactor = -mFactor - (int)-mFactor;
            }
            else
            {
                goto SAMPLE;
            }
            mAmountPerDelta = -rate;
        }
        else if (styleI == 1 && mFactor > 1.0f) // Loop
        {
            mFactor = mFactor - (int)mFactor;
        }

        SAMPLE:
        Sample(mFactor, false);
    }

    // Source: Ghidra SetOnFinished.c (overload 1)  RVA 0x19FD884
    // Replaces onFinished with new list containing single EventDelegate(callback).
    public void SetOnFinished(EventDelegate.Callback del)
    {
        if (onFinished == null) onFinished = new List<EventDelegate>();
        EventDelegate.Set(onFinished, del);
    }

    // Source: Ghidra SetOnFinished.c (overload 2)  RVA 0x19FD8F0
    public void SetOnFinished(EventDelegate del)
    {
        if (onFinished == null) onFinished = new List<EventDelegate>();
        EventDelegate.Set(onFinished, del);
    }

    // Source: Ghidra AddOnFinished.c (overload 1)  RVA 0x19FD95C
    public void AddOnFinished(EventDelegate.Callback del)
    {
        if (onFinished == null) onFinished = new List<EventDelegate>();
        EventDelegate.Add(onFinished, del);
    }

    // Source: Ghidra AddOnFinished.c (overload 2)  RVA 0x19FD9C8
    public void AddOnFinished(EventDelegate del)
    {
        if (onFinished == null) onFinished = new List<EventDelegate>();
        EventDelegate.Add(onFinished, del);
    }

    // Source: Ghidra RemoveOnFinished.c  RVA 0x19FDA34
    public void RemoveOnFinished(EventDelegate del)
    {
        if (onFinished != null) onFinished.Remove(del);
        if (mTemp != null) mTemp.Remove(del);
    }

    // Source: Ghidra OnDisable.c  RVA 0x19FDAA8
    // Resets mStarted flag to false so next enable re-starts the delay.
    private void OnDisable() { mStarted = false; }

    // Source: Ghidra work/06_ghidra/decompiled_full/UITweener/Sample.c  RVA 0x19FA740
    // Constants resolved from NGUI UITweener reference:
    //   DAT_0091c0e8 = 2*PI    (case 3 inner divisor)
    //   DAT_0091c180 = -PI/2   (case 1 EaseIn  — sin((1-t)*-PI/2)+1 = 1-sin((1-t)*PI/2))
    //   DAT_0091c170 = PI/2    (case 2 EaseOut — sin(t*PI/2))
    // Note: Ghidra clamps low only (`if (param_1 < 0.0) param_1 = 0.0;`) — no upper clamp.
    // The trailing virtual call at *(*this + 0x188) is the OnUpdate(factor, isFinished) slot.
    public void Sample(float factor, bool isFinished)
    {
        if (factor < 0.0f) factor = 0.0f;
        switch (method)
        {
            case Method.EaseIn:  // case 1
                factor = Mathf.Sin((1.0f - factor) * (-Mathf.PI * 0.5f)) + 1.0f;
                if (steeperCurves) factor = factor * factor;
                break;
            case Method.EaseOut:  // case 2
                factor = Mathf.Sin(factor * (Mathf.PI * 0.5f));
                if (steeperCurves)
                {
                    factor = 1.0f - (1.0f - factor) * (1.0f - factor);
                }
                break;
            case Method.EaseInOut:  // case 3
            {
                float pi2 = Mathf.PI * 2.0f;
                float s = Mathf.Sin(factor * pi2);
                factor = factor - s / pi2;
                if (steeperCurves)
                {
                    float v = factor + factor - 1.0f;
                    float absV = 1.0f - Mathf.Abs(v);
                    float vv = 1.0f - absV * absV;
                    if (v < 0.0f) vv = -vv;
                    factor = vv * 0.5f + 0.5f;
                }
                break;
            }
            case Method.BounceIn:  // case 4
                factor = BounceLogic(factor);
                break;
            case Method.BounceOut:  // case 5
                factor = 1.0f - BounceLogic(1.0f - factor);
                break;
        }
        if (animationCurve != null)
        {
            factor = animationCurve.Evaluate(factor);
        }
        OnUpdate(factor, isFinished);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/UITweener/BounceLogic.c  RVA 0x19FDAB0
    // Standard NGUI bounce easing (Robert Penner). Constants identified by structural match:
    //   DAT_0091c184 = 1/2.75  (~0.363636) — first threshold
    //   DAT_0091c1e8 = 2/2.75  (~0.727272) — second threshold
    //   DAT_0091c078 = 2.5/2.75(~0.909090) — third threshold
    //   DAT_0091c05c = 7.5625  — common coefficient
    //   DAT_0091c200 = -0.545454 (-6/11)   — offset for region 2
    //   DAT_0091c1cc = -0.818181 (-9/11)   — offset for region 3
    //   DAT_0091c188 = -0.954545 (-21/22)  — offset for region 4
    private float BounceLogic(float val)
    {
        if (val < 0.363636f)
        {
            return 7.5625f * val * val;
        }
        if (val >= 0.727272f)
        {
            if (val >= 0.909090f)
            {
                float v = val + -0.954545f;
                return v * v * 7.5625f + 0.984375f;
            }
            else
            {
                float v = val + -0.818181f;
                return v * v * 7.5625f + 0.9375f;
            }
        }
        else
        {
            float v = val + -0.545454f;
            return v * v * 7.5625f + 0.75f;
        }
    }

    // Source: Ghidra Play.c (no-arg)  RVA 0x19FDB68 — calls Play(forward=true)
    [Obsolete("Use PlayForward() instead")]
    public void Play() { Play(true); }

    // Source: Ghidra PlayForward.c  RVA 0x19FDBF0 — delegate to Play(true)
    public void PlayForward() { Play(true); }

    // Source: Ghidra PlayReverse.c  RVA 0x19FDBF8 — delegate to Play(false)
    public void PlayReverse() { Play(false); }

    // Source: Ghidra Play.c (bool)  RVA 0x19FDB70
    // Recompute amountPerDelta, force its sign by `forward`, enable behaviour, Update().
    public void Play(bool forward)
    {
        float rate = ComputeAmountPerDelta();
        float magnitude = Math.Abs(rate);
        mAmountPerDelta = forward ? magnitude : -magnitude;
        this.enabled = true;
        Update();
    }

    // Source: Ghidra ResetToBeginning.c  RVA 0x19FDC00
    // mStarted=false, mFactor = (rate>=0 ? 0 : 1), then Sample(mFactor, false).
    public void ResetToBeginning()
    {
        mStarted = false;
        float rate = ComputeAmountPerDelta();
        mFactor = rate >= 0.0f ? 0.0f : 1.0f;
        Sample(mFactor, false);
    }

    // Source: Ghidra Toggle.c  RVA 0x19FDC68
    // Flip direction sign based on current mFactor (<=0 → forward, else reverse), enable.
    public void Toggle()
    {
        float rate = ComputeAmountPerDelta();
        mAmountPerDelta = (mFactor <= 0.0f) ? Math.Abs(rate) : -rate;
        this.enabled = true;
    }

    // Abstract — overridden by concrete tweens (TweenAlpha, TweenScale, etc.)
    protected abstract void OnUpdate(float factor, bool isFinished);

    // Generic factory — instantiated for each tween subclass via Begin<T>.
    // RVA: -1 (generic). Ghidra has Begin<object>.c — single instantiation.
    public static T Begin<T>(GameObject go, float duration) where T : UITweener
    {
        T comp = go.GetComponent<T>();
        if (comp == null) comp = go.AddComponent<T>();
        comp.mStarted = false;
        comp.duration = duration;
        comp.mDuration = 0f;
        comp.enabled = true;
        return comp;
    }

    // Virtual hooks — concrete tweens override to capture current values.
    public virtual void SetStartToCurrentValue() { }
    public virtual void SetEndToCurrentValue() { }

    // Source: Ghidra (no .ctor.c) — inferred init pattern from field default values.
    // RVA: 0x19FA8E4
    protected UITweener()
    {
        animationCurve = new AnimationCurve(new Keyframe[2] {
            new Keyframe(0f, 0f, 0f, 1f),
            new Keyframe(1f, 1f, 1f, 0f),
        });
        ignoreTimeScale = true;
        duration = 1.0f;
        onFinished = new List<EventDelegate>();
        // mAmountPerDelta initially 1000f (from .ctor decompile note — backing slot 0x6C).
        mAmountPerDelta = 1000.0f;
    }

    // Source: dump.cs TDI 266
    public enum Method { Linear, EaseIn, EaseOut, EaseInOut, BounceIn, BounceOut }

    // Source: dump.cs TDI 267
    public enum Style { Once, Loop, PingPong }
}
