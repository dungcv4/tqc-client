// Source: Ghidra work/06_ghidra/decompiled_full/SMapTextureMgr/{_ctor,get_Instance,GetSMapSprite}.c
// RVAs: ctor 0x18CE648, get_Instance 0x18CE650, GetSMapSprite 0x18CE6D8.
// dump.cs TypeDefIndex 688:
//   private Image _img;                              // 0x10
//   private SMapTextureMgr.OnFinished _onFinished;   // 0x18
//   private static SMapTextureMgr _instance;         // 0x0
//   public static SMapTextureMgr Instance { get; }
//   public Sprite GetSMapSprite(string smapName);
//
// Lua usage: WndForm/WndForm_MainSMap.lua:601
//   self._smapImg.sprite = SMapTextureMgr.Instance:GetSMapSprite(tostring(stageData.smap_pic))
// Without a registered wrap, lua `SMapTextureMgr` global is nil → V_Create exception cascade
// from WndForm_MainSMap → MapInfoMgr → SceneMgr loaded-callback path.

using UnityEngine;
using UnityEngine.UI;

public class SMapTextureMgr
{
    public delegate void OnFinished(Image __img);

    // _img @ 0x10. Currently unused by GetSMapSprite — kept for layout parity / future OnFinished plumbing.
    private Image _img;

    // _onFinished @ 0x18. Delegate fired after async sprite load completes (not used by GetSMapSprite path).
    private OnFinished _onFinished;

    // _instance @ 0x0 (static).
    private static SMapTextureMgr _instance;

    // Source: Ghidra get_Instance.c RVA 0x18CE650
    // 1-1 mapping (lazy singleton):
    //   if (_instance == null) {
    //       SMapTextureMgr tmp = new SMapTextureMgr();   // FUN_01560214 = il2cpp_object_new
    //       System_Object__ctor(tmp);
    //       _instance = tmp;                              // write barrier (thunk_FUN_015ee8c4)
    //   }
    //   return _instance;
    public static SMapTextureMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SMapTextureMgr();
            }
            return _instance;
        }
    }

    // Source: Ghidra _ctor.c RVA 0x18CE648
    // Body: System.Object..ctor(this) — C# implicit. No field initialisation beyond defaults.
    public SMapTextureMgr() { }

    // Source: Ghidra GetSMapSprite.c RVA 0x18CE6D8
    // 1-1 mapping (control flow exactly matches the .c):
    //   ResMgr.cctor;                                       // class-init guard
    //   if (ResMgr.Instance != null) {                      // **(lVar2 + 0xb8) != 0 outer if
    //       if (ResMgr.Instance.SMapBundleOP == null)       // _instance + 0x68 (SMapBundleOP)
    //           return null;
    //       ResMgr.cctor;                                   // duplicated class-init (Cpp2IL leaves it twice)
    //       if (ResMgr.Instance != null) {                  // inner re-check (Cpp2IL artifact)
    //           AssetBundleOP smap = ResMgr.Instance.SMapBundleOP;
    //           Type t = typeof(UnityEngine.Sprite);        // PTR_DAT_0345a358 → GetTypeFromHandle
    //           if (smap != null) {
    //               UnityEngine.Object o = smap.Load(smapName, t);
    //               if (o == null) return null;
    //               if (o.GetType() != typeof(Sprite)) return null;  // klass-ptr check
    //               return (Sprite)o;
    //           }
    //       }
    //   }
    //   throw new NullReferenceException();                 // FUN_015cb8fc fall-through
    public Sprite GetSMapSprite(string smapName)
    {
        ResMgr rm = ResMgr.Instance;
        if (rm != null)
        {
            if (rm.SMapBundleOP == null) return null;
            // Inner re-check mirrors Cpp2IL's duplicated null-check on the singleton; keeps the
            // bracket-control-flow 1-1 with the .c. In managed land it's a no-op (rm can't change).
            if (rm != null)
            {
                AssetBundleOP smapBundle = rm.SMapBundleOP;
                if (smapBundle != null)
                {
                    UnityEngine.Object loaded = smapBundle.Load(smapName, typeof(UnityEngine.Sprite));
                    if (loaded == null) return null;
                    // Ghidra `if (*plVar3 == *(long *)PTR_DAT_03459188)` is the runtime klass-pointer
                    // check; managed equivalent is `loaded is Sprite` (the as-cast result).
                    Sprite sprite = loaded as Sprite;
                    if (sprite == null) return null;
                    return sprite;
                }
            }
        }
        throw new System.NullReferenceException();
    }
}
