// Source: Ghidra work/06_ghidra/decompiled_full/UJDebug/ (16 methods + ctor + cctor)
// Source: dump.cs TypeDefIndex 134
// ALL 16 methods ported 1-1 from Ghidra.
// String literals (from work/03_il2cpp_dump/stringliteral.json):
//   #2415="<color=yellow>", #2282=":</color>"
//   #9214="ProcessBase",    #9205="PrintStackTrace"

using System;
using UnityEngine;

public class UJDebug
{
    public delegate void CBSendLogToServer(int nLogType, string msg, string stackTrace);

    public const int Level_Off       = -1;
    public const int Level_Exception = 0;
    public const int Level_Error     = 1;
    public const int Level_Warning   = 2;
    public const int Level_Assert    = 3;
    public const int Level_Info      = 4;
    public const int Level_Log       = 5;
    public const int Level_Trace     = 6;

    public static UJLogType nowOpenLogType;        // static 0x0
    private static int s_min_log_Level;            // static 0x4
    private static CBSendLogToServer s_cbSend;     // static 0x8

    // ─── Helper (private) — builds tagged message string ────────────────────
    // Ghidra inlines this concat pattern in Log/LogWarning/LogError; extracted for DRY.
    // Format: "<color=yellow>" + type.ToString() + ":</color>" + message
    private static object TagMessage(object message, UJLogType type)
    {
        if (type == UJLogType.None) return message;
        return string.Concat("<color=yellow>", type.ToString(), ":</color>",
                              message == null ? null : message.ToString());
    }

    private static string TagFormat(string format, UJLogType type)
    {
        if (type == UJLogType.None) return format;
        return string.Concat("<color=yellow>", type.ToString(), ":</color>", format);
    }

    // ─── Filter accessors (1-1 ports) ──────────────────────────────────────

    // Source: Ghidra .../AddLogType.c  RVA 0x17BCAC0
    public static void AddLogType(UJLogType type)
    {
        nowOpenLogType = (UJLogType)((uint)nowOpenLogType | (uint)type);
    }

    // Source: Ghidra .../RemoveLogType.c  RVA 0x17BCB24
    public static void RemoveLogType(UJLogType type)
    {
        nowOpenLogType = (UJLogType)((uint)nowOpenLogType & ~(uint)type);
    }

    // Source: Ghidra .../SetLogLevel.c  RVA 0x17BCB88
    public static void SetLogLevel(int nLevel)
    {
        s_min_log_Level = nLevel;
    }

    // Source: Ghidra .../SetSendCB.c  RVA 0x17BCBE4
    public static void SetSendCB(CBSendLogToServer func)
    {
        s_cbSend = func;
    }

    // Source: Ghidra .../GetStackTrace.c  RVA 0x17BCC44
    // Walk \n boundaries to skip first nIgnoreStack frames.
    public static string GetStackTrace(int nIgnoreStack = 3)
    {
        string stackTrace = System.Environment.StackTrace;
        if (nIgnoreStack < 1) return stackTrace;
        if (stackTrace == null) throw new System.NullReferenceException();
        int idx = 0;
        int remaining = nIgnoreStack;
        while (remaining != 0)
        {
            int newlinePos = stackTrace.IndexOf('\n', idx);
            if (newlinePos < 0)
            {
                if (idx == 0) return stackTrace;
                break;
            }
            remaining--;
            idx = newlinePos + 1;
        }
        return stackTrace.Substring(idx);
    }

    // ─── Log methods (1-1 from Ghidra; same skeleton per level) ────────────

    // Source: Ghidra .../LogTrace.c  RVA 0x17BCCC4
    // Level filter: s_min_log_Level >= Level_Trace (6). No type filter, no toServer.
    public static void LogTrace(object message)
    {
        if (s_min_log_Level < Level_Trace) return;
        UnityEngine.Debug.Log(message);
    }

