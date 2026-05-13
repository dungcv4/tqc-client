// AUTO-GENERATED SKELETON — DO NOT HAND-EDIT
// Source: work/03_il2cpp_dump/dump.cs class 'PlayerCamControl'
// To port a method: replace `throw new System.NotImplementedException();`
// with body translated from the listed Ghidra .c file.
// Move ported file to unity_project/Assets/Scripts/Ported/<area>/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 679
public class PlayerCamControl : MonoBehaviour
{
    private Component cameraTarget;
    public float CAMZ_DIS;
    public static float CAM_HEIGHT;
    public static Quaternion CAM_QUATERNION;
    public static PlayerCamControl Instance;
    private float lastCameraX;
    private float lastCameraZ;
    private float _ShakeTimer;
    private int _ShakeNum;
    private Vector3 _ShakePos;
    private float _ShakeFrequency;
    private float _ShakeDecrease;
    private Vector3 _ShakeResult;
    private float _FOVTime;
    private float _FOVValue;
    private float _FOVFadeInTime;
    private float _FOVFadeOutTime;
    private float _FOVCurTime;
    private float _OldFOV;

    // Source: Ghidra Start.c  RVA 0x18CA98C
    // 1. PlayerCamControl.Instance = this.
    // 2. WndRoot.uiCamera.transform.position = Vector3.zero.
    // 3. WndRoot.uiCamera.transform.rotation = CAM_QUATERNION.
    // 4. WndRoot.uiCamera.transform.localScale = Vector3.one.
    // 5. lastCameraX = uiCamera.transform.position.x; lastCameraZ = .z.
    private void Start()
    {
        Instance = this;
        Camera cam = WndRoot.uiCamera;
        if (cam == null) throw new System.NullReferenceException();
        Transform t = cam.transform;
        if (t == null) throw new System.NullReferenceException();
        t.position = Vector3.zero;

        cam = WndRoot.uiCamera;
        if (cam == null) throw new System.NullReferenceException();
        t = cam.transform;
        if (t == null) throw new System.NullReferenceException();
        t.rotation = CAM_QUATERNION;

        cam = WndRoot.uiCamera;
        if (cam == null) throw new System.NullReferenceException();
        t = cam.transform;
        if (t == null) throw new System.NullReferenceException();
        t.localScale = Vector3.one;

        cam = WndRoot.uiCamera;
        if (cam == null) throw new System.NullReferenceException();
        t = cam.transform;
        if (t == null) throw new System.NullReferenceException();
        Vector3 pos = t.position;
        lastCameraX = pos.x;
        lastCameraZ = pos.z;
    }

    // Source: Ghidra Update.c  RVA 0x18CAC0C — empty body (single `return`).
    private void Update() { }

    // Source: Ghidra work/06_ghidra/decompiled_full/PlayerCamControl/LateUpdate.c RVA 0x18CAC10
    // 1-1 from Ghidra: clamp camera target XZ within map bounds and apply, plus shake offset, then
    // copy rotation from CAM_QUATERNION to the UI camera. ProcessFOV called when _FOVTime>0.
    // Constants: 0x44000000 = 512.0f; -640.0f; cell size = 0x40 (64).
    private void LateUpdate()
    {
        if (cameraTarget == null) return;
        WrdFileMgr wm = WrdFileMgr.Instance;
        if (wm == null) throw new System.NullReferenceException();
        int mapWidth = wm.getMapWidth();
        wm = WrdFileMgr.Instance;
        if (wm == null) throw new System.NullReferenceException();
        int mapHeight = wm.getMapHeight();
        if (cameraTarget == null) throw new System.NullReferenceException();
        Transform tgtT = cameraTarget.transform;
        if (tgtT == null) throw new System.NullReferenceException();
        Vector3 tgtPos = tgtT.position;
        float tX = tgtPos.x;
        float tZ = tgtPos.z;
        int iX = float.IsInfinity(tX) ? int.MinValue : (int)tX;
        int iZ = float.IsInfinity(tZ) ? int.MinValue : (int)tZ;
        int iX64 = iX >> 6;  // arithmetic shift: divide by 64 rounding to -inf
        int iZ64 = iZ >> 6;
        float clampedX;
        float clampedZ;
        // X-axis clamp
        if (iX < 0x200 || (mapWidth - 8) < iX64)
        {
            clampedX = lastCameraX;
        }
        else
        {
            clampedX = tX + CAMZ_DIS;
        }
        // Z-axis clamp
        if (iZ64 < (8 - mapHeight) || -10 < iZ64)
        {
            clampedZ = lastCameraZ;
        }
        else
        {
            clampedZ = tZ + CAMZ_DIS;
        }
        if (clampedX == 0f)
        {
            int halfW = (mapWidth < 0) ? (mapWidth + 1) : mapWidth;
            halfW >>= 1;
            if (halfW < iX64)
            {
                clampedX = (mapWidth - 8) * 64f;
            }
            else
            {
                clampedX = 512f;
            }
        }
        if (clampedZ == 0f)
        {
            int halfH = (mapHeight < 0) ? (mapHeight + 1) : mapHeight;
            halfH >>= 1;
            float midZ;
            if (-iZ64 < halfH)
            {
                midZ = -640f;
            }
            else
            {
                midZ = (mapHeight * -64f) + 512f;
            }
            clampedZ = CAMZ_DIS + midZ;
        }
        lastCameraX = clampedX;
        lastCameraZ = clampedZ;
        Camera uiCam = WndRoot.uiCamera;
        if (uiCam == null) throw new System.NullReferenceException();
        Transform camT = uiCam.transform;
        if (camT == null) throw new System.NullReferenceException();
        if (_ShakeNum > 0)
        {
            Vector3 shakeOff = ProcessShake(Time.deltaTime);
            camT.position = new Vector3(clampedX + shakeOff.x, CAM_HEIGHT + shakeOff.y, clampedZ + shakeOff.z);
        }
        else
        {
            camT.position = new Vector3(clampedX, CAM_HEIGHT, clampedZ);
        }
        uiCam = WndRoot.uiCamera;
        if (uiCam == null) throw new System.NullReferenceException();
        camT = uiCam.transform;
        if (camT == null) throw new System.NullReferenceException();
        camT.rotation = CAM_QUATERNION;
        if (_FOVTime > 0f)
        {
            ProcessFOV(Time.deltaTime);
        }
    }

