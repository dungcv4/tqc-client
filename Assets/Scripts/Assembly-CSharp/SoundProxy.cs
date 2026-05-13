// RE-RESTORED 2026-05-11 từ v1 source (unity_project/Assets/Scripts/Generated/_Skeletons/SoundProxy.cs)
// Reason: Cpp2IL property-form mismatched với tolua# Wrap method-form.
// v1 has explicit `get_X()/set_X(v)` matching Wrap calls.
//
// 2026-05-12 — Full 1-1 port of 29 NIE method bodies from Ghidra pseudo-C.
// Source: work/06_ghidra/decompiled_full/SoundProxy/*.c
// Source: work/03_il2cpp_dump/dump.cs line 7521 (TypeDefIndex 152)
// String literal indices resolved via work/03_il2cpp_dump/script.json ScriptString[idx]:
//   1586  -> "3dSound"        (Transform.Find target for _audio3D)
//   18397 -> "loopRunSound"   (Transform.Find target for _loopAudio)
//   16339 -> "dubbingSound"   (Transform.Find target for _mainDubbingAudio)
//   10351 -> "SoundVolume"    (PlayerPrefs key for masterVolume)
//   4859  -> "DubbingVolume"  (PlayerPrefs key for dubbingVolume)
//   3212  -> "Audio/Sounds/"  (Resources.Load prefix)
//   10349 -> "SoundBundleOP is null.."
//   10350 -> "SoundBundleOP load is fail...Sound Path : "
// Storage offsets (verified via Ghidra param_1 + N):
//   _isMain=0x20  _audio=0x28  _loopAudio=0x30  _audio3D=0x38  _cacheTrans=0x40
//   _freeSource3Ds=0x48  _activeSource3Ds=0x50  _maxActiveSource=0x58
//   _mainDubbingAudio=0x60  _freeDubbingAudios=0x68  _activeDubbingAudios=0x70
//   _maxActiveDubbingSource=0x78  _pauseUnloadTimer=0x7C
// Static field offsets (in il2cpp class statics block):
//   s_loaded=+0  s_masterVolume=+4  s_main=+8  s_disable=+0x10  s_dubbingVolume=+0x14
//   _soundAudioList=+0x18  _loopSoundList=+0x20

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

// Source: Il2CppDumper-stub  TypeDefIndex: 152
[AddComponentMenu("UJ RD1/Sound Proxy")]
public sealed class SoundProxy : MonoBehaviour, IGameAudioMgr
{
    private const string CPrefKey_SoundVolume = "SoundVolume";
    private const string CPrefKey_DubbingVolume = "DubbingVolume";
    public const float CMinVolume = 0f;
    public const float CMaxVoulume = 1f;
    private static bool s_loaded;
    private static float s_masterVolume = 0.7f; // 0x3f333333 (.cctor)
    private static SoundProxy s_main;
    private static bool s_disable;
    private static float s_dubbingVolume = 0.7f; // 0x3f333333 (.cctor)
    private static Dictionary<string, AudioClip> _soundAudioList;
    private static Dictionary<string, SoundProxy.ActiveInfo> _loopSoundList;
    public bool _isMain;
    private AudioSource _audio;
    private AudioSource _loopAudio;
    private AudioSource _audio3D;
    private Transform _cacheTrans;
    private Queue<AudioSource> _freeSource3Ds;
    private Queue<SoundProxy.ActiveInfo> _activeSource3Ds;
    private int _maxActiveSource;
    private AudioSource _mainDubbingAudio;
    private Queue<AudioSource> _freeDubbingAudios;
    private Queue<SoundProxy.ActiveInfo> _activeDubbingAudios;
    private int _maxActiveDubbingSource;
    private float _pauseUnloadTimer;

    // Source: dump.cs declares BOTH property + explicit get_main() (separate IL2Cpp entries).
    // C# can only express the property; underlying auto-gen `get_main()` matches dump's RVA 0x017c02d0.
    // Lua needs property form (`SoundProxy.main.masterVolume` in AudioManager:115) → RegVar.
    public static SoundProxy main { get { return s_main; } }

    // dump.cs `public static bool Disable { get; set; }` (+ explicit get/set at RVA 0x017c0328/0x017c0380).
    // C# expresses as property; auto-gen accessors match dump's RVAs semantically.
    public static bool Disable
    {
        get { return s_disable; }
        set { s_disable = value; }
    }

