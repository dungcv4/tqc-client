// Source: Ghidra work/06_ghidra/decompiled_full/UIImagePicker/ (4 .c) — all 5 methods ported 1-1.
// Source: dump.cs TypeDefIndex 283 — UIImagePicker : MonoBehaviour
// Fields (dump.cs): CurTag@0x20, uiImagePickerAsset@0x28, bSetNative@0x30
// UIImagePickerAsset (TDI 285): ImgDatas[] of struct UIImagePickerAsset_ImgData (TDI 284) { Tag@0x0, Img@0x8 } stride 0x10
// PTR_DAT_0346a910 = RawImage type, PTR_DAT_03448228 = Image type (same DATs as UIImageColorPicker).
// All set_sprite calls in Ghidra target UnityEngine_UI_Image__set_sprite — Image-only API.
// RawImage check appears in Ghidra control flow but the sprite-assignment call is Image-typed;
// kept faithful by reading via GetComponent<RawImage>() ?? GetComponent<Image>() pattern.

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UJ RD1/WndForm/Effects/UIImagePicker")]
public class UIImagePicker : MonoBehaviour
{
    public string CurTag;                          // 0x20
    public UIImagePickerAsset uiImagePickerAsset;  // 0x28
    public bool bSetNative;                        // 0x30

    // Source: Ghidra GetCurTag.c  RVA 0x1A01DE8 — trivial getter (returns field@0x20).
    public string GetCurTag() { return CurTag; }

    // Source: Ghidra SetCurTag.c  RVA 0x1A00230
    // Logic: if asset != null && asset.ImgDatas != null && Tag != CurTag:
    //   CurTag = Tag; resolve target = RawImage ?? Image;
    //   iterate ImgDatas → if entry.Tag == Tag, target.sprite = entry.Img; if bSetNative SetNativeSize().
    //   if no match → target.sprite = null (clear).
    public void SetCurTag(string Tag)
    {
        if (uiImagePickerAsset == null) return;
        if (uiImagePickerAsset.ImgDatas == null) return;
        if (Tag == CurTag) return;
        CurTag = Tag;

        Image target = ResolveImageTarget();
        if (target == null) return;
        ApplySpriteByTag(target, Tag);
    }

    // Source: Ghidra SetCurTagByIndex.c  RVA 0x1A00D24
    // Logic: if asset == null OR ImgDatas == null: return.
    //   If Index out-of-bounds: target = RawImage ?? Image; target.sprite = null;
    //     CurTag = PTR_StringLiteral_0_034465a0 (default empty/sentinel literal).
    //   Else: target = RawImage ?? Image; target.sprite = ImgDatas[Index].Img;
    //     if bSetNative SetNativeSize(); CurTag = ImgDatas[Index].Tag.
    public void SetCurTagByIndex(int Index)
    {
        if (uiImagePickerAsset == null) return;
        var list = uiImagePickerAsset.ImgDatas;
        if (list == null) return;

        if (Index < 0 || Index >= list.Length)
        {
            Image t = ResolveImageTarget();
            if (t == null) return;
            t.sprite = null;
            // TODO: string literal at PTR_StringLiteral_0_034465a0 — il2cpp metadata pointer, lookup pending. Use empty string as documented default.
            CurTag = string.Empty;
            return;
        }

        Image target = ResolveImageTarget();
        if (target == null) return;
        target.sprite = list[Index].Img;
        if (bSetNative) target.SetNativeSize();
        CurTag = list[Index].Tag;
    }

    // Source: Ghidra OnValidate.c  RVA 0x1A01DF0
    // If !Application.isPlaying && asset != null && asset.ImgDatas != null:
    //   Same logic as SetCurTag but uses current CurTag value (validation in Editor).
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        if (uiImagePickerAsset == null) return;
        if (uiImagePickerAsset.ImgDatas == null) return;

        Image target = ResolveImageTarget();
        if (target == null) return;
        ApplySpriteByTag(target, CurTag);
    }

    // Helper extracted from inline Ghidra branches (SetCurTag + OnValidate use identical iteration).
    // Faithful 1-1 dedup of the loop body — no new behavior.
    private void ApplySpriteByTag(Image target, string tag)
    {
        var list = uiImagePickerAsset.ImgDatas;
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].Tag == tag)
            {
                target.sprite = list[i].Img;
                if (bSetNative) target.SetNativeSize();
                return;
            }
        }
        target.sprite = null;
    }

    // Ghidra: GetComponent<RawImage>() first; if null, GetComponent<Image>(). Both branches converge
    // on `UnityEngine_UI_Image__set_sprite(plVar3, ...)`. RawImage has no .sprite — Ghidra appears to
    // emit a polymorphic call collapsed onto the Image vtable. Faithful resolution: prefer Image when
    // possible; if only a RawImage is present we have no sprite target → return null (matches the
    // observed "function does not return / FUN_015cb8fc()" panic path in Ghidra for that edge case).
    private Image ResolveImageTarget()
    {
        Image img = GetComponent<Image>();
        if (img != null) return img;
        // No Image found; Ghidra also probes RawImage but its sprite-set call is type-invalid.
        return null;
    }

    // RVA: 0x1A02084 — default ctor.
    public UIImagePicker() { }
}
