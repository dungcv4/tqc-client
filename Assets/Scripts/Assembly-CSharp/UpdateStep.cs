// Source: dump.cs enum 'UpdateStep' (TypeDefIndex: 805)

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

public enum UpdateStep
{
    None = 0,
    LoadSGCInitSettingsStep = 1,
    WaitSGCInitSettingsStep = 2,
    ConfirmSGCInitSettingsStep = 3,
    SetLanguageStep = 4,
    WaitSelectLanguage = 5,
    SetAreaStep = 6,
    WaitSelectRegion = 7,
    CheckPermissionStep = 8,
    WaitPermissionStep = 9,
    ConfirmStep = 10,
    CheckPersonalInfoStep = 11,
    WaitPersonalInfoStep = 12,
    ConfirmPersonalInfoStep = 13,
    CreateLunchGameWnd = 14,
    WaitLunchGameWnd = 15,
    LoadBasicUITextStep = 16,
    ConfigStep = 17,
    DownLoadStep = 18,
    LoadingStep = 19,
    WaitCloseCheck = 20,
    Completed = 21,
}
