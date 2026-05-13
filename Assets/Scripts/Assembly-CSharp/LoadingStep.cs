// Source: dump.cs enum 'LoadingStep' (TypeDefIndex: 807)

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

public enum LoadingStep
{
    None = 0,
    WaitPercent = 1,
    LoadingLua = 2,
    LoadingLuaWaiting = 3,
    Completed = 4,
}
