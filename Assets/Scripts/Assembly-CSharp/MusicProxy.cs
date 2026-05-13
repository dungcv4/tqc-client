// Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/ (14 .c files) + dump.cs TypeDefIndex 145
// Ported 1-1 from libil2cpp.so per CLAUDE.md §D6 / STRICT_RULES §2.
// String literals (work/03_il2cpp_dump/stringliteral.json):
//   #8221="MusicVolume"  #188=" Play music use resource file :"
//   #187=" Play music use cache file :"  #3211="Audio/Musics/"
//   #186=" Play music use bundle file :"  #8218="Music load is null.."
//   #8220="MusicProxy Clear Audio Cache Finish"
// Field offsets verified from dump.cs (instance from 0x20; statics 0x0..0x18).

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UJ RD1/Music Proxy")]
public sealed class MusicProxy : MonoBehaviour
{
    // Static field layout (from dump.cs, addresses 0x0..0x18 in static struct):
    private const string CPrefKey_MusicVolume = "MusicVolume";
    public const float CMinVolume = 0f;
    public const float CMaxVoulume = 1f;
    private static bool s_loaded;           // 0x0
    private static uint s_volumeMask;       // 0x4
    private static float s_masterVolume;    // 0x8
    private static MusicProxy s_main;       // 0x10
    private const uint EFadeOut = 1;
    private const uint EFadeIn  = 2;
    public bool _isMain;                                // 0x20
    private AudioSource _audio;                         // 0x28
    private float _volume;                              // 0x30
    private uint _volumeMask;                           // 0x34
    private bool _mute;                                 // 0x38
    private float _muteVolume;                          // 0x3C
    private float _realTime;                            // 0x40
    private uint _fadeMode;                             // 0x44
    private float _fadeTime;                            // 0x48
    private float _fadeIntervalIn;                      // 0x4C
    private float _fadeIntervalOut;                     // 0x50
    private AudioClip _nextClip;                        // 0x58
    private AssetBundleOP _nextClipBundleOP;            // 0x60
    private float _nextVolume;                          // 0x68
    private static IUJObjectOperation op;               // static 0x18
    public Dictionary<string, AudioClipCache> _CachedAudio; // 0x70 [NoToLua]
    private uint _musicMask;                            // 0x78
    private string _musicName;                          // 0x80
    private string _preMusicName;                       // 0x88

    // Source: dump.cs TypeDefIndex 145 lines 7266-7273. Production declares BOTH the property
    // and the explicit getter method (IL2Cpp keeps them as two metadata entries). In C#
    // surface syntax we can express only one — using the property form because Lua needs
    // it visible as `MusicProxy.main` (RegVar) per AudioManager:112 / SetMusicVolume.
    // The C# auto-generated `get_main()` accessor underneath the property is semantically
    // identical to dump.cs's `public static MusicProxy get_main()` at RVA 0x017BE260.
    public static MusicProxy main { get { return s_main; } }