    // Source: Ghidra .../Log.c  RVA 0x17B612C
    // Level filter: s_min_log_Level > 3 (Info/Log/Trace).
    // If type != None: filter (nowOpenLogType & type) == 0 → skip.
    // Build tagged msg, Debug.Log, then optional s_cbSend(4=Log, msg, stack).
    public static void Log(object message, bool toServer = false, UJLogType type = UJLogType.None)
    {
        if (s_min_log_Level <= 3) return;
        if (type != UJLogType.None)
        {
            if (((uint)nowOpenLogType & (uint)type) == 0) return;
        }
        object tagged = TagMessage(message, type);
        UnityEngine.Debug.Log(tagged);
        if (toServer && s_cbSend != null && tagged != null)
        {
            s_cbSend(Level_Info /*4 per Ghidra*/, tagged.ToString(), GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogException.c  RVA 0x17BA538
    // Level filter: s_min_log_Level >= 0 (Exception+).
    // Calls Debug.LogError(exception.Message), then optional s_cbSend(... 0? — see note).
    public static void LogException(Exception exception)
    {
        if (s_min_log_Level < Level_Exception) return;
        if (exception == null) throw new System.NullReferenceException();
        string msg = exception.Message;
        UnityEngine.Debug.LogError(msg);
        if (s_cbSend != null)
        {
            s_cbSend(Level_Exception, msg, GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogWarning.c  RVA 0x17B85D0
    // Level filter: s_min_log_Level > 1 (Warning/Assert/Info/Log/Trace).
    public static void LogWarning(object message, bool toServer = false, UJLogType type = UJLogType.None)
    {
        if (s_min_log_Level <= 1) return;
        if (type != UJLogType.None)
        {
            if (((uint)nowOpenLogType & (uint)type) == 0) return;
        }
        object tagged = TagMessage(message, type);
        UnityEngine.Debug.LogWarning(tagged);
        if (toServer && s_cbSend != null && tagged != null)
        {
            s_cbSend(Level_Warning /*2 per Ghidra*/, tagged.ToString(), GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogError.c  RVA 0x17B696C
    // Level filter: s_min_log_Level > 0 (Error/Warning/...).
    public static void LogError(object message, bool toServer = false, UJLogType type = UJLogType.None)
    {
        if (s_min_log_Level <= 0) return;
        if (type != UJLogType.None)
        {
            if (((uint)nowOpenLogType & (uint)type) == 0) return;
        }
        object tagged = TagMessage(message, type);
        UnityEngine.Debug.LogError(tagged);
        if (toServer && s_cbSend != null && tagged != null)
        {
            s_cbSend(Level_Error /*1 per Ghidra*/, tagged.ToString(), GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogFormat.c  RVA 0x17B5F58
    // Level filter: > 3. Build tagged format, Debug.LogFormat, optional cbSend(4=Log).
    public static void LogFormat(string format, bool toServer = false, UJLogType type = UJLogType.None, params object[] args)
    {
        if (s_min_log_Level <= 3) return;
        if (type != UJLogType.None)
        {
            if (((uint)nowOpenLogType & (uint)type) == 0) return;
        }
        string tagged = TagFormat(format, type);
        UnityEngine.Debug.LogFormat(tagged, args);
        if (toServer && s_cbSend != null)
        {
            s_cbSend(Level_Info /*4*/, string.Format(tagged, args), GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogWarningFormat.c  RVA 0x17BCD60
    public static void LogWarningFormat(string format, bool toServer = false, UJLogType type = UJLogType.None, params object[] args)
    {
        if (s_min_log_Level <= 1) return;
        if (type != UJLogType.None)
        {
            if (((uint)nowOpenLogType & (uint)type) == 0) return;
        }
        string tagged = TagFormat(format, type);
        UnityEngine.Debug.LogWarningFormat(tagged, args);
        if (toServer && s_cbSend != null)
        {
            s_cbSend(Level_Warning /*2*/, string.Format(tagged, args), GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogErrorFormat.c  RVA 0x17BCF34
    public static void LogErrorFormat(string format, bool toServer = false, UJLogType type = UJLogType.None, params object[] args)
    {
        if (s_min_log_Level <= 0) return;
        if (type != UJLogType.None)
        {
            if (((uint)nowOpenLogType & (uint)type) == 0) return;
        }
        string tagged = TagFormat(format, type);
        UnityEngine.Debug.LogErrorFormat(tagged, args);
        if (toServer && s_cbSend != null)
        {
            s_cbSend(Level_Error /*1*/, string.Format(tagged, args), GetStackTrace(3));
        }
    }

    // Source: Ghidra .../LogLuaStackTrace.c  RVA 0x17BD108
    // Body: LuaFramework.Util.CallMethod(literal#9214 "ProcessBase", literal#9205 "PrintStackTrace", new object[] { message })
    public static void LogLuaStackTrace(string message)
    {
        LuaFramework.Util.CallMethod("ProcessBase", "PrintStackTrace", new object[] { message });
    }

    // Source: Ghidra (no .ctor.c) — default empty ctor.
    // RVA: 0x17BD1E4
    public UJDebug() { }

    // Source: Ghidra (no .cctor.c) — defaults inferred from field types.
    // RVA: 0x17BD1EC
    static UJDebug()
    {
        nowOpenLogType = UJLogType.None;
        s_min_log_Level = Level_Log;
        s_cbSend = null;
    }
}
