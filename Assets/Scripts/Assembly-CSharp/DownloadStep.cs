// Source: dump.cs enum 'DownloadStep' (TypeDefIndex: 806)

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

public enum DownloadStep
{
    None = 0,
    WaitPercent = 1,
    LOGO = 2,
    LOGO_CN = 3,
    WARN = 4,
    CheckNetWork = 5,
    LoadPatchList = 6,
    WaitPatchList = 7,
    LoadAppVersion = 8,
    WaitAppVersion = 9,
    WaitCloseCheck = 10,
    GetResourceMainBundleManifest = 11,
    WaitResourceMainBundleManifest = 12,
    GetMainBundleManifest = 13,
    WaitMainBundleManifest = 14,
    LoadVersion = 15,
    WaitVersion = 16,
    LoadBundleSize = 17,
    WaitBundleSize = 18,
    CheckDownloadSize = 19,
    WaitDownloadCheck = 20,
    ShowDownloadCheck = 21,
    WaitLoadingCheck = 22,
    LoadServerList = 23,
    WaitServerList = 24,
    GetNeedDownLoadBundle = 25,
    GetLuaBundle = 26,
    WaitLuaBundle = 27,
    GetFontBundle = 28,
    WaitFontBundle = 29,
    GetMainUIBundle = 30,
    WaitMainUIBundle = 31,
    GetItemIconBundle = 32,
    WaitItemIconBundle = 33,
    GetHeadIconBundle = 34,
    WaitHeadIconBundle = 35,
    GetSkillIconBundle = 36,
    WaitSkillIconBundle = 37,
    GetFXBundle = 38,
    WaitFXBundle = 39,
    GetCreateCharBundle = 40,
    WaitCreateCharBundle = 41,
    GetSMapBundle = 42,
    WaitSMapBundle = 43,
    GetMapDataBundle = 44,
    WaitMapDataBundle = 45,
    GetSoundBundle = 46,
    WaitSoundBundle = 47,
    GetUIFXBundle = 48,
    WaitUIFXBundle = 49,
    GetMagicDataBundle = 50,
    WaitMagicDataBundle = 51,
    GetMagicFxBundle = 52,
    WaitMagicFxBundle = 53,
    GetCardIconBundle = 54,
    WaitCardIconBundle = 55,
    GetEmojiBundle = 56,
    WaitEmojiBundle = 57,
    GetSceneBundle = 58,
    WaitSceneBundle = 59,
    GetModelBundle = 60,
    WaitModelBundle = 61,
    GetMusicBundle = 62,
    WaitMusicBundle = 63,
    GetMenuBundle = 64,
    WaitMenuBundle = 65,
    Completed = 66,
}