    // dump.cs `public static float masterVolume { get; set; }` (+ explicit RVAs 0x017c03e0 / 0x017c0438).
    // Property form so Lua `SoundProxy.main.masterVolume = X` (AudioManager:115) works via RegVar.
    public static float masterVolume
    {
        get { return s_masterVolume; }
        // Ghidra 1-1: clamp value to [0,1] only when current was non-negative-and-<=1; write-through to PlayerPrefs.
        set
        {
            float current = s_masterVolume;
            float v = (0f <= current) ? value : 0f;
            current = s_masterVolume;
            float clamped = (current <= 1f) ? v : 1f;
            if (current == clamped) return;
            s_masterVolume = clamped;
            UnityEngine.PlayerPrefs.SetFloat(CPrefKey_SoundVolume, clamped);
        }
    }

    // dump.cs `public static float dubbingVolume { get; set; }` (+ explicit RVAs 0x017c0544 / 0x017c059c).
    // get body = Ghidra get_dubbingVolume.c (return s_dubbingVolume).
    // set body = Ghidra set_dubbingVolume.c — same branch structure as masterVolume, key=CPrefKey_DubbingVolume.
    public static float dubbingVolume
    {
        get { return s_dubbingVolume; }
        set
        {
            float current = s_dubbingVolume;
            float v = (0f <= current) ? value : 0f;
            current = s_dubbingVolume;
            float clamped = (current <= 1f) ? v : 1f;
            if (current == clamped) return;
            s_dubbingVolume = clamped;
            UnityEngine.PlayerPrefs.SetFloat(CPrefKey_DubbingVolume, clamped);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/Save.c RVA 0x017c06a8
    // Sets s_loaded=true, s_masterVolume=newVolume, PlayerPrefs.SetFloat("SoundVolume", newVolume).
    public static void Save(float newVolume)
    {
        s_loaded = true;
        s_masterVolume = newVolume;
        UnityEngine.PlayerPrefs.SetFloat(CPrefKey_SoundVolume, newVolume);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/get_audio3D.c RVA 0x017c0734
    // Single load: return *(this+0x38).
    public AudioSource get_audio3D()
    {
        return _audio3D;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/get_isPlaying.c RVA 0x017c073c
    // if (_audio != null) return _audio.isPlaying; else NRE.
    public bool get_isPlaying()
    {
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        return _audio.isPlaying;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/Start.c RVA 0x017c0758
    // 1-1 from Ghidra:
    //   _cacheTrans = transform;
    //   GameObject go = gameObject;
    //   DontDestroyOnLoad(go);
    //   if (_isMain) s_main = this;
    //   _audio = GetComponent<AudioSource>();
    //   if (Object.op_Equality(_audio, null)) {
    //       go = gameObject; if (go==null) NRE;
    //       _audio = go.AddComponent<AudioSource>();
    //       if (_audio==null) NRE;
    //       _audio.loop = false;
    //   }
    //   var t1 = transform.Find("3dSound");           // index 1586
    //   if (t1 != null) {
    //       _audio3D = t1.GetComponent<AudioSource>();
    //       var t2 = transform.Find("loopRunSound");   // index 18397
    //       if (t2 != null) {
    //           _loopAudio = t2.GetComponent<AudioSource>();
    //           if (_loopAudio != null) {
    //               _loopAudio.loop = true;
    //               var t3 = transform.Find("dubbingSound");   // index 16339
    //               if (t3 != null) {
    //                   _mainDubbingAudio = t3.GetComponent<AudioSource>();
    //                   float v = PlayerPrefs.GetFloat(s_masterVolume, "SoundVolume");
    //                   s_masterVolume = v;
    //                   if (v < 0) s_masterVolume = 0;
    //                   if (s_masterVolume > 1) s_masterVolume = 1;
    //                   float dv = PlayerPrefs.GetFloat(s_dubbingVolume, "DubbingVolume");
    //                   s_dubbingVolume = dv;
    //                   if (dv < 0) s_dubbingVolume = 0;
    //                   if (s_dubbingVolume > 1) s_dubbingVolume = 1;
    //                   return;
    //               }
    //           }
    //       }
    //   }
    //   NRE (LAB_018c0b44 -> FUN_015cb8fc)
    // Note: Ghidra's PlayerPrefs.GetFloat signature is (value, key, _) — Unity binding has
    //  static GetFloat(string key, float defaultValue). We translate to GetFloat(key, current).
    private void Start()
    {
        _cacheTrans = transform;
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        if (_isMain)
        {
            s_main = this;
        }
        _audio = GetComponent<UnityEngine.AudioSource>();
        if (_audio == null)
        {
            UnityEngine.GameObject go = gameObject;
            if (go == null)
            {
                throw new System.NullReferenceException();
            }
            _audio = go.AddComponent<UnityEngine.AudioSource>();
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.loop = false;
        }
        Transform t1 = transform.Find("3dSound");
        if (t1 != null)
        {
            _audio3D = t1.GetComponent<UnityEngine.AudioSource>();
            Transform t2 = transform.Find("loopRunSound");
            if (t2 != null)
            {
                _loopAudio = t2.GetComponent<UnityEngine.AudioSource>();
                if (_loopAudio != null)
                {
                    _loopAudio.loop = true;
                    Transform t3 = transform.Find("dubbingSound");
                    if (t3 != null)
                    {
                        _mainDubbingAudio = t3.GetComponent<UnityEngine.AudioSource>();
                        float mv = UnityEngine.PlayerPrefs.GetFloat(CPrefKey_SoundVolume, s_masterVolume);
                        s_masterVolume = mv;
                        if (mv < 0f) s_masterVolume = 0f;
                        if (s_masterVolume > 1f) s_masterVolume = 1f;
                        float dv = UnityEngine.PlayerPrefs.GetFloat(CPrefKey_DubbingVolume, s_dubbingVolume);
                        s_dubbingVolume = dv;
                        if (dv < 0f) s_dubbingVolume = 0f;
                        if (s_dubbingVolume > 1f) s_dubbingVolume = 1f;
                        return;
                    }
                }
            }
        }
        throw new System.NullReferenceException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/Update.c RVA 0x017c0b48
    // 1-1 from Ghidra:
    //   if (_activeSource3Ds != null) {
    //     int count = _activeSource3Ds.Count;
    //     if (count >= 1) {
    //       if (count > 99) count = 100;
    //       int loop = count + 1;
    //       do {
    //         var info = _activeSource3Ds.Dequeue();
    //         float now = Time.realtimeSinceStartup;
    //         if (now <= info._duedate) {
    //             if (_activeSource3Ds == null) break;
    //             _activeSource3Ds.Enqueue(info);
    //         } else {
    //             if (info._source == null) break;
    //             var go = info._source.gameObject;
    //             if (go == null) break;
    //             go.SetActive(false);
    //             if (_freeSource3Ds == null) break;
    //             _freeSource3Ds.Enqueue(info._source);
    //         }
    //         loop--;
    //         if (loop < 2) goto TimerSection;
    //       } while (_activeSource3Ds != null);
    //       NRE (LAB_018c1e7c equivalent)
    //     }
    //   }
    //   TimerSection:
    //     if (_pauseUnloadTimer > 0) {
    //       _pauseUnloadTimer -= RealTime.deltaTime;
    //       if (_pauseUnloadTimer <= 0) ResourceUnloader.s_whenIdle = false;  // byte at +5
    //     }
    private void Update()
    {
        bool reachedTimerSection = false;
        if (_activeSource3Ds != null)
        {
            int count = _activeSource3Ds.Count;
            if (count >= 1)
            {
                if (count > 99) count = 100;
                int loop = count + 1;
                bool nullExit = false;
                while (true)
                {
                    SoundProxy.ActiveInfo info = _activeSource3Ds.Dequeue();
                    float now = UnityEngine.Time.realtimeSinceStartup;
                    if (now <= info._duedate)
                    {
                        if (_activeSource3Ds == null) { nullExit = true; break; }
                        _activeSource3Ds.Enqueue(info);
                    }
                    else
                    {
                        if (info._source == null) { nullExit = true; break; }
                        UnityEngine.GameObject go = info._source.gameObject;
                        if (go == null) { nullExit = true; break; }
                        go.SetActive(false);
                        if (_freeSource3Ds == null) { nullExit = true; break; }
                        _freeSource3Ds.Enqueue(info._source);
                    }
                    loop--;
                    if (loop < 2)
                    {
                        reachedTimerSection = true;
                        break;
                    }
                    if (_activeSource3Ds == null) { nullExit = true; break; }
                }
                if (nullExit)
                {
                    throw new System.NullReferenceException();
                }
            }
            else
            {
                reachedTimerSection = true;
            }
        }
        else
        {
            reachedTimerSection = true; // Ghidra: outer if (_activeSource3Ds != 0) - fallthrough goes to timer
        }
        if (!reachedTimerSection)
        {
            return;
        }
        if (_pauseUnloadTimer > 0f)
        {
            float delta = RealTime.get_deltaTime();
            _pauseUnloadTimer = _pauseUnloadTimer - delta;
            if (_pauseUnloadTimer <= 0f)
            {
                ResourceUnloader.idle = false;
            }
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/AudioSourcePlay.c RVA 0x017c0cdc
    // 1-1:
    //   if (_audio == null) NRE;
    //   _audio.clip = clip;
    //   _audio.volume = s_masterVolume;
    //   _audio.Play();
    //   PauseUnload(clip);
    public void AudioSourcePlay(AudioClip clip)
    {
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        _audio.clip = clip;
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        _audio.volume = s_masterVolume;
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        _audio.Play();
        PauseUnload(clip);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/AudioSourceStop.c RVA 0x017c0e34
    // 1-1: if (_audio==null) NRE; _audio.Stop(); _audio.clip = null;
    public void AudioSourceStop()
    {
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        _audio.Stop();
        if (_audio == null)
        {
            throw new System.NullReferenceException();
        }
        _audio.clip = null;
    }

    // Source: dump.cs RVA 0x17C0E68 — single-arg overload delegates to (clip,1,1).
    // Ghidra .c shows shared body at RVA 0x17C0E74 (the 3-arg overload).
    public bool Play(AudioClip clip)
    {
        return Play(clip, 1f, 1f);
    }

    // Source: dump.cs RVA 0x17C0F9C — two-arg overload delegates to (clip,volume,1).
    public bool Play(AudioClip clip, float volume)
    {
        return Play(clip, volume, 1f);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/Play.c RVA 0x017c0e74
    // 1-1:
    //   if (Object.op_Equality(clip, null)) return false;
    //   float v = s_masterVolume * volume;
    //   if (v > 0) {
    //       if (_audio == null) NRE;
    //       _audio.pitch = pitch;
    //       if (_audio == null) NRE;
    //       _audio.PlayOneShot(clip, v);
    //   }
    //   return true;
    public bool Play(AudioClip clip, float volume, float pitch)
    {
        if (clip == null)
        {
            return false;
        }
        float v = s_masterVolume * volume;
        if (0f < v)
        {
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.pitch = pitch;
            if (_audio == null)
            {
                throw new System.NullReferenceException();
            }
            _audio.PlayOneShot(clip, v);
        }
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/PlayLoop.c RVA 0x017c0fa4
    // 1-1:
    //   if (Object.op_Equality(clip, null)) return false;
    //   float v = s_masterVolume * volume;
    //   if (v > 0) {
    //       var dict = _loopSoundList;
    //       if (dict == null) NRE;
    //       if (dict.ContainsKey(name)) {
    //           var dict2 = _loopSoundList; if (dict2==null) NRE;
    //           var existing = dict2[name]._source;  // ActiveInfo._source via get_Item
    //           if (existing == null) NRE;
    //           existing.volume = v;
    //           var dict3 = _loopSoundList; if (dict3==null) NRE;
    //           existing = dict3[name]._source;
    //           if (existing == null) NRE;
    //           var ego = existing.gameObject;
    //           if (ego == null) NRE;
    //           ego.SetActive(true);
    //           return true;
    //       }
    //       // not found: instantiate new from _loopAudio (param_2+0x30)
    //       AudioSource newAS = Object.Instantiate<AudioSource>(_loopAudio);
    //       if (newAS==null) NRE;
    //       newAS.clip = clip;
    //       newAS.volume = v;
    //       var ngo = newAS.gameObject; if (ngo==null) NRE;
    //       ngo.SetActive(true);
    //       var nt = newAS.transform; if (nt==null) NRE;
    //       nt.parent = _cacheTrans;
    //       nt.localScale = Vector3.one;       // PTR_DAT_03446bc8 = Vector3.one
    //       nt.localRotation = Quaternion.identity; // PTR_DAT_03446b08 = Quaternion.identity
    //       var dict4 = _loopSoundList; if (dict4==null) NRE;
    //       dict4.Add(name, new ActiveInfo { _duedate=0, _source=newAS });
    //   }
    //   return true;
    private bool PlayLoop(string name, AudioClip clip, float volume, float pitch)
    {
        if (clip == null)
        {
            return false;
        }
        float v = s_masterVolume * volume;
        if (0f < v)
        {
            if (_loopSoundList == null)
            {
                throw new System.NullReferenceException();
            }
            if (_loopSoundList.ContainsKey(name))
            {
                if (_loopSoundList == null)
                {
                    throw new System.NullReferenceException();
                }
                UnityEngine.AudioSource existing = _loopSoundList[name]._source;
                if (existing == null)
                {
                    throw new System.NullReferenceException();
                }
                existing.volume = v;
                if (_loopSoundList == null)
                {
                    throw new System.NullReferenceException();
                }
                existing = _loopSoundList[name]._source;
                if (existing == null)
                {
                    throw new System.NullReferenceException();
                }
                UnityEngine.GameObject ego = existing.gameObject;
                if (ego == null)
                {
                    throw new System.NullReferenceException();
                }
                ego.SetActive(true);
                return true;
            }
            UnityEngine.AudioSource newAS = UnityEngine.Object.Instantiate<UnityEngine.AudioSource>(_loopAudio);
            if (newAS == null)
            {
                throw new System.NullReferenceException();
            }
            newAS.clip = clip;
            if (newAS == null)
            {
                throw new System.NullReferenceException();
            }
            newAS.volume = v;
            if (newAS == null)
            {
                throw new System.NullReferenceException();
            }
            UnityEngine.GameObject ngo = newAS.gameObject;
            if (ngo == null)
            {
                throw new System.NullReferenceException();
            }
            ngo.SetActive(true);
            if (newAS == null)
            {
                throw new System.NullReferenceException();
            }
            UnityEngine.Transform nt = newAS.transform;
            if (nt == null)
            {
                throw new System.NullReferenceException();
            }
            nt.parent = _cacheTrans;
            nt.localScale = UnityEngine.Vector3.one;
            nt.localRotation = UnityEngine.Quaternion.identity;
            if (_loopSoundList == null)
            {
                throw new System.NullReferenceException();
            }
            SoundProxy.ActiveInfo info = new SoundProxy.ActiveInfo();
            info._duedate = 0f;
            info._source = newAS;
            _loopSoundList.Add(name, info);
        }
        return true;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/PlaySoundForLoop.c RVA 0x017c1314
    // 1-1:
    //   if (String.IsNullOrEmpty(name)) return;
    //   AudioClip clip = loadAudioClip(name, fullPath);
    //   if (Object.op_Inequality(clip, null)) PlayLoop(name, clip, volume, pitch);
    // Note: Ghidra signature shows (this, name, volume, pitch, fullPath). The param ordering in
    //  the decompile shows (param_1=this, param_2=volume, param_3=pitch, param_4=name? no -)
    //  Actually Ghidra decompile here is somewhat condensed — the call to loadAudioClip uses
    //  (uVar2, param_3) suggesting (this, name) but param mapping is obscure. We follow dump.cs
    //  signature: (string name, float volume = 1, float pitch = 1, bool fullPath = false).
    public void PlaySoundForLoop(string name, float volume = 1f, float pitch = 1f, bool fullPath = false)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }
        UnityEngine.AudioClip clip = loadAudioClip(name, fullPath);
        if (clip != null)
        {
            PlayLoop(name, clip, volume, pitch);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/StopSoundForLoop.c RVA 0x017c175c
    // 1-1:
    //   if (_loopSoundList == null) NRE;
    //   if (!_loopSoundList.ContainsKey(name)) return;
    //   if (_loopSoundList == null) NRE;
    //   var src = _loopSoundList[name]._source;
    //   if (src == null) NRE;
    //   var go = src.gameObject;
    //   if (go == null) NRE;
    //   go.SetActive(false);
    public void StopSoundForLoop(string name)
    {
        if (_loopSoundList == null)
        {
            throw new System.NullReferenceException();
        }
        if (!_loopSoundList.ContainsKey(name))
        {
            return;
        }
        if (_loopSoundList == null)
        {
            throw new System.NullReferenceException();
        }
        UnityEngine.AudioSource src = _loopSoundList[name]._source;
        if (src == null)
        {
            throw new System.NullReferenceException();
        }
        UnityEngine.GameObject go = src.gameObject;
        if (go == null)
        {
            throw new System.NullReferenceException();
        }
        go.SetActive(false);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/PlaySoundByName.c RVA 0x017c184c
    // 1-1:
    //   if (!s_disable && !String.IsNullOrEmpty(name)) {
    //       var clip = loadAudioClip(name, fullPath);
    //       if (Object.op_Inequality(clip, null)) Play(clip, volume, pitch);
    //   }
    public void PlaySoundByName(string name, float volume = 1f, float pitch = 1f, bool fullPath = false)
    {
        if (s_disable) return;
        if (string.IsNullOrEmpty(name)) return;
        UnityEngine.AudioClip clip = loadAudioClip(name, fullPath);
        if (clip != null)
        {
            Play(clip, volume, pitch);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/Play3DSoundByName.c RVA 0x017c1948
    // Identical pattern to PlaySoundByName but calls Play3D.
    public void Play3DSoundByName(string name, Vector3 pos, float volume = 1f, float pitch = 1f, bool fullPath = false)
    {
        if (s_disable) return;
        if (string.IsNullOrEmpty(name)) return;
        UnityEngine.AudioClip clip = loadAudioClip(name, fullPath);
        if (clip != null)
        {
            Play3D(clip, pos, volume, pitch);
        }
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/loadAudioClip.c RVA 0x017c13cc
    // 1-1:
    //   if (_soundAudioList == null) NRE;
    //   if (_soundAudioList.ContainsKey(name)) {
    //       if (_soundAudioList == null) NRE;
    //       return _soundAudioList[name];
    //   }
    //   // Try Resources first
    //   AudioClip ac = Resources.Load("Audio/Sounds/" + name) as AudioClip;
    //   if (Object.op_Inequality(ac, null)) goto Add;
    //   // Resources failed - try AssetBundle
    //   if (ResMgr.Instance == null) NRE;
    //   if (ResMgr.Instance.SoundBundleOP == null) {
    //       UJDebug.LogError("SoundBundleOP is null..");        // index 10349
    //   } else {
    //       var bundle = ResMgr.Instance.SoundBundleOP;
    //       Type t = typeof(AudioClip);
    //       ac = bundle.Load(name, t) as AudioClip;
    //       if (Object.op_Inequality(ac, null)) goto Add;
    //       UJDebug.LogError("SoundBundleOP load is fail...Sound Path : " + name);   // index 10350
    //   }
    //   // Fallthrough: don't cache, return whatever ac is (null per above)
    //   return ac;   // (Ghidra returns at the bottom outside both Resources / Bundle branches when both null)
    //   Add:
    //   if (_soundAudioList == null) NRE;
    //   _soundAudioList.Add(name, ac);
    //   return ac;
    // Note on parameter mapping: Ghidra signature shows (param_1=this, param_2=name, param_3=fullPath_or_method).
    //  dump.cs signature is (string name, bool fullPath = false). We honor dump.cs.
    private AudioClip loadAudioClip(string name, bool fullPath = false)
    {
        if (_soundAudioList == null)
        {
            throw new System.NullReferenceException();
        }
        if (_soundAudioList.ContainsKey(name))
        {
            if (_soundAudioList == null)
            {
                throw new System.NullReferenceException();
            }
            return _soundAudioList[name];
        }
        UnityEngine.AudioClip ac = UnityEngine.Resources.Load("Audio/Sounds/" + name) as UnityEngine.AudioClip;
        bool gotIt = (ac != null);
        if (!gotIt)
        {
            if (ResMgr.Instance == null)
            {
                throw new System.NullReferenceException();
            }
            if (ResMgr.Instance.SoundBundleOP == null)
            {
                UJDebug.LogError("SoundBundleOP is null..");
            }
            else
            {
                if (ResMgr.Instance == null)
                {
                    throw new System.NullReferenceException();
                }
                AssetBundleOP bundle = ResMgr.Instance.SoundBundleOP;
                if (bundle == null)
                {
                    throw new System.NullReferenceException();
                }
                System.Type t = typeof(UnityEngine.AudioClip);
                ac = bundle.Load(name, t) as UnityEngine.AudioClip;
                gotIt = (ac != null);
                if (!gotIt)
                {
                    UJDebug.LogError("SoundBundleOP load is fail...Sound Path : " + name);
                }
            }
        }
        if (gotIt)
        {
            if (_soundAudioList == null)
            {
                throw new System.NullReferenceException();
            }
            _soundAudioList.Add(name, ac);
        }
        return ac;
    }

    // Source: dump.cs RVA 0x17C1E80 — 2-arg overload delegates to (clip,pos,1,1).
    public void Play3D(AudioClip clip, Vector3 pos)
    {
        Play3D(clip, pos, 1f, 1f);
    }

    // Source: dump.cs RVA 0x17C1E8C — 3-arg overload delegates to (clip,pos,volume,1).
    public void Play3D(AudioClip clip, Vector3 pos, float volume)
    {
        Play3D(clip, pos, volume, 1f);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/Play3D.c RVA 0x017c1a74
    // 1-1:
    //   AudioSource template = _audio3D;
    //   float baseVol = s_masterVolume;
    //   if (Object.op_Equality(template, null)) return;
    //   float v = baseVol * volume;
    //   if (Object.op_Equality(clip, null)) return;
    //   if (v <= 0) return;
    //   AudioSource newAS = null;
    //   if (_freeSource3Ds == null) NRE;
    //   if (_freeSource3Ds.Count < 1) {
    //       if (_activeSource3Ds == null) NRE;
    //       if (_maxActiveSource <= _activeSource3Ds.Count) return;
    //       newAS = Object.Instantiate<AudioSource>(_audio3D);
    //       if (newAS==null) NRE;
    //       var nt = newAS.transform; if (nt==null) NRE;
    //       nt.parent = _cacheTrans;
    //       nt.localPosition = pos;
    //       nt.localScale = Vector3.one;
    //       nt.localRotation = Quaternion.identity;
    //   } else {
    //       newAS = _freeSource3Ds.Dequeue();
    //       if (newAS==null) NRE;
    //       var nt = newAS.transform; if (nt==null) NRE;
    //       nt.localPosition = pos;
    //   }
    //   float scale = Time.timeScale;
    //   float now = Time.realtimeSinceStartup;
    //   float clipLen;
    //   float finalPitch;
    //   if (scale == 0) {
    //       finalPitch = pitch;
    //       if (clip == null) NRE;
    //       clipLen = clip.length;
    //   } else {
    //       finalPitch = scale * pitch;
    //       if (clip == null) NRE;
    //       clipLen = clip.length / Math.Abs(Time.timeScale);
    //   }
    //   if (newAS==null) NRE;
    //   newAS.pitch = finalPitch;
    //   float duedate = now + clipLen + DAT_0091c120;   // small epsilon constant
    //   if (newAS==null) NRE;
    //   newAS.clip = clip;
    //   newAS.volume = v;
    //   var ngo = newAS.gameObject;
    //   if (ngo==null) NRE;
    //   ngo.SetActive(true);
    //   if (_activeSource3Ds==null) NRE;
    //   _activeSource3Ds.Enqueue(new ActiveInfo { _duedate=duedate, _source=newAS });
    // Note: DAT_0091c120 is read-only float at libil2cpp.so offset; without exact extraction
    // we use 0.05f as a safe placeholder buffer (epsilon to outlast clip). Documented deviation.
    public void Play3D(AudioClip clip, Vector3 pos, float volume, float pitch)
    {
        if (_audio3D == null)
        {
            return;
        }
        float v = s_masterVolume * volume;
        if (clip == null)
        {
            return;
        }
        if (v <= 0f)
        {
            return;
        }
        if (_freeSource3Ds == null)
        {
            throw new System.NullReferenceException();
        }
        UnityEngine.AudioSource newAS;
        if (_freeSource3Ds.Count < 1)
        {
            if (_activeSource3Ds == null)
            {
                throw new System.NullReferenceException();
            }
            if (_maxActiveSource <= _activeSource3Ds.Count)
            {
                return;
            }
            newAS = UnityEngine.Object.Instantiate<UnityEngine.AudioSource>(_audio3D);
            if (newAS == null)
            {
                throw new System.NullReferenceException();
            }
            UnityEngine.Transform nt = newAS.transform;
            if (nt == null)
            {
                throw new System.NullReferenceException();
            }
            nt.parent = _cacheTrans;
            nt.localPosition = pos;
            nt.localScale = UnityEngine.Vector3.one;
            nt.localRotation = UnityEngine.Quaternion.identity;
        }
        else
        {
            newAS = _freeSource3Ds.Dequeue();
            if (newAS == null)
            {
                throw new System.NullReferenceException();
            }
            UnityEngine.Transform nt = newAS.transform;
            if (nt == null)
            {
                throw new System.NullReferenceException();
            }
            nt.localPosition = pos;
        }
        float scale = UnityEngine.Time.timeScale;
        float now = UnityEngine.Time.realtimeSinceStartup;
        float clipLen;
        float finalPitch;
        if (scale == 0f)
        {
            finalPitch = pitch;
            if (clip == null)
            {
                throw new System.NullReferenceException();
            }
            clipLen = clip.length;
        }
        else
        {
            finalPitch = scale * pitch;
            if (clip == null)
            {
                throw new System.NullReferenceException();
            }
            clipLen = clip.length / System.Math.Abs(UnityEngine.Time.timeScale);
        }
        if (newAS == null)
        {
            throw new System.NullReferenceException();
        }
        newAS.pitch = finalPitch;
        // DAT_0091c120 epsilon constant (RVA in libil2cpp.so RDATA, not extracted) — using 0.05f.
        float duedate = now + clipLen + 0.05f;
        newAS.clip = clip;
        if (newAS == null)
        {
            throw new System.NullReferenceException();
        }
        newAS.volume = v;
        if (newAS == null)
        {
            throw new System.NullReferenceException();
        }
        UnityEngine.GameObject ngo = newAS.gameObject;
        if (ngo == null)
        {
            throw new System.NullReferenceException();
        }
        ngo.SetActive(true);
        if (_activeSource3Ds == null)
        {
            throw new System.NullReferenceException();
        }
        SoundProxy.ActiveInfo info = new SoundProxy.ActiveInfo();
        info._duedate = duedate;
        info._source = newAS;
        _activeSource3Ds.Enqueue(info);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/PlaySound.c RVA 0x017c1e94
    // 1-1:
    //   if (Object.op_Inequality(andioSource, null)) {
    //       if (andioSource == null) NRE;
    //       float origVol = andioSource.volume;
    //       andioSource.volume = origVol * s_masterVolume;
    //       float ts = Time.timeScale;
    //       if (ts != 0) {
    //           float origPitch = andioSource.pitch;
    //           float ts2 = Time.timeScale;
    //           andioSource.pitch = origPitch * ts2;
    //       }
    //       andioSource.Play();
    //   }
    public void PlaySound(AudioSource andioSource)
    {
        if (andioSource == null)
        {
            return;
        }
        float origVol = andioSource.volume;
        andioSource.volume = origVol * s_masterVolume;
        float ts = UnityEngine.Time.timeScale;
        if (ts != 0f)
        {
            float origPitch = andioSource.pitch;
            float ts2 = UnityEngine.Time.timeScale;
            andioSource.pitch = origPitch * ts2;
        }
        andioSource.Play();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/PauseUnload.c RVA 0x017c0db8
    // 1-1:
    //   ResourceUnloader.s_whenIdle = true;    // byte at +5 = 1
    //   if (clip == null) NRE;
    //   _pauseUnloadTimer = _pauseUnloadTimer + clip.length;
    private void PauseUnload(AudioClip clip)
    {
        ResourceUnloader.idle = true;
        if (clip == null)
        {
            throw new System.NullReferenceException();
        }
        _pauseUnloadTimer = _pauseUnloadTimer + clip.length;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/PlayDubbingSoundByName.c RVA 0x017c1fd8
    // The Ghidra body is an opaque tail-call FUN_032a5ad8(DAT_036837c9) with no recoverable args.
    // This is a stripped/obfuscated thunk in the binary. Keeping NIE + TODO until we can
    // hand-decompile via Ghidra GUI or extract from a related dubbing flow.
    public void PlayDubbingSoundByName(string name, float volume, bool stopPrev = true, float pitch = 1f, bool fullPath = false)
    {
        // TODO: Ghidra RVA 0x017c1fd8 — body is opaque tail-call FUN_032a5ad8(DAT_036837c9);
        // need Ghidra GUI re-decompile or cross-reference from AudioManager.lua callers.
        throw new System.NotImplementedException();
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/StopDubbingSound.c RVA 0x017c2498
    // 1-1:
    //   if (_mainDubbingAudio == null) NRE;
    //   _mainDubbingAudio.Stop();
    //   if (_mainDubbingAudio == null) NRE;
    //   _mainDubbingAudio.clip = null;
    //   if (_mainDubbingAudio == null) NRE;
    //   var go = _mainDubbingAudio.gameObject;
    //   if (go == null) NRE;
    //   go.SetActive(false);
    public void StopDubbingSound()
    {
        if (_mainDubbingAudio == null)
        {
            throw new System.NullReferenceException();
        }
        _mainDubbingAudio.Stop();
        if (_mainDubbingAudio == null)
        {
            throw new System.NullReferenceException();
        }
        _mainDubbingAudio.clip = null;
        if (_mainDubbingAudio == null)
        {
            throw new System.NullReferenceException();
        }
        UnityEngine.GameObject go = _mainDubbingAudio.gameObject;
        if (go == null)
        {
            throw new System.NullReferenceException();
        }
        go.SetActive(false);
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/.ctor.c RVA 0x017c24ec
    // 1-1: init 4 queues + 2 max counts; base MonoBehaviour ctor handles last.
    //   _freeSource3Ds        = new Queue<AudioSource>();   // +0x48
    //   _activeSource3Ds      = new Queue<ActiveInfo>();    // +0x50
    //   _maxActiveSource      = 10;                          // +0x58
    //   _freeDubbingAudios    = new Queue<AudioSource>();   // +0x68
    //   _activeDubbingAudios  = new Queue<ActiveInfo>();    // +0x70
    //   _maxActiveDubbingSource = 3;                         // +0x78
    public SoundProxy()
    {
        _freeSource3Ds = new Queue<UnityEngine.AudioSource>();
        _activeSource3Ds = new Queue<SoundProxy.ActiveInfo>();
        _maxActiveSource = 10;
        _freeDubbingAudios = new Queue<UnityEngine.AudioSource>();
        _activeDubbingAudios = new Queue<SoundProxy.ActiveInfo>();
        _maxActiveDubbingSource = 3;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/SoundProxy/.cctor.c RVA 0x017c2620
    // 1-1:
    //   s_loaded         = false;          // +0
    //   s_masterVolume   = 0.7f (0x3f333333); // +4
    //   s_main           = null;            // +8
    //   s_disable        = false;           // +0x10
    //   s_dubbingVolume  = 0.7f (0x3f333333); // +0x14
    //   _soundAudioList  = new Dictionary<string, AudioClip>(); // +0x18
    //   _loopSoundList   = new Dictionary<string, ActiveInfo>(); // +0x20
    // Note: s_masterVolume/s_dubbingVolume defaults set inline at field-decl above (0.7f);
    //  here we just instantiate the dictionaries (other statics auto-zero/false in C#).
    static SoundProxy()
    {
        _soundAudioList = new Dictionary<string, UnityEngine.AudioClip>();
        _loopSoundList = new Dictionary<string, SoundProxy.ActiveInfo>();
    }

    // Source: Il2CppDumper-stub  TypeDefIndex: 151
    private struct ActiveInfo
    {
        public float _duedate;
        public AudioSource _source;

    }
}
