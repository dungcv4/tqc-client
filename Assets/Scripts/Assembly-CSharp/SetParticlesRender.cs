// Source: Ghidra work/06_ghidra/decompiled_full/SetParticlesRender/ (7 .c, all 1-1)
// Source: dump.cs TypeDefIndex 49 — SetParticlesRender : MonoBehaviour
// Fields per dump.cs:
//   OrderInLayer@0x20, Ps_r@0x28, AutoFudge@0x30, chooseNum@0x34, choose@0x38,
//   once@0x40, RendererSortingOrder@0x48

using UnityEngine;

[AddComponentMenu("UJ RD1/WndForm/Progs/Particle Layer")]
public class SetParticlesRender : MonoBehaviour
{
    public int OrderInLayer;                      // 0x20
    public ParticleSystemRenderer[] Ps_r;         // 0x28
    public bool AutoFudge;                        // 0x30
    public int chooseNum;                         // 0x34
    public GameObject[] choose;                   // 0x38
    private bool once;                            // 0x40
    public GameObject RendererSortingOrder;       // 0x48

    // Source: Ghidra Start.c RVA 0x15AEE88 — Body: once = true.
    private void Start()
    {
        once = true;
    }

    // Source: Ghidra OnEnable.c RVA 0x15AF214
    // 1-1: once = true; if chooseNum > 0:
    //   target = choose[chooseNum - 1]; if !target.activeSelf → ClChoose(); OpChoose();
    private void OnEnable()
    {
        once = true;
        if (chooseNum > 0)
        {
            if (choose == null) throw new System.NullReferenceException();
            uint idx = (uint)chooseNum - 1u;
            if (idx >= (uint)choose.Length) throw new System.IndexOutOfRangeException();
            GameObject target = choose[idx];
            if (target == null) throw new System.NullReferenceException();
            if (!target.activeSelf)
            {
                ClChoose();
                OpChoose();
            }
        }
    }

    // Source: Ghidra Update.c RVA 0x15AEE94
    // 1-1: if !once OR OrderInLayer != 0 → return.
    //   parentSPR = gameObject.GetComponentInParent<SetParticlesRender>();
    //   if parentSPR == null → return.
    //   OrderInLayer = parentSPR.OrderInLayer; chooseNum = parentSPR.chooseNum;
    //   if Ps_r != null: iterate ps_r elements i=4.. while non-null, set sortingOrder = OrderInLayer.
    //   Fallback path (Ps_r is null OR all set):
    //     if RendererSortingOrder != null:
    //       renderers = RendererSortingOrder.GetComponentsInChildren<Renderer>();
    //       for each renderer r: r.sortingOrder = OrderInLayer; Debug.Log(r.name + ": " + r.sortingOrder);
    //   ClChoose(); OpChoose(); once = false;
    private void Update()
    {
        if (!once) return;
        if (OrderInLayer != 0) return;
        GameObject go = gameObject;
        if (go == null) return;
        SetParticlesRender parentSPR = go.GetComponentInParent<SetParticlesRender>();
        if (parentSPR == null) return;
        OrderInLayer = parentSPR.OrderInLayer;
        chooseNum = parentSPR.chooseNum;
        // Ghidra: iterate Ps_r from index 4 upward while non-null
        bool fellThroughToFallback = true;
        if (Ps_r != null)
        {
            for (int i = 4; i < Ps_r.Length; i++)
            {
                var psr = Ps_r[i];
                if (psr == null) break;
                psr.sortingOrder = OrderInLayer;
                fellThroughToFallback = false;
            }
        }
        if (fellThroughToFallback)
        {
            if (RendererSortingOrder != null)
            {
                var renderers = RendererSortingOrder.GetComponentsInChildren<Renderer>();
                if (renderers != null)
                {
                    for (int i = 0; i < renderers.Length; i++)
                    {
                        var r = renderers[i];
                        if (r == null) throw new System.NullReferenceException();
                        r.sortingOrder = OrderInLayer;
                        Debug.Log(r.name + ": " + r.sortingOrder);
                    }
                }
            }
        }
        ClChoose();
        OpChoose();
        once = false;
    }

    // Source: Ghidra ClChoose.c RVA 0x15AF14C
    // 1-1: for each GameObject g in choose: g.SetActive(false).
    private void ClChoose()
    {
        if (choose == null) throw new System.NullReferenceException();
        for (int i = 0; i < choose.Length; i++)
        {
            var g = choose[i];
            if (g == null) throw new System.NullReferenceException();
            g.SetActive(false);
        }
    }

    // Source: Ghidra OpChoose.c RVA 0x15AF1C0
    // 1-1: if chooseNum < 1 return; choose[chooseNum - 1].SetActive(true).
    private void OpChoose()
    {
        if (chooseNum < 1) return;
        if (choose == null) throw new System.NullReferenceException();
        uint idx = (uint)chooseNum - 1u;
        if (idx >= (uint)choose.Length) return;
        var g = choose[idx];
        if (g == null) throw new System.NullReferenceException();
        g.SetActive(true);
    }

    // Source: Ghidra ForceUpdateOrder.c RVA 0x15AF284
    // 1-1: once = true; OrderInLayer = 0.
    public void ForceUpdateOrder()
    {
        once = true;
        OrderInLayer = 0;
    }

    // Source: Ghidra .ctor not exported (likely empty body).
    public SetParticlesRender()
    {
    }
}
