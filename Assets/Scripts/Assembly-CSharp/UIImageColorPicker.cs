// Source: Ghidra work/06_ghidra/decompiled_full/UIImageColorPicker/ (3 .c) — all 4 methods ported 1-1.
// Source: dump.cs TypeDefIndex 280
// Note: PTR_DAT_0346a910 = RawImage type, PTR_DAT_03448228 = Image type.
// UIImageColorPickerAsset._ColorDatas[] entries have Tag + _Color fields (24-byte stride matches Ghidra).

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/UIImageColorPicker")]
public class UIImageColorPicker : MonoBehaviour
{
    public string CurTag;                          // 0x20
    public UIImageColorPickerAsset uiColorPickerAsset; // 0x28

    // Source: Ghidra GetCurTag.c  RVA 0x1A018A0 — trivial getter.
    public string GetCurTag() { return CurTag; }

    // Source: Ghidra SetCurTag.c  RVA 0x1A018A8
    // If asset != null && asset._ColorDatas != null && Tag != CurTag:
    //   CurTag = Tag
    //   target = GetComponent<RawImage>() ?? GetComponent<Image>()
    //   if target != null: iterate _ColorDatas → if entry.Tag == Tag, target.color = entry._Color
    public void SetCurTag(string Tag)
    {
        if (uiColorPickerAsset == null) return;
        if (uiColorPickerAsset._ColorDatas == null) return;
        if (Tag == CurTag) return;
        CurTag = Tag;
        RawImage rawImg = GetComponent<RawImage>();
        if (rawImg != null)
        {
            ApplyColor(rawImg, null, Tag);
        }
        else
        {
            Image img = GetComponent<Image>();
            if (img != null)
            {
                ApplyColor(null, img, Tag);
            }
        }
    }

    // Helper extracted from inline Ghidra branches (both SetCurTag and OnValidate use identical
    // _ColorDatas iteration). Faithful 1-1 — no new behavior, just dedup of the loop body.
    private void ApplyColor(RawImage rawImg, Image img, string tag)
    {
        if (uiColorPickerAsset == null || uiColorPickerAsset._ColorDatas == null) return;
        var list = uiColorPickerAsset._ColorDatas;
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].Tag == tag)
            {
                Color c = list[i]._Color;
                if (rawImg != null) rawImg.color = c;
                else if (img != null) img.color = c;
                return;
            }
        }
    }

    // Source: Ghidra OnValidate.c  RVA 0x1A01B04
    // If !Application.isPlaying && asset != null && asset._ColorDatas != null:
    //   Same logic as SetCurTag but uses current CurTag value (validation in Editor).
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        if (uiColorPickerAsset == null) return;
        if (uiColorPickerAsset._ColorDatas == null) return;
        RawImage rawImg = GetComponent<RawImage>();
        if (rawImg != null)
        {
            ApplyColor(rawImg, null, CurTag);
        }
        else
        {
            Image img = GetComponent<Image>();
            if (img != null)
            {
                ApplyColor(null, img, CurTag);
            }
        }
    }

    // RVA: 0x1A01D88 — default ctor.
    public UIImageColorPicker() { }
}
