// Source: work/03_il2cpp_dump/dump.cs MarsSDK.Permission.PermissionManager +
//   work/06_ghidra/decompiled_full/MarsSDK.Permission.PermissionManager/*.c
// All methods ported 1-1 from Ghidra. Where Unity Editor cannot reach Android JNI
// (AndroidJavaClass / AndroidJavaObject), behavior is documented per §E10 SDK strip.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarsSDK.Permission
{
	public class PermissionManager : MarsMessageProcess
	{
		private delegate void PermissionProcess(string[] permissions);

		private static PermissionManager mInstance;

		private static PermissionCallback _callback;

		public const string PERMISSION_MSG_ONCANCEL = "0";
		public const string PERMISSION_MSG_ONCONFIRM = "1";
		public const string PERMISSION_MSG_USRDENIED = "2";

		private Dictionary<string, PermissionProcess> mPermissionProcess;

		public static AndroidJavaObject _permissionManager;

		public static bool _initialized;

		// Source: Ghidra .ctor.c RVA 0x019e82f8
		// 1-1: mPermissionProcess = new Dictionary<string, PermissionProcess>();
		//      base.MarsMessageProcess(EOperationAgent.PermissionMaganger=0);
		//      mPermissionProcess["0"] = PermissionProcessOnCancel;   (StringLit_1034)
		//      mPermissionProcess["1"] = PermissionProcessOnConfirm;  (StringLit_1071)
		//      mPermissionProcess["2"] = PermissionProcessOnDenied;   (StringLit_1312)
		//      DoInitialize();
		public PermissionManager() : base(EOperationAgent.PermissionMaganger)
		{
			mPermissionProcess = new Dictionary<string, PermissionProcess>();
			mPermissionProcess[PERMISSION_MSG_ONCANCEL]  = new PermissionProcess(this.PermissionProcessOnCancel);
			mPermissionProcess[PERMISSION_MSG_ONCONFIRM] = new PermissionProcess(this.PermissionProcessOnConfirm);
			mPermissionProcess[PERMISSION_MSG_USRDENIED] = new PermissionProcess(this.PermissionProcessOnDenied);
			DoInitialize();
		}

		// Source: Ghidra GetMessageUserType.c RVA 0x019e88b0
		// 1-1: return typeof(PermissionAgent) via Type.GetTypeFromHandle.
		// PTR_DAT_0346a1f8 token resolves to EOperationAgent enum (the message-user type per Mars SDK convention).
		public override Type GetMessageUserType()
		{
			return typeof(EOperationAgent);
		}

		// Source: Ghidra Instance.c RVA 0x019e891c
		// 1-1: if (mInstance == null) mInstance = new PermissionManager(); return mInstance;
		public static PermissionManager Instance()
		{
			if (mInstance == null)
			{
				mInstance = new PermissionManager();
			}
			return mInstance;
		}

		// Source: Ghidra DoInitialize.c RVA 0x019e8584
		// 1-1:
		//   var jc = new AndroidJavaClass("com.userjoy.mars.core.common.PermissionManager");  (StringLit_15691)
		//   _permissionManager = jc.CallStatic<AndroidJavaObject>("Instance", AndroidJavaObject<>(currentActivity));  (StringLit_6609)
		//   jc.Dispose();
		//   if (_permissionManager == null) MarsLog.Info(StringLit_6574 "Initialize Failed!!");
		//   else _initialized = true;
		// [§E10 SDK strip: Mars SDK Java class is bundled into the original APK's AAR. In
		//  Editor mode without an Android JVM context, AndroidJavaClass.CallStatic throws.
		//  We attempt the call (matches gốc) but wrap with try/catch so editor-mode init
		//  does not abort the proc machine. On failure, _permissionManager stays null and
		//  _initialized = false, mirroring gốc's "Initialize Failed" branch.]
		public static void DoInitialize()
		{
			try
			{
				using (var jc = new AndroidJavaClass("com.userjoy.mars.core.common.PermissionManager"))
				{
					// Pass currentActivity (via AndroidJavaClass("UnityPlayer").GetStatic<>("currentActivity"))
					using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
					{
						var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
						_permissionManager = jc.CallStatic<AndroidJavaObject>("Instance", activity);
					}
				}
			}
			catch (System.Exception ex)
			{
				UnityEngine.Debug.LogWarning("[PermissionManager.DoInitialize] §E10 stripped JNI: " + ex.Message);
				_permissionManager = null;
			}

			if (_permissionManager == null)
			{
				UnityEngine.Debug.Log("[MarsLog] Initialize Failed!!");  // StringLit_6574
			}
			else
			{
				_initialized = true;
			}
		}

		// Source: Ghidra V_doMessageProcess.c RVA 0x019e89a0
		// 1-1:
		//   if (mPermissionProcess == null) NRE;
		//   var handler = mPermissionProcess[msg];
		//   if (handler == null) NRE;
		//   handler.Invoke(args);
		protected override void V_doMessageProcess(string msg, string[] args)
		{
			if (mPermissionProcess == null) throw new System.NullReferenceException();
			PermissionProcess handler;
			if (!mPermissionProcess.TryGetValue(msg, out handler) || handler == null)
				throw new System.NullReferenceException();
			handler(args);
		}

		// Source: Ghidra RequestPermission.c RVA 0x19e8b88
		// 1-1:
		//   if (callback == null) callback = new PermissionCallback(null, null, null);
		//   _callback = callback;                                         // static field
		//   MarsLog.Info("RequestPermission call java api", <opaque-ctx>); // StringLit_9546
		//   _permissionManager.Call("RequestPermission", new object[]{permission}); // StringLit_9545
		// [§E10 SDK strip: skips JNI call when _permissionManager is null (DoInitialize JNI
		//  failed in Editor); matches null-deref trap behavior on real device.]
		// TODO: confidence:medium — the second MarsLog.Info arg traces back to PTR_DAT_03446688
		//   static field +0xb8; cross-ref unresolved. Passing `permission` here as
		//   contextual identifier matches typical log shape; replace once symbol known.
		public void RequestPermission(string permission, PermissionCallback callback = null)
		{
			if (callback == null)
			{
				callback = new PermissionCallback(null, null, null);
			}
			_callback = callback;
			MarsLog.Info("RequestPermission call java api", permission);
			if (_permissionManager == null)
			{
				// §E10: in Editor without JNI, FUN_015cb8fc would throw. Log + return to allow
				// proc machine to keep advancing (mirrors Initialize Failed branch).
				UnityEngine.Debug.LogWarning("[PermissionManager.RequestPermission] §E10 stripped — _permissionManager null");
				return;
			}
			_permissionManager.Call("RequestPermission", new object[] { permission });
		}

		// Source: Ghidra RequestPermissions.c RVA 0x19e8d6c
		// 1-1:
		//   if (callback == null) callback = new PermissionCallback(null, null, null);
		//   _callback = callback;
		//   _permissionManager.Call("RequestPermissions", new object[]{permissions});  // StringLit_9547
		// (no MarsLog.Info in this overload — matches simpler Ghidra body)
		public void RequestPermissions(string[] permissions, PermissionCallback callback = null)
		{
			if (callback == null)
			{
				callback = new PermissionCallback(null, null, null);
			}
			_callback = callback;
			if (_permissionManager == null)
			{
				// §E10 SDK strip
				UnityEngine.Debug.LogWarning("[PermissionManager.RequestPermissions] §E10 stripped — _permissionManager null");
				return;
			}
			_permissionManager.Call("RequestPermissions", new object[] { permissions });
		}

		// Source: Ghidra CheckPermission.c RVA 0x019e8e9c
		// 1-1:
		//   var args = new object[1] { permission };
		//   if (_permissionManager == null) NRE;
		//   return _permissionManager.Call<bool>("CheckPermission", args);  (StringLit_4040)
		// [§E10 SDK strip: when _permissionManager is null (DoInitialize JNI failed in Editor),
		//  the gốc behavior is NRE. To allow proc machine advancement in Editor, we return true
		//  (auto-grant) instead of NRE. Real Android builds get the JNI call result as-is.]
		public bool CheckPermission(string permission)
		{
			object[] args = new object[] { permission };
			if (_permissionManager == null)
			{
				// §E10: Editor-mode stub — auto-grant since JNI is unavailable.
				return true;
			}
			return _permissionManager.Call<bool>("CheckPermission", args);
		}

		// Source: Ghidra PermissionCanShow.c RVA 0x19e8f98 — body is literally `return 1;`.
		// 1-1: const-true on Android since SDK always allows the rationale dialog; iOS
		// implementations override.
		public bool PermissionCanShow(string permission)
		{
			return true;
		}

		// Source: Ghidra OpenPermissionSetting.c RVA 0x19e8fa0
		// 1-1: _permissionManager.Call("OpenPermissionSetting", <currentActivity>);  StringLit_8842
		//   (Ghidra: passes **(undefined8 **)(lVar3 + 0xb8) — UnityPlayer.currentActivity static slot.)
		// [§E10 SDK strip: skip when _permissionManager is null in Editor; mirrors NRE branch.]
		public void OpenPermissionSetting()
		{
			if (_permissionManager == null)
			{
				UnityEngine.Debug.LogWarning("[PermissionManager.OpenPermissionSetting] §E10 stripped — _permissionManager null");
				return;
			}
			AndroidJavaObject activity = null;
			try
			{
				using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				}
			}
			catch (System.Exception ex)
			{
				UnityEngine.Debug.LogWarning("[PermissionManager.OpenPermissionSetting] §E10 currentActivity unavailable: " + ex.Message);
			}
			_permissionManager.Call("OpenPermissionSetting", new object[] { activity });
		}

		// Source: Ghidra PermissionProcessOnCancel.c RVA 0x19e9074
		// 1-1: if (_callback != null) _callback.OnCancel(permissions);
		private void PermissionProcessOnCancel(string[] permissions)
		{
			if (_callback != null)
			{
				_callback.OnCancel(permissions);
			}
		}

		// Source: Ghidra PermissionProcessOnConfirm.c RVA 0x19e90d4
		// 1-1: if (_callback != null) _callback.OnConfirm(permissions);
		private void PermissionProcessOnConfirm(string[] permissions)
		{
			if (_callback != null)
			{
				_callback.OnConfirm(permissions);
			}
		}

		// Source: Ghidra PermissionProcessOnDenied.c RVA 0x19e9134
		// 1-1: if (_callback != null) _callback.OnDenied(permissions);
		private void PermissionProcessOnDenied(string[] permissions)
		{
			if (_callback != null)
			{
				_callback.OnDenied(permissions);
			}
		}
	}
}
