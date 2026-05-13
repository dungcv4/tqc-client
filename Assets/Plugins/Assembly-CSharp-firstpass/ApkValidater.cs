// Source: Ghidra-decompiled libil2cpp.so
// RVAs: 0x1589B84, 0x158A140, 0x158A26C, 0x158A5BC, 0x158A5C4
// Ghidra dir: work/06_ghidra/decompiled_full/ApkValidater/

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

// Source: Il2CppDumper-stub  TypeDefIndex: 8229
public class ApkValidater
{
    // RVA: 0x1589B84  Ghidra: work/06_ghidra/decompiled_full/ApkValidater/GetData.c
    public static string GetData()
    {
        // uVar11 starts as the empty-string literal (PTR_StringLiteral_0_034465a0 = "").
        string s = string.Empty;
        bool b = false;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass build = new AndroidJavaClass("android.os.Build");
            // First GetStatic ("BRAND") result is discarded in Ghidra body.
            build.GetStatic<string>("BRAND");
            // uVar5..uVar9 from Ghidra (in declaration order):
            string uVar5_FINGERPRINT  = build.GetStatic<string>("FINGERPRINT");
            string uVar6_MODEL        = build.GetStatic<string>("MODEL");
            string uVar7_MANUFACTURER = build.GetStatic<string>("MANUFACTURER");
            string uVar8_DEVICE       = build.GetStatic<string>("DEVICE");
            string uVar9_PRODUCT      = build.GetStatic<string>("PRODUCT");

            s = string.Concat(s, Application.installerName);
            s = string.Concat(s, "/");
            ApplicationInstallMode installMode = Application.installMode;
            s = string.Concat(s, installMode.ToString());
            s = string.Concat(s, "/");
            s = string.Concat(s, Application.buildGUID);
            s = string.Concat(s, "/");
            b = Application.genuine;
            s = string.Concat(s, "Genuine :", b.ToString());
            s = string.Concat(s, "/");
            b = ApkValidater.isRooted();
            s = string.Concat(s, "Rooted : ", b.ToString());
            s = string.Concat(s, "/");
            b = ApkValidater.IsEmulator();
            s = string.Concat(s, "Emulator : ", b.ToString());
            s = string.Concat(s, "/");
            s = string.Concat(s, "Model : ", uVar6_MODEL);
            s = string.Concat(s, "/");
            s = string.Concat(s, "Menufacturer : ", uVar7_MANUFACTURER);
            s = string.Concat(s, "/");
            s = string.Concat(s, "Device : ", uVar8_DEVICE);
            s = string.Concat(s, "/");
            s = string.Concat(s, "Fingerprint : ", uVar5_FINGERPRINT);
            s = string.Concat(s, "/");
            // Final System_String__Concat — Ghidra discards the return; preserved as
            // the last assignment so the method returns the final composed string.
            s = string.Concat(s, "Product : ", uVar9_PRODUCT);
        }
        else
        {
            s = string.Concat(s, Application.installerName);
            s = string.Concat(s, "/");
            ApplicationInstallMode installMode = Application.installMode;
            s = string.Concat(s, installMode.ToString());
            s = string.Concat(s, "/");
            s = string.Concat(s, Application.buildGUID);
            s = string.Concat(s, "/");
            b = Application.genuine;
            s = string.Concat(s, "Genuine :", b.ToString());
            // Ghidra discards the final concat — preserved as final assignment.
            s = string.Concat(s, "/");
        }
        return s;
    }

    // RVA: 0x158A140  Ghidra: work/06_ghidra/decompiled_full/ApkValidater/isRooted.c
    public static bool isRooted()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            uint a = System.IO.File.Exists("/system/bin/su") ? 1u : 0u;
            uint b = System.IO.File.Exists("/system/xbin/su") ? 1u : 0u;
            uint c = System.IO.File.Exists("/system/app/SuperUser.apk") ? 1u : 0u;
            uint d = System.IO.File.Exists("/data/data/com.noshufou.android.su") ? 1u : 0u;
            uint e = System.IO.File.Exists("/sbin/su") ? 1u : 0u;
            return ((a | b | c | d | e) & 1u) != 0u;
        }
        else
        {
            return false;
        }
    }

    // RVA: 0x158A26C  Ghidra: work/06_ghidra/decompiled_full/ApkValidater/IsEmulator.c
    public static bool IsEmulator()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            // Ghidra returns (platform == 0). RuntimePlatform value 0 is OSXEditor.
            return Application.platform == (RuntimePlatform)0;
        }
        AndroidJavaClass build = new AndroidJavaClass("android.os.Build");
        if (build != null)
        {
            // Order matches Ghidra GetStatic call sequence.
            string fingerprint   = build.GetStatic<string>("FINGERPRINT");   // lit 5307
            string model         = build.GetStatic<string>("MODEL");         // lit 7865
            string manufacturer  = build.GetStatic<string>("MANUFACTURER");  // lit 7792
            string brand         = build.GetStatic<string>("BRAND");         // lit 3298
            string device        = build.GetStatic<string>("DEVICE");        // lit 4513
            string product       = build.GetStatic<string>("PRODUCT");       // lit 8942

            if (fingerprint != null)
            {
                // lit 16823 = "generic", lit 20695 = "unknown"
                if (fingerprint.Contains("generic")) return true;
                if (fingerprint.Contains("unknown")) return true;
                if (model != null)
                {
                    // lit 16974 = "google_sdk", lit 4976 = "Emulator", lit 2996 = "Android SDK built for x86"
                    if (model.Contains("google_sdk")) return true;
                    if (model.Contains("Emulator")) return true;
                    if (model.Contains("Android SDK built for x86")) return true;
                    if (manufacturer != null)
                    {
                        // lit 5680 = "Genymotion"
                        if (manufacturer.Contains("Genymotion")) return true;
                        if (brand != null)
                        {
                            // lit 16823 = "generic"
                            if (brand.Contains("generic"))
                            {
                                if (device == null) throw new System.NullReferenceException();
                                if (device.Contains("generic")) return true;
                            }
                            if (product != null)
                            {
                                // lit 16974 = "google_sdk", lit 20695 = "unknown"
                                if (product.Equals("google_sdk")) return true;
                                return product.Equals("unknown");
                            }
                        }
                    }
                }
            }
        }
        // Ghidra fall-through: FUN_015cb8fc — null-deref / unreachable terminator.
        throw new System.NullReferenceException();
    }

    // RVA: 0x158A5BC  Ghidra: work/06_ghidra/decompiled_full/ApkValidater/isRootedPrivate.c
    public static bool isRootedPrivate(string path)
    {
        // Ghidra body: System_IO_File__Exists(param_1,0); return; (return value discarded).
        // Translated faithfully — the call's result is not propagated.
        System.IO.File.Exists(path);
        // TODO: confidence:low — Ghidra body returns void (no explicit return value);
        // declared signature is bool. Returning the File.Exists result is the most
        // plausible intent but Ghidra dropped the assignment.
        throw new System.NotImplementedException();
    }

    // RVA: 0x158A5C4  Ghidra: work/06_ghidra/decompiled_full/ApkValidater/.ctor.c
    public ApkValidater()
    {
        // TODO: confidence:low — .ctor.c not present in decompiled_full/ApkValidater/.
        // Default object constructor body assumed.
    }

}