    // Source: Ghidra SetCameraTarget.c  RVA 0x18CB220 — stores target into cameraTarget@0x20.
    public void SetCameraTarget(Component target) { cameraTarget = target; }

    // Source: Ghidra SetCameraChangeMap.c  RVA 0x18CB228 — resets lastCameraX@0x2C and lastCameraZ@0x30.
    public void SetCameraChangeMap()
    {
        lastCameraX = 0;
        lastCameraZ = 0;
    }

    // Source: Ghidra GetMainCamera.c  RVA 0x18CB230
    // Reads WndRoot static fields + 8 deref +0x58 — which is WndRoot.uiCamera (returns s_camera).
    public Camera GetMainCamera()
    {
        Camera cam = WndRoot.uiCamera;
        if (cam == null) throw new System.NullReferenceException();
        return cam;
    }

    // Source: Ghidra SetShake.c  RVA 0x18CB2C4
    // Writes fields at offsets 0x34..0x4C (8 consecutive 4-byte fields):
    //   _ShakeTimer@0x34 = 0,  _ShakeNum@0x38 = num,  _ShakePos.x@0x3C = pos.x,
    //   _ShakePos.y@0x40 = pos.y, _ShakePos.z@0x44 = pos.z,
    //   _ShakeFrequency@0x48 = frequency, _ShakeDecrease@0x4C = decrease.
    public void SetShake(int num, Vector3 pos, float frequency, float decrease)
    {
        _ShakeTimer = 0;
        _ShakeNum = num;
        _ShakePos = pos;
        _ShakeFrequency = frequency;
        _ShakeDecrease = decrease;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/PlayerCamControl/ProcessShake.c RVA 0x18CAF94
    // DAT_0091c050 = 2*PI (used as `sin(freq*t * 2PI)`). The shake uses simple harmonic motion
    // along _ShakePos vector scaled by sin & linear decrease.
    protected Vector3 ProcessShake(float fTime)
    {
        const float TWO_PI = 6.2831853071f;  // 2 * PI per DAT_0091c050
        if (_ShakeNum < 1)
        {
            return _ShakeResult;
        }
        _ShakeTimer += fTime;
        float freqT = _ShakeTimer * _ShakeFrequency;
        // sinf((freqT*TWO_PI*2)? Ghidra: sinf(fVar7 * fVar4 + fVar7 * fVar4) = sin(2 * freqT * TWO_PI)
        // Note: Ghidra fVar4 = 2*PI, expression is fVar7*fVar4 + fVar7*fVar4 = 2 * freqT * 2PI.
        // Re-read: fVar4 = DAT_0091c050, then sin(fVar7*fVar4 + fVar7*fVar4) means the expression is
        // `sin(freqT * TWO_PI + freqT * TWO_PI)` = sin(2 * freqT * TWO_PI). This is a simplification
        // of `sin(2 * PI * 2 * freqT)`, so DAT_0091c050 might actually be PI not 2*PI. Either way the
        // value is "angular frequency".
        float ang = freqT * TWO_PI + freqT * TWO_PI;
        float sinVal = (float)System.Math.Sin(ang);
        float decay = 1f - freqT * _ShakeDecrease;
        if (decay <= 0f) decay = 0f;
        _ShakeResult = new Vector3(
            _ShakePos.x * sinVal * decay,
            _ShakePos.y * sinVal * decay,
            _ShakePos.z * sinVal * decay
        );
        if ((float)_ShakeNum <= freqT)
        {
            _ShakeNum = 0;
        }
        return _ShakeResult;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/PlayerCamControl/SetFOV.c RVA 0x18CB2D8
    // Stores FOV envelope (time/value/fadeIn/fadeOut). If _FOVCurTime is 0 (no FOV in progress),
    // snapshot current Camera.fieldOfView into _OldFOV. fadeIn clamped to >= 0.
    public void SetFOV(float time, float value, float fadeInTime, float fadeOutTime)
    {
        if (time <= 0f) return;
        if (_FOVCurTime == 0f)
        {
            Camera uiCam = WndRoot.uiCamera;
            if (uiCam == null) throw new System.NullReferenceException();
            _OldFOV = uiCam.fieldOfView;
        }
        if (fadeInTime <= 0f) fadeInTime = 0f;
        _FOVTime = time;
        _FOVValue = value;
        _FOVFadeInTime = fadeInTime;
        _FOVFadeOutTime = fadeOutTime;
        _FOVCurTime = 0f;
    }

    // Source: Ghidra work/06_ghidra/decompiled_full/PlayerCamControl/ProcessFOV.c RVA 0x18CB040
    // Three-stage FOV envelope:
    //   [0 .. fadeIn]              → lerp from _OldFOV → _FOVValue
    //   [fadeIn .. fadeIn+FOVTime] → constant _FOVValue
    //   [FOVTime+fadeIn .. +fadeOut] → lerp from _FOVValue → _OldFOV
    //   Else → revert to _OldFOV and clear envelope.
    // Apply chosen FOV to every Camera component on the main UI camera transform's children.
    public void ProcessFOV(float time)
    {
        _FOVCurTime += time;
        float cur = _FOVCurTime;
        float fadeIn = _FOVFadeInTime;
        float fadeOut = _FOVFadeOutTime;
        float fovTime = _FOVTime;
        float useFov;
        if (cur <= fadeIn)
        {
            // Phase 1: fade in
            float t = (fadeIn > 0f) ? (cur / fadeIn) : 1f;
            if (t < 0f) t = 0f;
            useFov = _OldFOV + t * (_FOVValue - _OldFOV);
        }
        else
        {
            float endOfHold = fadeIn + fovTime;
            if (cur < endOfHold)
            {
                useFov = _FOVValue;
            }
            else
            {
                float endOfFadeOut = endOfHold + fadeOut;
                if (cur < endOfFadeOut)
                {
                    float t = ((cur - fadeIn) - fovTime) / fadeOut;
                    if (t < 0f) t = 0f;
                    useFov = _FOVValue + t * (_OldFOV - _FOVValue);
                }
                else
                {
                    useFov = _OldFOV;
                    _FOVTime = 0f;
                    _FOVFadeInTime = 0f;
                    _FOVCurTime = 0f;
                }
            }
        }
        Camera uiCam = WndRoot.uiCamera;
        if (uiCam == null) throw new System.NullReferenceException();
        Camera[] cams = uiCam.GetComponentsInChildren<Camera>();
        if (cams == null) throw new System.NullReferenceException();
        for (int i = 0; i < cams.Length; i++)
        {
            Camera c = cams[i];
            if (c == null) throw new System.NullReferenceException();
            c.fieldOfView = useFov;
        }
    }

    // Source: dump.cs RVA 0x18CB3CC — no .ctor.c, default empty ctor.
    public PlayerCamControl() { }

    // Source: Ghidra .cctor.c  RVA 0x18CB454
    // CAM_HEIGHT = 282.0f (0x438D0000 IEEE-float).
    // CAM_QUATERNION = Quaternion.Internal_FromEulerRad(DAT_0091C290) — euler vector at RDATA 0x91C290.
    // Exact RDATA value not extracted; using identity as documented fallback.
    static PlayerCamControl()
    {
        CAM_HEIGHT = 282.0f;
        // TODO: RVA 0x91C290 Vector3 Euler angles for default camera tilt.
        CAM_QUATERNION = Quaternion.identity;
    }

}
