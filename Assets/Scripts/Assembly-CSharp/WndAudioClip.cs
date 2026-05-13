// Source: Ghidra work/06_ghidra/decompiled_full/WndAudioClip/ (7 .c files all ported 1-1)
// Source: dump.cs TypeDefIndex 178 — `WndAudioClip : MonoBehaviour, IPointerDownHandler`
// All 7 methods + ctor/cctor ported (Start has opaque PIC call — empty body documented).

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class WndAudioClip : MonoBehaviour, IPointerDownHandler
{
    private static Dictionary<string, AudioClip> loadedAudioDic; // static 0x0
    private static Dictionary<string, int> audioUseTimesDic;     // static 0x8

    private AudioClip _audioClip;  // 0x20
    public string _audioName;       // 0x28
    public float _volume;           // 0x30
    public float _pitch;            // 0x34
    public bool _pressEvent;        // 0x38
    public bool _EnableEvent;       // 0x39
    public float _delayTime;        // 0x3C
    private float _waitPlayTime;    // 0x40

    // Source: Ghidra Update.c  RVA 0x17C8D1C
    // If _waitPlayTime > 0: decrement by deltaTime; when reaches 0, fire PlaySound().
    private void Update()
    {
        float wait = _waitPlayTime;
        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
            _waitPlayTime = wait;
            if (wait <= 0.0f)
            {
                _waitPlayTime = 0.0f;
                PlaySound();
            }
        }
    }

    // Source: Ghidra Start.c  RVA 0x17C8FE4
    // Single PIC call (FUN_032a5e38) — opaque external function reference.
    // Empty body so MonoBehaviour Start doesn't break.
    private void Start()
    {
        // TODO body RVA 0x017C8FE4 — opaque PIC call
    }

    // Source: Ghidra OnEnable.c  RVA 0x17C96EC
    // If _EnableEvent: if _delayTime > 0 → schedule wait, else PlaySound() immediately.
    public void OnEnable()
    {
        if (_EnableEvent)
        {
            if (_delayTime <= 0.0f)
            {
                PlaySound();
                return;
            }
            _waitPlayTime = _delayTime;
        }
    }

    // Source: Ghidra OnDestroy.c  RVA 0x17C8E08
    // Decrement audioUseTimesDic[_audioName] count; if reaches 0, remove from both dicts.
    private void OnDestroy()
    {
        if (_audioClip == null) return;
        if (audioUseTimesDic == null) return;
        if (!audioUseTimesDic.ContainsKey(_audioName)) return;
        int count = audioUseTimesDic[_audioName];
        audioUseTimesDic[_audioName] = count - 1;
        int newCount = audioUseTimesDic[_audioName];
        if (newCount > 0) return;
        audioUseTimesDic.Remove(_audioName);
        if (loadedAudioDic != null)
        {
            loadedAudioDic.Remove(_audioName);
        }
    }

    // Source: Ghidra PlaySound.c  RVA 0x17C9550 — explicit (volume, pitch) variant
    // Body: SoundProxy.main.Play(_audioClip, volume, pitch)
    public void PlaySound(float volume, float pitch)
    {
        SoundProxy sp = SoundProxy.main;
        if (sp == null) throw new System.NullReferenceException();
        sp.Play(_audioClip, volume, pitch);
    }

    // Source: Ghidra (no separate .c for overloads — patterns from dump.cs)
    // RVA: 0x17C8D6C — no-arg PlaySound(): uses field _volume, _pitch
    public void PlaySound()
    {
        PlaySound(_volume, _pitch);
    }

    // RVA: 0x17C94A4 — single-arg PlaySound(volume): uses field _pitch
    public void PlaySound(float volume)
    {
        PlaySound(volume, _pitch);
    }

    // Source: Ghidra OnPointerDown.c  RVA 0x17C9600
    // If _pressEvent && _audioClip != null: SoundProxy.main.Play(_audioClip, _volume, _pitch)
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_pressEvent) return;
        if (_audioClip == null) return;
        SoundProxy sp = SoundProxy.main;
        if (sp == null) throw new System.NullReferenceException();
        sp.Play(_audioClip, _volume, _pitch);
    }

    // Source: Ghidra Play.c  RVA 0x17C970C
    // If _delayTime > 0: set _waitPlayTime = _delayTime; else PlaySound() immediately.
    private void Play()
    {
        if (_delayTime > 0.0f)
        {
            _waitPlayTime = _delayTime;
            return;
        }
        PlaySound();
    }

    // RVA: 0x17C9724 — default ctor.
    public WndAudioClip() { }

    // RVA: 0x17C9784 — .cctor (static fields).
    static WndAudioClip()
    {
        loadedAudioDic = new Dictionary<string, AudioClip>();
        audioUseTimesDic = new Dictionary<string, int>();
    }
}
