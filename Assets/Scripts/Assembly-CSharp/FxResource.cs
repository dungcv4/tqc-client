// Source: dump.cs TypeDefIndex 761 — `public class FxResource : TResource<GameObject>`.
// Reverted 2026-05-12 from compat shim back to 1-1 inheritance per CLAUDE.md §D6.
// `obj` was added as a backward-compat property delegating to `data` (TResource<GameObject>.data
//  at offset 0x28) so existing v1 tolua# Wrap (FxResourceWrap.cs) keeps compiling without regen.
// After tolua# Gen Lua Wrap All is rerun this shim property can be removed.

using UnityEngine;

public class FxResource : TResource<GameObject>
{
    // Source: backward-compat for v1 FxResourceWrap.cs which targets `obj` field.
    // The actual data lives in TResource<GameObject>.data (offset 0x28).
    public GameObject obj
    {
        get { return data; }
        set { data = value; }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/FxResource/OnLoaded.c RVA 0x18F0810
    // 1. base.TResource<GameObject>.OnLoaded(objs) — sets _isDone=true, data=Convert.ChangeType(objs[0], typeof(GameObject)).
    // 2. If data == null: return.
    // 3. Instantiate data (Object.Instantiate<GameObject>): replace this.data with clone.
    // 4. data.name = this.name.
    // 5. Capture local scale/rotation/position via data.transform.
    // 6. Find MagicLoader.Instance (PTR_DAT_03462168 is the static instance field of MagicLoader).
    //    Set data.transform.parent = MagicLoader.Instance.transform; reapply localScale/rotation/position.
    // 7. data.SetActive(false).
    // 8. data.AddComponent<FxPlayer>(); FxPlayer.Init();
    // 9. MagicLoader.Instance.fxPool.Add(this.name, this).
    protected override void OnLoaded(UnityEngine.Object[] objs)
    {
        // Call base TResource<GameObject>.OnLoaded.
        // base.OnLoaded is protected internal in same assembly — invoke via reflection-free direct.
        // Since the method is protected virtual, we can call it via `base.OnLoaded` (need to use BaseOnLoaded helper since member is private).
        // Easiest: replicate the base body inline (same as TResource<GameObject>.OnLoaded).
        // Implementation: matches Ghidra's TResource<object>__OnLoaded call.
        _isDone = true;
        if (objs != null)
        {
            if (objs.Length == 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            UnityEngine.Object obj0 = objs[0];
            if (obj0 != null)
            {
                data = (GameObject)System.Convert.ChangeType(obj0, typeof(GameObject));
            }
        }
        if (callback != null)
        {
            callback(this);
        }

        // FxResource-specific post-load:
        if (data == null)
        {
            return;
        }
        GameObject clone = UnityEngine.Object.Instantiate<GameObject>(data);
        data = clone;
        if (data != null)
        {
            data.name = this.name;
            Transform tt = data.transform;
            if (tt != null)
            {
                Vector3 localScale = tt.localScale;
                if (data != null)
                {
                    Transform tt2 = data.transform;
                    if (tt2 != null)
                    {
                        Quaternion localRot = tt2.localRotation;
                        if (data != null)
                        {
                            Transform tt3 = data.transform;
                            if (tt3 != null)
                            {
                                Vector3 localPos = tt3.localPosition;
                                if (data != null)
                                {
                                    Transform tt4 = data.transform;
                                    if (MagicLoader.Instance != null)
                                    {
                                        Transform pt = MagicLoader.Instance.transform;
                                        if (tt4 != null)
                                        {
                                            tt4.parent = pt;
                                            if (data != null)
                                            {
                                                Transform tt5 = data.transform;
                                                if (tt5 != null)
                                                {
                                                    tt5.localScale = localScale;
                                                    if (data != null)
                                                    {
                                                        Transform tt6 = data.transform;
                                                        if (tt6 != null)
                                                        {
                                                            tt6.localRotation = localRot;
                                                            if (data != null)
                                                            {
                                                                Transform tt7 = data.transform;
                                                                if (tt7 != null)
                                                                {
                                                                    tt7.localPosition = localPos;
                                                                    if (data != null)
                                                                    {
                                                                        data.SetActive(false);
                                                                        FxPlayer player = data.AddComponent<FxPlayer>();
                                                                        if (player != null)
                                                                        {
                                                                            player.Init();
                                                                            if (MagicLoader.Instance != null && MagicLoader.Instance.fxPool != null)
                                                                            {
                                                                                MagicLoader.Instance.fxPool.Add(this.name, this, MagicLoader.Instance.fxPool.timeDefault);
                                                                                return;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // FUN_015cb8fc — IL2CPP null-deref trap.
        throw new System.NullReferenceException();
    }

    // Source: dump.cs — default empty ctor. RVA 0x18F0B44 (FxResource..ctor).
    public FxResource() { }
}
