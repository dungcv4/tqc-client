using System;
using Cpp2IlInjected;
using MarsSDK.ThirdParty.Extensions;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class MarsPlatform : BasePlatform
	{
		public delegate void dMsgProcess(string[] args);

		public delegate void dMsgExtensionProcess(ExtensionDefinition extensionDefinition, string[] otherInfo);

		public const string MARS_PLATFORM_TEST_MSG = "0";

		public const string MARS_PLATFORM_MSG_SUCCESS = "1";

		public const string MARS_PLATFORM_MSG_ERROR = "2";

		public const string MARS_PLATFORM_MSG_NET_ERROR = "3";

		public const string MARS_PLATFORM_MSG_BIND_SUCCESS = "4";

		public const string MARS_PLATFORM_MSG_MODIFY_SUCCESS = "5";

		public const string MARS_PLATFORM_MSG_PASSWORD_HAD_BEEN_MODIFIED = "6";

		public const string MARS_PLATFORM_MSG_ADD_PROTOCOL = "9";

		public const string MARS_PLATFORM_MSG_CLEAR_USERINFO = "10";

		public const string MARS_PLATFORM_MSG_VOICE_MESSAGE_UPLOAD_RESULT = "11";

		public const string MARS_PLATFORM_MSG_VOICE_PLAY_COMPLETE = "12";

		public const string MARS_PLATFORM_MSG_VOICE_PLAY_ERROR = "13";

		public const string MARS_PLATFORM_MSG_LOGIN_VIEW_CLOSE = "14";

		public const string MARS_PLATFORM_MSG_USER_CANCEL_VOICE_PERMISSION = "15";

		public const string MARS_PLATFORM_MSG_VOICE_PERMISSION_DENY = "16";

		public const string MARS_PLATFORM_MSG_IMAGE_PERSONAL_UPLOAD_RESULT = "17";

		public const string MARS_PLATFORM_MSG_IMAGE_MESSAGE_UPLOAD_RESULT = "18";

		public const string MARS_PLATFORM_MSG_USER_RULE_NEED_UPDATE = "19";

		public const string MARS_PLATFORM_MSG_GET_UJ_WEB_URL = "20";

		public const string MARS_PLATFORM_MSG_UNBIND_SUCCESS = "21";

		public const string MARS_PLATFORM_MSG_GET_SETTINGS_COMPLETE = "22";

		public const string MARS_PLATFORM_MSG_NEED_RELOGIN = "23";

		public const string MARS_PLATFORM_MSG_VOICE_PERSONAL_UPLOAD_RESULT = "24";

		public const string MARS_PLATFORM_MSG_VOICE_RECORD_COMPLETE = "25";

		public const string MARS_PLATFORM_MSG_REPLY_USER_RULE_INFO = "26";

		public const string MARS_PLATFORM_MSG_SYNC_PGS_DATA_RESULT = "27";

		public const string MARS_PLATFORM_MSG_INIT_MSDK_COMPLETED = "31";

		public const string MARS_PLATFORM_MSG_INIT_MSDK_FAILED = "32";

		public const string MARS_PLATFORM_MSG_ACCOUNT_DELETED = "28";

		public const string MARS_PLATFORM_MSG_REQUEST_DELETE_ACCOUNT_SUCCESS = "29";

		public const string MARS_PLATFORM_MSG_REQUEST_DELETE_ACCOUNT_FAILURE = "30";

		public const string MARS_PLATFORM_MSG_REQUEST_RESTORE_ACCOUNT_SUCCESS = "33";

		public const string MARS_PLATFORM_MSG_REQUEST_RESTORE_ACCOUNT_FAILURE = "34";

		public const string MARS_PLATFORM_MSG_ACCOUNT_HESITATION_DELETION_PERIOD = "36";

		public const string MARS_PLATFORM_MSG_SHOW_MESSAGE = "35";

		public const string MARS_PLATFORM_MSG_EXTENSION_NOT_AVAILAVLE = "37";

		private static MarsPlatform mInstance;

		public static dMsgProcess doMsgProcessDelegateSuccess;

		public static dMsgProcess doMsgProcessDelegateError;

		public static dMsgProcess doMsgProcessDelegateTest;

		public static dMsgProcess doMsgProcessDelegateNetError;

		public static dMsgProcess doMsgProcessDelegateBindSuccess;

		public static dMsgProcess doMsgProcessDelegateModifySuccess;

		public static dMsgProcess doMsgProcessDelegatePasswordHadBeenModified;

		public static dMsgProcess doMsgProcessDelegateClearUserInfo;

		public static dMsgProcess doMsgProcessVoiceMessageUploadSuccess;

		public static dMsgProcess doMsgProcessVoiceMessageUploadFail;

		public static dMsgProcess doMsgProcessVoicePersonalUploadSuccess;

		public static dMsgProcess doMsgProcessVoicePersonalUploadFail;

		public static dMsgProcess doMsgProcessVoiceRecordComplete;

		public static dMsgProcess doMsgProcessVoicePlayComplete;

		public static dMsgProcess doMsgProcessVoicePlayError;

		public static dMsgProcess doMsgProcessGetUJWebURL;

		public static dMsgProcess doMsgProcessLoginViewClose;

		public static dMsgProcess doMsgProcessUserCancelVoicePermission;

		public static dMsgProcess doMsgProcessVoicePermissionDeny;

		public static dMsgProcess doMsgProcessImagePersonalUploadSuccess;

		public static dMsgProcess doMsgProcessImagePersonalUploadFail;

		public static dMsgProcess doMsgProcessImageMessageUploadSuccess;

		public static dMsgProcess doMsgProcessImageMessageUploadFail;

		public static dMsgProcess doMsgProcessUserRuleNeedsUpdate;

		public static dMsgProcess doMsgProcessDelegateUnBindSuccess;

		public static dMsgProcess doMsgProcessDelegateGetSettingComplete;

		public static dMsgProcess doMsgProcessDelegateNeedRelogin;

		public static dMsgProcess doMsgProcessReplyUserRuleInfo;

		public static dMsgProcess doMsgProcessSyncPGSDataResult;

		public static dMsgProcess doMsgProcessAccountDeleted;

		public static dMsgProcess doMsgProcessRequestDeleteAccountSuccess;

		public static dMsgProcess doMsgProcessRequestDeleteAccountFailure;

		public static dMsgProcess doMsgProcessRequestRestoreAccountSuccess;

		public static dMsgProcess doMsgProcessRequestRestoreAccountFailure;

		public static dMsgProcess doMsgProcessAccountHesitationDeletionPeriod;

		public static dMsgProcess doMsgProcessInitMSDKCompleted;

		public static dMsgProcess doMsgProcessInitMSDKFailed;

		public static dMsgProcess doMsgProcessAdditionalProtocol;

		public static dMsgProcess doMsgProcessShowMessage;

		public static dMsgExtensionProcess doMsgProcessExtensionNotAvailable;

		private static AndroidJavaClass mJc;

		public MarsPlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static MarsPlatform Instance()
		{ return default; }

		private static void MsgProcessTest(string[] args)
		{ }

		private static void MsgProcessSuccess(string[] args)
		{ }

		private static void MsgProcessError(string[] args)
		{ }

		private static void MsgProcessNetError(string[] args)
		{ }

		private static void MsgProcessDelegateBindSuccess(string[] args)
		{ }

		private static void MsgProcessDelegateModifySuccess(string[] args)
		{ }

		private static void MsgProcessDelegatePasswordHadBeenModified(string[] args)
		{ }

		private static void MsgProcessAdditionalProtocol(string[] args)
		{ }

		private static void MsgProcessGetUJWebURL(string[] args)
		{ }

		private static void MsgProcessClearUserInfo(string[] args)
		{ }

		private static void MsgProcessVoiceMessageUploadResult(string[] args)
		{ }

		private static void MsgProcessVoicePersonalUploadResult(string[] args)
		{ }

		private static void MsgProcessVoiceRecordComplete(string[] args)
		{ }

		private static void MsgProcessVoicePlayComplete(string[] args)
		{ }

		private static void MsgProcessVoicePlayError(string[] args)
		{ }

		private static void MsgProcessLoginViewClose(string[] args)
		{ }

		private static void MsgProcessUserCancelVoicePermission(string[] args)
		{ }

		private static void MsgProcessVoicePermissionDeny(string[] args)
		{ }

		private static void MsgProcessImagePersonalUploadResult(string[] args)
		{ }

		private static void MsgProcessImageMessageUploadResult(string[] args)
		{ }

		private static void MsgProcessUserRuleNeedsUpdate(string[] args)
		{ }

		private static void MsgProcessDelegateUnBindSuccess(string[] args)
		{ }

		private static void MsgProcessDelegateGetSettingComplete(string[] args)
		{ }

		private static void MsgProcessDelegateNeedRelogin(string[] args)
		{ }

		private static void MsgProcessReplyUserRuleInfo(string[] args)
		{ }

		private static void MsgProcessSyncPGSDataResult(string[] args)
		{ }

		private static void MsgProcessAccountDeleted(string[] args)
		{ }

		private static void MsgProcessRequestDeleteAccountSuccess(string[] args)
		{ }

		private static void MsgProcessRequestDeleteAccountFailure(string[] args)
		{ }

		private static void MsgProcessRequestRestoreAccountSuccess(string[] args)
		{ }

		private static void MsgProcessRequestRestoreAccountFailure(string[] args)
		{ }

		private static void MsgProcessAccountHesitationDeletionPeriod(string[] args)
		{ }

		private static void MsgProcessInitMSDKCompleted(string[] args)
		{ }

		private static void MsgProcessInitMSDKFailed(string[] args)
		{ }

		private static void MsgProcessShowMessage(string[] args)
		{ }

		private void MsgProcessExtensionNotAvailable(string[] args)
		{ }

		public static AndroidJavaClass GetMarsClass()
		{ return default; }

		public static void WebBillingTest_SendTradeData()
		{ }

		public void AdditionalMarsProtocol(int addition_cmd, string[] args)
		{ }

		public void AdditionalGameProtocol(int addition_cmd, string[] args)
		{ }

		public void AdditionalApiProtocol(int addition_cmd, string[] args)
		{ }

		public static void OpenLoginAccountPanel()
		{ }

		public static void OpenUserCenterPanel()
		{ }

		public static void OpenPrivacyPanel()
		{ }

		public static void OpenReadmePanel()
		{ }

		public static void OpenURL(string url, bool useTitleBar, string title)
		{ }

		public static void OpenURLWithFullScreen(string url, bool useTitleBar, string title)
		{ }

		public static void RecordStart_SyncLock(int sec, bool autoUpload = true)
		{ }

		public static void RecordStart_SyncLock(bool autoUpload = true)
		{ }

		public static void RecordPersonalVoiceStart_SyncLock(string serverID, string characterID, int voiceKey, int sec, bool autoUpload = true)
		{ }

		public static void RecordPersonalVoiceStart_SyncLock(string serverID, string characterID, int voiceKey, bool autoUpload = true)
		{ }

		public static void RecordStop_SyncLock()
		{ }

		public static void RecordCancel_SyncLock()
		{ }

		public static void PlayTemporaryVoice()
		{ }

		public static void StopTemporaryVoice()
		{ }

		public static bool IsPlayingTemporaryVoice()
		{ return default; }

		public static void UploadTemporaryVoice()
		{ }

		public static void GiveUpTemporaryVoice()
		{ }

		public static void AdjustVolumeShowUI()
		{ }

		public static void CloseVolumeUI()
		{ }

		public static void DownloadAndPlayFile(string downloadKey)
		{ }

		public static void StopPlayingFile(string downloadKey)
		{ }

		public static void StopAllPlayingFiles()
		{ }

		public static bool IsPlayingFile(string downloadKey)
		{ return default; }

		public static void SetEnableMultiVoicePlayer(bool enabled)
		{ }

		public static void EnableImageWaitProgress(bool enableMessage, bool enablePersonal)
		{ }

		public static void EnableVoiceWaitProgress(bool enableUpload, bool enableDownload)
		{ }

		public static void UseSystemData(bool enable)
		{ }

		public static void SetValue_SystemSetting(string key, string value)
		{ }

		public static string GetValue_SystemSetting(string key)
		{ return default; }

		public static int GetMAXVolume()
		{ return default; }

		public static int GetVolume()
		{ return default; }

		public static void SetVolume(int v)
		{ }

		public static void SetVoiceVolume(int v)
		{ }

		public static void OpenModifyPasswordPanel()
		{ }

		public static void OpenLoginPasswordPanel()
		{ }

		public static void OpenTelephoneVertifyPanel()
		{ }

		public static void OpenMailVertifyPanel()
		{ }

		public static void OpenPlatformBindingPanel()
		{ }

		public static void OpenFanPagePanel()
		{ }

		public static void OpenNewsPanel()
		{ }

		public static void OpenFaqPanel()
		{ }

		public static void ModifyPassword(string newpass, string confirmnewpass)
		{ }

		public static void UploadHeadIcon(string serverID, string characterID, byte[] imageData)
		{ }

		public static void UploadRecentImage(string serverID, string characterID, byte[] imageData)
		{ }

		public static void UploadMessageImage(byte[] largeImage, byte[] smallImage)
		{ }

		public string GetEnvironment()
		{ return default; }

		public static void RequestUserRuleInfo()
		{ }

		public static bool IsUserAdvertisingIdEnabled()
		{ return default; }

		public static string GetAdvertisingId()
		{ return default; }

		public static void ResetAdvertisingId()
		{ }

		public static void SyncPGSData()
		{ }
	}
}
