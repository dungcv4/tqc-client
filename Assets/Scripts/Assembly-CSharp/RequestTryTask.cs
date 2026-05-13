// Source: Ghidra work/06_ghidra/decompiled_full/RequestTryTask/ (5 .c)
// Source: dump.cs TypeDefIndex 817
// Field offsets: _name@0x10, _request@0x18, _bundleBytes@0x20, _assetType@0x28 (int),
//                _waitTime@0x2C (float), _tryCount@0x30 (int).
// 0xBF800000 = IEEE float -1.0f.

using UnityEngine;

public class RequestTryTask
{
    private string _name;                          // 0x10
    private RequestFile _request;                  // 0x18
    private long _bundleBytes;                     // 0x20
    private ResourcesLoader.AssetType _assetType;  // 0x28
    private float _waitTime;                       // 0x2C
    private int _tryCount;                         // 0x30

    // Source: Ghidra get_name.c  RVA 0x1908E04 — returns field at +0x10.
    public string get_name() { return _name; }

    // Source: Ghidra get_nFetchBytes.c  RVA 0x1908E0C
    // If _request == null: 0.
    // If _request._bFinished (offset 0x11): return _bundleBytes (full size).
    // Else: return (long)(_request.progress * _bundleBytes); special: if NaN/Infinity return Int64.MinValue.
    public long get_nFetchBytes()
    {
        if (_request == null) return 0;
        if (_request.isDone) return _bundleBytes;
        float fProg = _request.progress;
        double scaled = (double)fProg * (double)_bundleBytes;
        if (float.IsInfinity(fProg) || float.IsNaN(fProg)) return long.MinValue;
        return (long)scaled;
    }

    // Source: dump.cs RVA 0x1908E60 — no .ctor.c, body inferred from field-init pattern + dump.cs sig.
    // Assigns _name, _assetType from args; other fields default (_bundleBytes=0, _waitTime=-1f, _tryCount=0).
    public RequestTryTask(string name, ResourcesLoader.AssetType assetType)
    {
        _name = name;
        _assetType = assetType;
        _bundleBytes = 0;
        _waitTime = -1f;
        _tryCount = 0;
        _request = null;
    }

    // Source: Ghidra Reset.c  RVA 0x1908EA4
    // Ghidra writes 8-byte 0xBF800000 at offset 0x2C: _waitTime = -1f AND _tryCount = 0.
    public void Reset()
    {
        _waitTime = -1f;
        _tryCount = 0;
    }

    // Source: Ghidra _CBAssetBundle.c  RVA 0x1908EB0
    // ResourcesLoader.addPreDownloadedBundle((int)_assetType, bundleOP).
    public void _CBAssetBundle(AssetBundleOP bundleOP)
    {
        ResourcesLoader.addPreDownloadedBundle(_assetType, bundleOP);
    }

    // Source: Ghidra ProcessOne.c  RVA 0x1908F1C
    // State machine for downloading bundle with retries:
    //   tryCount > 5 → E_NET_LOST.
    //   waitTime > 0 && now < waitTime → E_WAITING (backoff).
    //   request == null → start fresh: waitTime = -1, create CBAssetBundle delegate, request = ABM.LoadAssetBundle(name, cb).
    //   request != null:
    //     !_request.isDone → E_FETCHING.
    //     err = _request.error; IsNullOrEmpty(err) → E_OK.
    //     UJDebug.LogError(err); tryCount++; waitTime = now + 5; request = null.
    //   Default tail return: E_FETCHING.
    public RequestTryTask.EState ProcessOne()
    {
        if (_tryCount > 5) return EState.E_NET_LOST;
        float now = UnityEngine.Time.realtimeSinceStartup;
        if (_waitTime > 0f && now < _waitTime) return EState.E_WAITING;

        if (_request == null)
        {
            _waitTime = -1f;
            AssetBundleManager abm = AssetBundleManager.Instance;
            AssetBundleManager.CBAssetBundle cb = new AssetBundleManager.CBAssetBundle(this._CBAssetBundle);
            if (abm == null) throw new System.NullReferenceException();
            _request = abm.LoadAssetBundle(_name, cb);
        }
        else
        {
            if (!_request.isDone) return EState.E_FETCHING;
            string err = _request.error;
            if (string.IsNullOrEmpty(err)) return EState.E_OK;
            UJDebug.LogError(err);
            _tryCount++;
            _waitTime = UnityEngine.Time.realtimeSinceStartup + 5f;
            _request = null;
        }
        return EState.E_FETCHING;
    }

    // Source: dump.cs TypeDefIndex 816
    public enum EState
    {
        E_OK = 0,
        E_FETCHING = 1,
        E_WAITING = 2,
        E_NET_LOST = 3,
    }
}
