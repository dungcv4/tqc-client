// Try Gen All again — after Play mode init, Debugger DLL should be loaded.

using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;

public static class RunToLuaGenAll
{
    public static void Execute()
    {
        try
        {
            UnityEngine.Debug.Log("[GenAll v4] Start");

            // Pre-load Debugger by forcing its DLL into memory via known type
            try
            {
                var debugType = Type.GetType("LuaInterface.Debugger, Debugger");
                if (debugType != null)
                {
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(debugType.TypeHandle);
                    UnityEngine.Debug.Log("[GenAll v4] Debugger cctor OK");
                }
                else
                {
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        debugType = asm.GetType("LuaInterface.Debugger");
                        if (debugType != null)
                        {
                            try
                            {
                                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(debugType.TypeHandle);
                                UnityEngine.Debug.Log($"[GenAll v4] Debugger cctor OK from {asm.GetName().Name}");
                            }
                            catch (Exception dex)
                            {
                                UnityEngine.Debug.LogWarning($"[GenAll v4] Debugger cctor: {dex.GetType().Name}: {dex.Message}");
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning($"[GenAll v4] Debugger pre-load: {ex.Message}");
            }

            // Now try menu items
            EditorApplication.ExecuteMenuItem("Lua/Gen Lua Delegates");
            AssetDatabase.Refresh();

            EditorApplication.ExecuteMenuItem("Lua/Gen Lua Wrap Files");
            AssetDatabase.Refresh();

            EditorApplication.ExecuteMenuItem("Lua/Gen LuaBinder File");
            AssetDatabase.Refresh();

            int count = Directory.GetFiles("Assets/Source/Generate", "*.cs").Length;
            UnityEngine.Debug.Log($"[GenAll v4] Done. Files: {count}");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"[GenAll v4] Failed: {ex}");
            throw;
        }
    }
}