    // Source: dump.cs `public static float masterVolume { get; set; }` + explicit get/set methods.
    // Express as property in C#; auto-gen accessors functionally identical to dump's RVA
    // 0x017BE2B8 (getter) + 0x017BE3D8 (setter). Lua `MusicProxy.main.masterVolume = X` needs property form.
    public static float masterVolume
    {
        // get: Ghidra get_masterVolume.c RVA 0x017BE2B8
        // If !s_loaded: s_loaded=true; s_masterVolume = PlayerPrefs.GetFloat("MusicVolume",0);
        //   clamp <0 -> 0; >1 -> 1.  Then return s_masterVolume.
        get
        {
            if (!s_loaded)
            {
                s_loaded = true;
                s_masterVolume = PlayerPrefs.GetFloat(CPrefKey_MusicVolume, 0f);
                if (s_masterVolume < 0f) s_masterVolume = 0f;
                if (s_masterVolume > 1f) s_masterVolume = 1f;
            }
            return s_masterVolume;
        }
        // set: Ghidra set_masterVolume.c RVA 0x017BE3D8
        // Clamps to [0,1]; if changed write to s_masterVolume + s_loaded=true + PlayerPrefs.SetFloat + s_volumeMask++.
        set
        {
            float fVar6 = (s_masterVolume >= 0f) ? value : 0f;
            float fVar7 = (s_masterVolume <= 1f) ? fVar6 : 1f;
            if (s_masterVolume != fVar7)
            {
                s_masterVolume = fVar7;
                s_loaded = true;
                PlayerPrefs.SetFloat(CPrefKey_MusicVolume, fVar7);
                s_volumeMask = s_volumeMask + 1;
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Save.c  RVA 0x017BE4F4
    // Sets s_loaded=true, s_masterVolume=(int)newVolume(!),
    //   PlayerPrefs.SetFloat("MusicVolume", newVolume).
    // Ghidra: *(int *)(puVar4 + 8) = (int)param_1;  -- this is genuinely an int store
    //         on the float field (truncation).  Preserve 1-1.
    public static void Save(float newVolume)
    {
        s_loaded = true;
        // 1-1 with Ghidra: int store on the float slot (truncate-to-int cast).
        s_masterVolume = (float)(int)newVolume;
        PlayerPrefs.SetFloat(CPrefKey_MusicVolume, newVolume);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Start.c  RVA 0x017BE580
    private void Start()
    {
        // GetComponent<AudioSource>()
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        {
            // gameObject.AddComponent<AudioSource>()
            GameObject go = gameObject;
            if (go == null)
            {
                // Ghidra: subroutine does not return -> null deref
                throw new System.NullReferenceException();
            }
            _audio = go.AddComponent<AudioSource>();
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.priority = 0;
        }
        // _audio != null path
        _audio.loop = true;
        Object.DontDestroyOnLoad(gameObject);
        if (!_isMain)
        {
            return;
        }
        // s_main = this; _CachedAudio = new Dictionary<string, AudioClipCache>();
        s_main = this;
        _CachedAudio = new Dictionary<string, AudioClipCache>();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Update.c  RVA 0x017BE764
    private void Update()
    {
        float dt = Time.realtimeSinceStartup - _realTime;
        _realTime = Time.realtimeSinceStartup;

        // Fade-out branch
        if (_fadeMode == EFadeOut)
        {
            _fadeTime = _fadeTime - dt;
            _volumeMask = s_volumeMask;
            if (_fadeTime <= 0f)
            {
                _fadeMode = 0;
                if (_audio == null)
                {
                    throw new System.NullReferenceException();
                }
                _audio.Stop();
            }
            else
            {
                SetVolume((masterVolume * _volume * _fadeTime) / _fadeIntervalOut);
            }
        }

        // Pending next clip: if non-null & audio not playing, swap in and play
        if (_nextClip != null)
        {
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            if (!_audio.isPlaying)
            {
                _audio.clip = _nextClip;
                _volume = _nextVolume;
                float v;
                if (_fadeIntervalIn <= 0f)
                {
                    _volumeMask = s_volumeMask;
                    v = masterVolume * _volume;
                }
                else
                {
                    _fadeMode = EFadeIn;
                    _fadeTime = 0f;
                    v = 0f;
                }
                SetVolume(v);
                _audio.Play();
                _nextClip = null;
                _nextClipBundleOP = null;
            }
        }

        // Fade-in branch
        if (_fadeMode == EFadeIn)
        {
            _fadeTime = _fadeTime + dt;
            _volumeMask = s_volumeMask;
            float v2;
            if (_fadeTime >= _fadeIntervalIn)
            {
                _fadeMode = 0;
                v2 = masterVolume * _volume;
            }
            else
            {
                v2 = (masterVolume * _volume * _fadeTime) / _fadeIntervalIn;
            }
            SetVolume(v2);
        }

        // Master-volume mask change -> refresh
        if (_volumeMask != s_volumeMask)
        {
            _volumeMask = s_volumeMask;
            SetVolume(_volume * masterVolume);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Play.c  RVA 0x017BEA7C
    // 1-arg overload calls 3-arg with default volume=1.0, bundleOp=null.
    public bool Play(AudioClip clip)
    {
        return Play(clip, 1f, null);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Play.c  RVA 0x017BEA88
    // Main overload — sets _nextClip / _nextVolume / _nextClipBundleOP from args.
    // Returns true if a clip was queued (non-null), false otherwise.
    public bool Play(AudioClip clip, float volume, AssetBundleOP bundleOp)
    {
        if (clip == null)
        {
            return false;
        }
        _nextClip = clip;
        _nextVolume = volume;
        _nextClipBundleOP = bundleOp;
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Stop.c  RVA 0x017BEBC8
    // froced=true OR _fadeIntervalOut<=0: immediate stop.  Else trigger fade-out.
    public void Stop(bool froced = false)
    {
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        if (_audio.isPlaying)
        {
            _musicName = string.Empty; // StringLiteral_0 = ""
            if (_fadeIntervalOut <= 0f || froced)
            {
                if (_audio == null)
                {
                    throw new System.NullReferenceException();
                }
                _audio.Stop();
                return;
            }
            _fadeMode = EFadeOut;
            _fadeTime = _fadeIntervalOut;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/get_musicMask.c  RVA 0x017BEC6C
    public uint get_musicMask()
    {
        return _musicMask;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Play.c  RVA 0x017BEC74
    // 1-arg string overload: forwards with volume=1.0.
    public bool Play(string name)
    {
        return Play(name, 1f);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/Play.c  RVA 0x017BEC7C
    // Resolve a music clip by name.
    //   * If name is null/empty -> return false.
    //   * Normalize: name = name.PadLeft(3,'0').
    //   * If equals current _musicName -> skip (already loaded), return true.
    //   * Else: swap _preMusicName <- _musicName <- normalized; bump _musicMask;
    //     - Check s_main._CachedAudio.ContainsKey(name) -> play cached & log "Play music use cache file :"
    //     - Else try Resources.Load<AudioClip>("Audio/Musics/"+name).
    //       If found -> log "Play music use resource file :", play.
    //       Else -> async fetch via ResourcesLoader.GetObjectTypeAssetDynamic(MUSIC, name, OnMusicLoaded);
    //              op = handle; log "Play music use bundle file :".
    //   * Returns true if a play attempt was made.
    public bool Play(string name, float volume)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }
        // PadLeft(3,'0') normalization (e.g. "12" -> "012")
        string padded = name.PadLeft(3, '0');
        if (padded == _musicName)
        {
            // already playing — Ghidra still returns 1
            return true;
        }
        _preMusicName = _musicName;
        _musicName = padded;
        _musicMask = _musicMask + 1;

        // s_main._CachedAudio — Ghidra reads s_main then field 0x70.
        MusicProxy main = s_main;
        if (main == null || main._CachedAudio == null)
        {
            throw new System.NullReferenceException();
        }
        if (main._CachedAudio.ContainsKey(padded))
        {
            UJDebug.Log(" Play music use cache file :" + padded);
            // re-fetch (Ghidra re-loads s_main / _CachedAudio identically)
            MusicProxy main2 = s_main;
            if (main2 == null || main2._CachedAudio == null)
            {
                throw new System.NullReferenceException();
            }
            AudioClipCache cache = main2._CachedAudio[padded];
            if (cache == null)
            {
                throw new System.NullReferenceException();
            }
            // Tail-recurse into AudioClip overload: Play(cache.AudioClip, 1.0f, ...)
            // (Ghidra calls Play(0x3f800000, this, cache.AudioClip) — 3rd slot of AudioClipCache @ 0x18 = AudioClip.)
            return Play(cache.AudioClip);
        }

        // Try Resources.Load<AudioClip>("Audio/Musics/" + padded)
        string resourcePath = "Audio/Musics/" + padded;
        AudioClip res = Resources.Load(resourcePath, typeof(AudioClip)) as AudioClip;
        if (res != null)
        {
            UJDebug.Log(" Play music use resource file :" + padded);
            return Play(res);
        }

        // Async dynamic bundle load: callback -> OnMusicLoaded.
        CBNewObjectLoader cb = new CBNewObjectLoader(OnMusicLoaded);
        op = ResourcesLoader.GetObjectTypeAssetDynamic((int)ResourcesLoader.AssetType.MUSIC, padded, cb);
        UJDebug.Log(" Play music use bundle file :" + padded);
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/OnMusicLoaded.c  RVA 0x017BF114
    // Receives an Object[] from ResourcesLoader.  If music[0] is non-null AudioClip,
    // calls Play(clip, 1.0f, null).  Else logs "Music load is null..".
    public void OnMusicLoaded(UnityEngine.Object[] music)
    {
        if (music == null)
        {
            throw new System.NullReferenceException();
        }
        if (music.Length != 0)
        {
            UnityEngine.Object first = music[0];
            if (first == null)
            {
                // Ghidra: if (op_Equality(uVar4,0,0) & 1) -> log-null branch
                UJDebug.LogError("Music load is null..");
                return;
            }
            // Cast to AudioClip (Ghidra checks vtable == AudioClip)
            AudioClip clip = first as AudioClip;
            Play(clip);
            return;
        }
        UJDebug.LogError("Music load is null..");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/PlayPreMusic.c  RVA 0x017BF22C
    // Body: Play(_preMusicName, 1.0f).  Ghidra signature has float param but body ignores it.
    // (Per dump.cs:  public bool PlayPreMusic(float volume).)
    public bool PlayPreMusic(float volume)
    {
        return Play(_preMusicName, 1f);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/SetVolume.c  RVA 0x017BEA40
    // If _mute: _muteVolume = volume; set AudioSource volume to 0.
    // Else: set AudioSource volume to volume.
    private void SetVolume(float volume)
    {
        if (_mute)
        {
            // Capture target volume into _muteVolume; actually set audio to 0.
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _muteVolume = volume;
            _audio.volume = 0f;
        }
        else
        {
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.volume = volume;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/SetMute.c  RVA 0x017BF238
    // No-op if state unchanged.  unmute: restore _muteVolume to AudioSource.
    // mute: capture current AudioSource.volume into _muteVolume, then 0.
    public void SetMute(bool mute)
    {
        if (_mute == mute)
        {
            return;
        }
        _mute = mute;
        if (!mute)
        {
            // unmute: restore
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.volume = _muteVolume;
        }
        else
        {
            // mute: snapshot then zero
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _muteVolume = _audio.volume;
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.volume = 0f;
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/ClearCache.c  RVA 0x017BF29C
    // Removes from _CachedAudio every key that is NOT the name of the current
    // AudioSource.clip AND NOT the name of _nextClip.  Logs completion message.
    public void ClearCache()
    {
        if (_CachedAudio == null)
        {
            throw new System.NullReferenceException();
        }
        Dictionary<string, AudioClipCache>.KeyCollection keys = _CachedAudio.Keys;
        if (keys == null)
        {
            throw new System.NullReferenceException();
        }
        int count = keys.Count;
        string[] arr = new string[count];
        keys.CopyTo(arr, 0);
        for (int i = 0; i < arr.Length; i++)
        {
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            AudioClip clip = _audio.clip;
            // remove = (clip == null) || (arr[i] != clip.name)
            bool remove;
            if (clip == null)
            {
                remove = true;
            }
            else
            {
                if (_audio == null)
                {
                    throw new System.NullReferenceException();
                }
                AudioClip clip2 = _audio.clip;
                if (clip2 == null)
                {
                    throw new System.NullReferenceException();
                }
                remove = arr[i] != clip2.name;
            }
            // AND with: (_nextClip == null) || (arr[i] != _nextClip.name)
            if (_nextClip != null)
            {
                if (_nextClip == null)
                {
                    throw new System.NullReferenceException();
                }
                bool notNext = arr[i] != _nextClip.name;
                remove = remove & notNext;
            }
            if (remove)
            {
                if (_CachedAudio == null)
                {
                    throw new System.NullReferenceException();
                }
                _CachedAudio.Remove(arr[i]);
            }
        }
        UJDebug.Log("MusicProxy Clear Audio Cache Finish");
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/.ctor.c  RVA 0x017BF530
    // Instance defaults: _volume=1, _muteVolume=1 (offset 0x3C), _fadeIntervalIn=DAT_008e3490(0)
    //                    _fadeIntervalOut=0 (combined 8-byte 0 store at 0x4C covers 0x4C+0x50),
    //                    _nextVolume=1, _volumeMask = s_volumeMask - 1.
    // DAT_008e3490 is read as 8 bytes at 0x4C; observed = 0 (default static).
    public MusicProxy()
    {
        _volume = 1f;            // 0x3F800000 at offset 0x30
        _muteVolume = 1f;        // 0x3F800000 at offset 0x3C
        // *(undefined8*)(this+0x4C) = DAT_008e3490 -> zero _fadeIntervalIn & _fadeIntervalOut.
        _fadeIntervalIn = 0f;
        _fadeIntervalOut = 0f;
        _nextVolume = 1f;        // 0x3F800000 at offset 0x68
        _volumeMask = s_volumeMask - 1u;
        // base() (MonoBehaviour..ctor) is implicit in C#.
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/MusicProxy/.cctor.c  RVA 0x017BF5B8
    // Static defaults: s_loaded=0, s_volumeMask=0 (low 4 bytes of 0x3F00000000000000),
    //                  s_masterVolume = 0.5f (high 4 bytes of 0x3F00000000000000 = 0x3F000000),
    //                  s_main = null, op = null.
    // Ghidra: *(undefined8 *)(puVar2 + 4) = 0x3f00000000000000;
    //   low4 bytes at +4 (= s_volumeMask) = 0; high4 bytes at +8 (= s_masterVolume) = 0x3F000000 = 0.5f.
    static MusicProxy()
    {
        s_loaded = false;
        s_volumeMask = 0u;
        s_masterVolume = 0.5f;
        s_main = null;
        op = null;
    }
}
