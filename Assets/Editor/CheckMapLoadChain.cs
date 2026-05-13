// Source: Manual diag — verify MapInfoMgr -> WrdFileMgr/BinFileMgr -> AssetBundleOP chain.
// Phase C verification: after ProcessInMap, Lua calls MapInfoMgr:EnterMapProcess(2)
// which routes to WrdFileMgr.Instance.loadLevel(...) and BinFileMgr.Instance.loadLevel(...),
// then SceneMgr:SwitchScene("stage11", ...).
//
// This script does a synthetic-Editor-mode test of:
//   1) ResMgr.Instance != null and MapDataBundleOP loaded
//   2) WrdFileMgr.Instance.loadLevel(11) runs without exception
//   3) WrdFileMgr.Instance.getMapWidth() returns > 0 after load
//   4) BinFileMgr.Instance.loadLevel(11) runs without exception
//
// DO NOT execute during normal play — only invoke from Unity menu after boot reached map
// state (or after a manual GetBundle("mapdata") call).
//
// Pattern follows CheckIconBundle / CheckBootState (Editor/CheckXxx.cs).
using UnityEngine;

public static class CheckMapLoadChain
{
    public static void Execute()
    {
        Debug.Log("[CheckMapLoadChain] === Phase C scene-load chain diag ===");

        // (1) ResMgr.Instance + MapDataBundleOP
        var rm = ResMgr.Instance;
        if (rm == null)
        {
            Debug.LogError("[CheckMapLoadChain] ResMgr.Instance == null — boot state machine has not reached ResMgr.cctor");
            return;
        }
        Debug.Log("[CheckMapLoadChain] ResMgr.Instance  = OK");
        Debug.Log("[CheckMapLoadChain] MapDataBundleOP  = " + (rm.MapDataBundleOP == null ? "NULL (mapdata bundle NOT loaded by ProcessLunchGame state 0x2c yet)" : "OK"));
        Debug.Log("[CheckMapLoadChain] SMapBundleOP     = " + (rm.SMapBundleOP == null ? "NULL" : "OK"));
        Debug.Log("[CheckMapLoadChain] LuaBundleOP      = " + (rm.LuaBundleOP == null ? "NULL" : "OK"));

        if (rm.MapDataBundleOP == null)
        {
            Debug.LogWarning("[CheckMapLoadChain] cannot continue — MapDataBundleOP null; run after boot reaches 'MapData' state");
            return;
        }

        // (2) WrdFileMgr.loadLevel — Lua side calls with wrd_mapid (likely 1..N)
        // For map=2 path Lua passes stageData.wrd_mapid (data-driven). We use 11 here as a
        // synthetic probe matching stage11 -> Level011.wrd asset name pattern.
        int probeLevel = 11;
        Debug.Log("[CheckMapLoadChain] --- WrdFileMgr.loadLevel(" + probeLevel + ") ---");
        try
        {
            bool wrdOk = WrdFileMgr.Instance.loadLevel(probeLevel);
            Debug.Log("[CheckMapLoadChain] WrdFileMgr.loadLevel returned " + wrdOk);
            if (wrdOk)
            {
                int w = WrdFileMgr.Instance.getMapWidth();
                int h = WrdFileMgr.Instance.getMapHeight();
                Debug.Log("[CheckMapLoadChain] WrdFileMgr mapWidth = " + w + ", mapHeight = " + h);
                if (w <= 0 || h <= 0)
                {
                    Debug.LogWarning("[CheckMapLoadChain] map dimensions are non-positive — header parse may be wrong");
                }
                var codeAry = WrdFileMgr.Instance.getMapCode();
                Debug.Log("[CheckMapLoadChain] mapCodeAry length = " + (codeAry == null ? -1 : codeAry.Length));
                var blocks = WrdFileMgr.Instance.getMapBlock();
                Debug.Log("[CheckMapLoadChain] mapBlock count    = " + (blocks == null ? -1 : blocks.Length));
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("[CheckMapLoadChain] WrdFileMgr.loadLevel EXC: " + e.GetType().Name + " " + e.Message + "\n" + e.StackTrace);
        }

        // (3) BinFileMgr.loadLevel
        Debug.Log("[CheckMapLoadChain] --- BinFileMgr.loadLevel(" + probeLevel + ") ---");
        try
        {
            bool binOk = BinFileMgr.Instance.loadLevel(probeLevel);
            Debug.Log("[CheckMapLoadChain] BinFileMgr.loadLevel returned " + binOk);
            if (binOk)
            {
                var bd = BinFileMgr.Instance.binData;
                if (bd != null && bd.headerClass != null)
                {
                    Debug.Log("[CheckMapLoadChain] BinData eveTotalNumber = " + bd.headerClass.eveTotalNumber);
                    Debug.Log("[CheckMapLoadChain] BinData dataAry.Length = " + (bd.dataAry == null ? -1 : bd.dataAry.Length));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("[CheckMapLoadChain] BinFileMgr.loadLevel EXC: " + e.GetType().Name + " " + e.Message + "\n" + e.StackTrace);
        }

        // (4) SceneMgr-side: ResMgr.LoadScene("stage11") — exercises ResourcesLoader.GetObjectTypeAssetDynamic.
        // This is what Lua SceneMgr:UpdateLoading calls each frame until the bundle is ready.
        Debug.Log("[CheckMapLoadChain] --- ResMgr.LoadScene(\"stage11\") (one-shot probe) ---");
        try
        {
            float p = rm.LoadScene("stage11");
            Debug.Log("[CheckMapLoadChain] ResMgr.LoadScene(stage11) progress = " + p);
            bool ready = rm.IsSceneReady("stage11");
            Debug.Log("[CheckMapLoadChain] ResMgr.IsSceneReady(stage11) = " + ready);
        }
        catch (System.Exception e)
        {
            Debug.LogError("[CheckMapLoadChain] ResMgr.LoadScene EXC: " + e.GetType().Name + " " + e.Message + "\n" + e.StackTrace);
        }

        Debug.Log("[CheckMapLoadChain] === Phase C diag complete ===");
    }
}
