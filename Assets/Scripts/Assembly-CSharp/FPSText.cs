// Source: work/03_il2cpp_dump/dump.cs TypeDefIndex 783 + Ghidra decompiled_full/FPSText/*.c

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSText : MonoBehaviour
{
    private static FPSText m_instance;
    public float updateInterval;
    private float accum;
    private int frames;
    private float timeleft;
    public Text fpsText;

    // Source: Ghidra get_Instance.c RVA 0x018f2a5c — return m_instance
    public static FPSText get_Instance()
    {
        return m_instance;
    }

    // Source: Ghidra Start.c RVA 0x018f2aa4
    // 1-1:
    //   m_instance = this;
    //   if (!fpsText) fpsText = GetComponent<Text>();
    //   if (fpsText != null) { fpsText.alignment = MiddleCenter; fpsText.fontSize = 16; }
    //   fpsText.gameObject.SetActive(false);
    //   timeleft = updateInterval;
    //   DontDestroyOnLoad(transform.parent);
    //   StartCoroutine(UpdateCounter());
    private void Start()
    {
        m_instance = this;
        if (!fpsText)
        {
            fpsText = GetComponent<Text>();
        }
        if (fpsText != null)
        {
            fpsText.alignment = TextAnchor.MiddleCenter;
            fpsText.fontSize = 16;
        }
        if (fpsText == null) throw new System.NullReferenceException();
        var go = fpsText.gameObject;
        if (go == null) throw new System.NullReferenceException();
        go.SetActive(false);
        timeleft = updateInterval;
        var tr = transform;
        if (tr == null) throw new System.NullReferenceException();
        UnityEngine.Object.DontDestroyOnLoad(tr.parent);
        StartCoroutine(UpdateCounter());
    }

    // Source: Ghidra UpdateCounter.c RVA 0x018f2c40
    // The Ghidra wrapper allocates a compiler-generated enumerator class capturing `this`;
    // the actual coroutine body (in <UpdateCounter>d__9) is not exported. Standard FPS-text
    // pattern: update text every `updateInterval` seconds with frames/sec average.
    // [Deviation note: <UpdateCounter>d__9 body inferred from typical Unity FPS overlay pattern;
    //  variables match the field set (accum, frames, timeleft, updateInterval, fpsText).]
    protected IEnumerator UpdateCounter()
    {
        while (true)
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            frames++;
            if (timeleft <= 0f)
            {
                float fps = accum / frames;
                if (fpsText != null)
                {
                    fpsText.text = fps.ToString("F1");
                }
                timeleft = updateInterval;
                accum = 0f;
                frames = 0;
            }
            yield return null;
        }
    }

    // Source: Ghidra SetDisplay.c RVA 0x018f2cd4 (skeleton — fpsText.gameObject.SetActive(display))
    public void SetDisplay(bool display)
    {
        if (fpsText == null) throw new System.NullReferenceException();
        var go = fpsText.gameObject;
        if (go == null) throw new System.NullReferenceException();
        go.SetActive(display);
    }

    // Source: dump.cs RVA 0x18F2D04 — default ctor
    public FPSText() { }
}
