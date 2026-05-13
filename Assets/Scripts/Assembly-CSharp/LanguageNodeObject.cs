// Source: Ghidra work/06_ghidra/decompiled_full/LanguageNodeObject/ (1 .c) — setNodeText ported 1-1.
// Source: dump.cs TypeDefIndex 665
// Field layout: spCheck@0x20, lbText@0x28, lanBoard@0x30, clickArea@0x38

using UnityEngine;
using UnityEngine.UI;

public class LanguageNodeObject : MonoBehaviour
{
    public GameObject spCheck;
    public Text lbText;
    public RectTransform lanBoard;
    public Button clickArea;

    // Source: Ghidra setNodeText.c  RVA 0x18C767C
    // text = this.lbText (offset 0x28); resmgr = ResMgr.Instance (PTR_DAT_034481e8 + 0xb8); if both
    // non-null: text.text = resmgr.GetBasicUIText(baseUI_id). Vtable slot 0x5e8 = UI.Text.set_text.
    public void setNodeText(int baseUI_id)
    {
        Text text = this.lbText;
        ResMgr mgr = ResMgr.Instance;
        if (mgr == null) throw new System.NullReferenceException();
        string str = mgr.GetBasicUIText(baseUI_id);
        if (text == null) throw new System.NullReferenceException();
        text.text = str;
    }

    // Source: Ghidra (no .ctor.c) — default MonoBehaviour ctor. RVA 0x18C7708.
    public LanguageNodeObject() { }
}
