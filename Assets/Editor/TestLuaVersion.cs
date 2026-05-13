using System.IO;
using UnityEngine;
using LuaInterface;

public class TestLuaVersion
{
    public static void Execute()
    {
        var sb = new System.Text.StringBuilder();
        var all = Resources.FindObjectsOfTypeAll<LuaFramework.LuaManager>();
        if (all.Length == 0) { File.WriteAllText("/tmp/lua_ver.txt", "No LuaManager"); return; }
        var lm = all[0];

        var luaField = typeof(LuaFramework.LuaManager).GetField("lua",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var lua = luaField?.GetValue(lm) as LuaState;
        if (lua == null) { File.WriteAllText("/tmp/lua_ver.txt", "lua field null"); return; }

        System.Func<string, string> getGlobalString = (name) => {
            lua.LuaGetGlobal(name);
            string s = lua.LuaToString(-1);
            lua.LuaPop(1);
            return s;
        };

        try {
            lua.DoString("_G._lua_version_test = tostring(_VERSION) .. ' jit=' .. tostring(jit and jit.version or 'no-jit')", "ver_probe");
            sb.AppendLine("Lua _VERSION: " + getGlobalString("_lua_version_test"));
        } catch (System.Exception e) { sb.AppendLine("VER probe failed: " + e.Message); }

        // TEST 1: Original pattern using 'arg'
        try {
            lua.DoString(@"
function f_with_arg(...)
    local arg = {...}
    return function(...)
        local arg2 = {...}
        return 'arg=' .. tostring(arg) .. ' #arg=' .. tostring(arg and #arg or 'NIL_arg') .. ' arg2=' .. tostring(arg2)
    end
end
_G._cb_arg_empty = f_with_arg()
_G._cb_arg_2args = f_with_arg('x', 'y')
", "arg_test");
            var fn1 = lua.GetFunction("_cb_arg_empty"); fn1.BeginPCall(); fn1.PushObject(new object()); fn1.PCall();
            sb.AppendLine("TEST 1a (arg, empty): " + lua.LuaToString(-1));
            fn1.EndPCall(); fn1.Dispose();

            var fn2 = lua.GetFunction("_cb_arg_2args"); fn2.BeginPCall(); fn2.PushObject(new object()); fn2.PCall();
            sb.AppendLine("TEST 1b (arg, 2args): " + lua.LuaToString(-1));
            fn2.EndPCall(); fn2.Dispose();
        } catch (System.Exception e) { sb.AppendLine("TEST 1 ex: " + e.Message); }

        // TEST 2: Renamed pattern using 'argv'
        try {
            lua.DoString(@"
function f_with_argv(...)
    local argv = {...}
    return function(...)
        local argv2 = {...}
        return 'argv=' .. tostring(argv) .. ' #argv=' .. tostring(argv and #argv or 'NIL_argv') .. ' argv2=' .. tostring(argv2)
    end
end
_G._cb_argv_empty = f_with_argv()
_G._cb_argv_2args = f_with_argv('x', 'y')
", "argv_test");
            var fn1 = lua.GetFunction("_cb_argv_empty"); fn1.BeginPCall(); fn1.PushObject(new object()); fn1.PCall();
            sb.AppendLine("TEST 2a (argv, empty): " + lua.LuaToString(-1));
            fn1.EndPCall(); fn1.Dispose();

            var fn2 = lua.GetFunction("_cb_argv_2args"); fn2.BeginPCall(); fn2.PushObject(new object()); fn2.PCall();
            sb.AppendLine("TEST 2b (argv, 2args): " + lua.LuaToString(-1));
            fn2.EndPCall(); fn2.Dispose();
        } catch (System.Exception e) { sb.AppendLine("TEST 2 ex: " + e.Message); }

        // TEST 3: Non-vararg outer with local 'arg'
        try {
            lua.DoString(@"
function f_no_vararg()
    local arg = {1, 2, 3}
    return function()
        return 'no_vararg arg=' .. tostring(arg) .. ' #arg=' .. tostring(arg and #arg or 'NIL')
    end
end
_G._cb_no_vararg = f_no_vararg()
", "no_vararg_test");
            var fn = lua.GetFunction("_cb_no_vararg"); fn.BeginPCall(); fn.PCall();
            sb.AppendLine("TEST 3 (non-vararg+arg): " + lua.LuaToString(-1));
            fn.EndPCall(); fn.Dispose();
        } catch (System.Exception e) { sb.AppendLine("TEST 3 ex: " + e.Message); }

        File.WriteAllText("/tmp/lua_ver.txt", sb.ToString());
        Debug.Log("[TestLuaVersion]\n" + sb.ToString());
    }
}
