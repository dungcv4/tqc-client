using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using MarsSDK.Notification;
using MarsSDK.Platform;
using UnityEngine;

namespace MarsSDK.Demo
{
	public class Demo : MonoBehaviour
	{
		private enum ApiMode
		{
			Mars = 0,
			UserCenter = 1,
			ThridParty = 2,
			ExtensionPlugins = 3,
			Other = 9
		}

		private enum Mode_Mars
		{
			Default = 0,
			Billing = 1,
			WebBilling = 2,
			NativeUI = 3,
			MobileMailLogin = 4,
			TelephoneLogin = 5,
			StandAloneWeb = 6
		}

		private enum Mode_UserCenter
		{
			Default = 0,
			TelephoneVerify = 1,
			MailVerify = 2,
			MoJoy = 3,
			GiftCode = 4
		}

		private enum Mode_ThirdParty
		{
			CrossPlatformSignIn = 0,
			Facebook = 1,
			Twitter = 2,
			Instagram = 3,
			PlatformNative = 4,
			DMM = 5,
			Erolabs = 6,
			DmmAppStore = 7,
			Steam = 8
		}

		private enum Mode_Other
		{
			VoiceRecord = 1,
			SelectPhoto = 14,
			Notification = 17,
			System = 18
		}

		private enum Mode_ExtensionPlugins
		{
			Default = 0,
			UJAdMob = 1
		}

		private enum ActionAfterLogin
		{
			ACTION_NONE = 0,
			ACTION_BINDFACEBOOK = 1,
			ACTION_UNBINDFACEBOOK = 2,
			ACTION_BINDMOBILEMAIL = 3,
			ACTION_UNBINDMOBILEMAIL = 4,
			ACTION_MODIFY_MOBILEMAIL_PASSWORD = 5,
			ACTION_ASK_MODIFY_MOBILEMAIL_PASSWORD = 6,
			ACTION_MODIFY_QUICKACCOUNT_PASSWORD = 7,
			ACTION_BINDNATIVEPLATFORM = 8,
			ACTION_UNBINDNATIVEPLATFORM = 9,
			ACTION_RESETACCOUNT = 10
		}

		[CompilerGenerated]
		private sealed class _003CSelectPhotoCoroutine_003Ed__325 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float quality;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{ return default; }
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{ return default; }
			}

			[DebuggerHidden]
			public _003CSelectPhotoCoroutine_003Ed__325(int _003C_003E1__state)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			private bool MoveNext()
			{ return default; }

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		[CompilerGenerated]
		private sealed class _003CStart_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Demo _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{ return default; }
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{ return default; }
			}

			[DebuggerHidden]
			public _003CStart_003Ed__60(int _003C_003E1__state)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			private bool MoveNext()
			{ return default; }

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		private static ActionAfterLogin _afterLoginAction;

		private static bool _enable_editorDialog;

		private static string _status;

		private static string _result;

		private static ApiMode _ApiMode;

		private static Mode_Mars _ApiMars;

		private static Mode_ThirdParty _ApiThirdParty;

		private static Mode_UserCenter _ApiUserCenter;

		private static Mode_Other _ApiOther;

		private static Mode_ExtensionPlugins _extension;

		private static string[] _ApiModeTitle;

		private static bool _forceHideGUILayout;

		private static bool _isPressEsc;

		private static int _Mars;

		private static int _Facebook;

		private static string _nickname;

		private static string _platform;

		private static string _playerId;

		private static string _password;

		private static bool _billingInitResult;

		private static string _bindPlatformData;

		private Dictionary<string, string> _tmpCache;

		private GUISkin _gui_skin;

		private Vector2 _gui_scrollPosition;

		private int _gui_area_width;

		private int textWidth;

		private Rect _gui_rc;

		private GUILayoutOption[] _btnGUILayoutOption;

		private GUILayoutOption[] _textGUILayoutOption;

		private GUILayoutOption[] _textGUILayoutNearTextOption;

		public GameObject hintPanel;

		public GameObject cancelableHintPanel;

		public GameObject confirmOrCancelPanel;

		public Canvas mainCanvas;

		public GameObject IngameDebugConsole;

		public static string ServiceURL_Text;

		private string _mars_demo_env;

		private static bool _initSuccess;

		private static bool _settingExist;

		private bool isShowingDebugPanel;

		private static string _appLink;

		private static string _appImg;

		private static string _appPicPath;

		private static string _base64TestImg;

		private static string _loginAccessToken;

		private int _fb_ui_title_width;

		private float _fb_ui_value_width;

		private int _giftcode_ui_title_width;

		private float _giftcode_ui_value_width;

		private static string _giftCode;

		private string emailAddress;

		private string emailVerifyCode;

		private string emailInfoMsg;

		private int _mailVer_ui_title_width;

		private float _mailVer_ui_value_width;

		public static bool use_default_native_login;

		public static bool isNative;

		private static string usernameLogin_textField;

		private static string password_textField;

		private static string new_password_textField;

		private string ujweb_url_textField;

		private string ujweb_setid_textField;

		private string ujweb_nickname_textField;

		private static string sns_token_textField;

		private static int sns_token_type;

		private static string designId_textFidle;

		private static string mars_token;

		private static string _serverId;

		private static string _characterId;

		private static string _characterName;

		private int _curMarslangIndex;

		private readonly string[] _supportLangList;

		private int _mars_ui_title_width;

		private float _mars_ui_value_width;

		private GameObject _embeddedBrowserObject;

		private static string emailLogin_textField;

		private static string emailpassword_textField;

		private static string new_emailpassword_textField;

		private int _mail_ui_title_width;

		private float _mail_ui_value_width;

		private string mojoyMailAddress;

		private string mojoyMailVerifyCode;

		private string mojoyMailInfoMsg;

		private int _mojoy_ui_title_width;

		private float _mojoy_ui_value_width;

		private bool notificationIsInit;

		private NotificationManager notificationManager;

		private string nickname_textField;

		private string key_textField;

		private string value_textField;

		private string redirectno_textField;

		private string aaid_textField;

		private int _other_ui_title_width;

		private float _other_ui_value_width;

		public static Texture2D photoTexture_selected;

		public static Texture2D photoTexture_downloaded;

		private static string _photo_downloadURL;

		private float imageQuality;

		private static int _width_selected_pic;

		private static int _height_selected_pic;

		private static byte[] _tempPic;

		private bool showPopup;

		private Rect popupWindowRect;

		private bool showTakePhotoPopup;

		private Rect popupTakePhotoWindowRect;

		private static string _siwaWebToken;

		private static string _siwaWebInfoString;

		private int _siwa_ui_title_width;

		private float _siwa_ui_value_width;

		private string telephoneNumber;

		private string verifyCode;

		private string passCode;

		private string infoString;

		private int _tel_ui_title_width;

		private float _tel_ui_value_width;

		private static string _twitterVerifier;

		private static string _twitterInfoString;

		private int _twitter_ui_title_width;

		private float _twitter_ui_value_width;

		private string volume;

		private string volumeLabel;

		private static string downloadKey;

		private static string voiceKey;

		private static bool multiVoice;

		private int _voice_ui_title_width;

		private float _voice_ui_value_width;

		private static int MAX_PERSONAL_VOICE;

		private static Dictionary<int, string> PersonalVoiceList;

		private static string selectedIndex;

		private static bool enableVoiceDownloadProgress;

		private static string webbilling_DesignId_textField;

		private static string webbilling_ItemType_textField;

		public static string status
		{
			get
			{ return default; }
			set
			{ }
		}

		public static string result
		{
			get
			{ return default; }
			set
			{ }
		}

		public static bool IsSDKInited
		{
			get
			{ return default; }
			set
			{ }
		}

		public static bool IsSettingExist
		{
			get
			{ return default; }
			set
			{ }
		}

		public static int FitScale(float size)
		{ return default; }

		private void Awake()
		{ }

		[IteratorStateMachine(typeof(_003CStart_003Ed__60))]
		private IEnumerator Start()
		{ return default; }

		private void Update()
		{ }

		private void OnApplicationFocus(bool hasFocus)
		{ }

		private void OnApplicationPause(bool pauseStatus)
		{ }

		private bool HideGUILayout()
		{ return default; }

		private void ClearInfoUI()
		{ }

		private void initMSDKGUIStyle()
		{ }

		private void OnGUI()
		{ }

		public void CreateEnviromentChooser()
		{ }

		private bool IsApiMode(ApiMode mode)
		{ return default; }

		private bool IsApiMenu(Mode_Mars mode)
		{ return default; }

		private bool IsApiMenu(Mode_UserCenter mode)
		{ return default; }

		private bool IsApiMenu(Mode_ThirdParty mode)
		{ return default; }

		private bool IsApiMenu(Mode_Other mode)
		{ return default; }

		private bool IsApiMenu(Mode_ExtensionPlugins mode)
		{ return default; }

		private void _Independent_toggled_ApiMode(ApiMode mode)
		{ }

		private void _Independent_toggled_ApiMenu(Mode_Mars mode)
		{ }

		private void _Independent_toggled_ApiMenu(Mode_UserCenter mode)
		{ }

		private void _Independent_toggled_ApiMenu(Mode_Other mode)
		{ }

		private void _Independent_toggled_ApiMenu(Mode_ExtensionPlugins mode)
		{ }

		private void _Independent_toggled_ApiMenu(Mode_ThirdParty mode)
		{ }

		private bool IsModuleRegister(Mode_ThirdParty mode)
		{ return default; }

		private bool IsModuleRegister(Mode_ExtensionPlugins mode)
		{ return default; }

		private void checkRequireLogined(Action act, string errorMsg)
		{ }

		private void NativeOnly(Action act, string errorMsg)
		{ }

		private void StandAloneOnly(Action act, string errorMsg)
		{ }

		public void DemoBilling()
		{ }

		public void DemoCharacter()
		{ }

		public string GetCharacterStatusString()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> CustomCharacterGUI()
		{ return default; }

		public void DemoCrossPlatformSignIn()
		{ }

		public void DemoFacebook()
		{ }

		public void DemoGiftCode()
		{ }

		public void DemoInstagram()
		{ }

		public void EmailVerifyDemo()
		{ }

		public Dictionary<string, Dictionary<string, Action>> CustomMailVerifyGUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> CustomMailSendVerifyGUI()
		{ return default; }

		public void CreateEnvironmentChooser()
		{ }

		public void DemoMars()
		{ }

		public Dictionary<string, Dictionary<string, Action>> CustomPlayIdPasswordGUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> CustomModifyPasswordGUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> CustomDeleteAccountGUI()
		{ return default; }

		public void initMarsPlatform()
		{ }

		private void doEventSetDataResult(int status, string[] args)
		{ }

		public static void doMsgProcessSuccess(string[] args)
		{ }

		public static void doMsgProcessError(string[] args)
		{ }

		public static void doMsgProcessNetError(string[] args)
		{ }

		public static void doMsgProcessBindSuccess(string[] args)
		{ }

		public static void doMsgProcessUnBindSuccess(string[] args)
		{ }

		public static void doMsgProcessGetSettingComplete(string[] args)
		{ }

		public static void doMsgProcessNeedRelogin(string[] args)
		{ }

		public static void doMsgProcessReplyUserRuleInfo(string[] args)
		{ }

		public static void doMsgProcessSyncPGSDataResult(string[] args)
		{ }

		public static void doMsgProcessAccountDeleted(string[] args)
		{ }

		public static void doMsgProcessRequestDeleteAccountSuccess(string[] args)
		{ }

		public static void doMsgProcessRequestDeleteAccountFailure(string[] args)
		{ }

		public static void doMsgProcessRequestRestoreAccountSuccess(string[] args)
		{ }

		public static void doMsgProcessRequestRestoreAccountFailure(string[] args)
		{ }

		public static void doMsgProcessAccountHesitationDeletionPeriod(string[] args)
		{ }

		public static void doMsgProcessInitMSDKCompleted(string[] args)
		{ }

		public static void doMsgProcessInitMSDKFailed(string[] args)
		{ }

		public static void doMsgProcessTest(string[] args)
		{ }

		public void doMsgProcessShowMessage(string[] args)
		{ }

		public static void FacebookCallbackOnSuccess(string[] args)
		{ }

		public static void FacebookCallbackOnCancel(string[] args)
		{ }

		public static void FacebookCallbackOnError(string[] args)
		{ }

		public static void doTwitterEventLoginVerifySucceeded(string[] args)
		{ }

		public static void doTwitterEventLoginVerifyFailed(string[] args)
		{ }

		public static void doTwitterEventPostTweetSucceeded(string[] args)
		{ }

		public static void doTwitterEventPostTweetFailed(string[] args)
		{ }

		public static void InstagramCallbackOnSuccess(string[] args)
		{ }

		public static void InstagramCallbackOnCancel(string[] args)
		{ }

		public static void InstagramCallbackOnError(string[] args)
		{ }

		public static void MobileMailCallbackOnSuccess(string[] args)
		{ }

		public static void MobileMailCallbackOnError(string[] args)
		{ }

		public static void doEventInitSucceeded()
		{ }

		public static void doEventInitFailed()
		{ }

		public static void doEventQueryInventory(string status, List<UJBillingProduct> ujSkuDetails)
		{ }

		public static void doEventPurchaseSucceeded()
		{ }

		public static void doEventUserCanceled()
		{ }

		public static void doEventPurchaseFailed(string[] args)
		{ }

		public static void doEventUJOrderListCount(string[] args)
		{ }

		public static void doEventRequestGold()
		{ }

		public static void doEventDeviceNotSupportGooglePlayService()
		{ }

		public static void doEventMissGoogleAccount()
		{ }

		public static void doEventRSAKeyNotMatch()
		{ }

		public static void doEventVerifyFail(string[] args)
		{ }

		public static void doEventGooglePlayApiConnected()
		{ }

		public static void doEventGooglePlayApiConnectionSuspended()
		{ }

		public static void doEventGameCenterAuthenticateSucceeded()
		{ }

		public static void doEventGameCenterAuthenticateFailed()
		{ }

		public static void doEventGooglePlayLoginSuccess(string[] args)
		{ }

		public static void doEventGooglePlayLoginFail(string[] args)
		{ }

		public static void doEventGooglePlayLoginCancel(string[] args)
		{ }

		public static void doEventGooglePlayRevokeAccess(string[] args)
		{ }

		public static void doEventGotGPPOrderInfo(string[] args)
		{ }

		public static void doEventHandleLostGold(string[] args)
		{ }

		public void doEventPurchaseUpdated(string[] args)
		{ }

		public void doEventPurchasePending(string[] args)
		{ }

		public void doEventPlayStoreNeedUpgrate(string[] args)
		{ }

		public static void doEventLoadAchievementsDone()
		{ }

		public static void doEventRequestATTrackingAuthorized(string[] args)
		{ }

		public static void doEventRequestATTrackingDenied(string[] args)
		{ }

		public static void doUJEventUJOrderListCount(string[] args)
		{ }

		public static void doUJEventRequestGold()
		{ }

		public static void doAppleEventSIWASucceeded(string[] args)
		{ }

		public static void doAppleEventSIWAFailed(string[] args)
		{ }

		public static void doAppleEventSIWACanceled(string[] args)
		{ }

		public static void doEventPGSSupported()
		{ }

		public static void doEventGamePromotionSupported()
		{ }

		public static void doEventNetworkChanged(string status)
		{ }

		public static void doMsgProcessAdditionalProtocol(string[] args)
		{ }

		public static void doMsgProcessClearUserInfo(string[] args)
		{ }

		public static void doMsgProcessVoiceMessageUploadSuccess(string[] args)
		{ }

		public static void doMsgProcessVoiceMessageUploadFail(string[] args)
		{ }

		public static void doMsgProcessVoicePersonalUploadSuccess(string[] args)
		{ }

		public static void doMsgProcessVoicePersonalUploadFail(string[] args)
		{ }

		public static void doMsgProcessVoiceRecordComplete(string[] args)
		{ }

		public static void doMsgProcessVoicePlayComplete(string[] args)
		{ }

		public static void doMsgProcessVoicePlayError(string[] args)
		{ }

		public static void doMsgProcessLoginViewClose(string[] args)
		{ }

		public static void doMsgProcessUserCancelVoicePermission(string[] args)
		{ }

		public static void doMsgProcessVoicePermissionDeny(string[] args)
		{ }

		public static void doMsgProcessImagePersonalUploadSuccess(string[] args)
		{ }

		public static void doMsgProcessImagePersonalUploadFail(string[] args)
		{ }

		public static void doMsgProcessImageMessageUploadSuccess(string[] args)
		{ }

		public static void doMsgProcessImageMessageUploadFail(string[] args)
		{ }

		public static void doMsgProcessGetUJWebURL(string[] args)
		{ }

		public static void doMsgProcessUserRuleNeedsUpdate(string[] args)
		{ }

		public static void doMailEventHasBinded()
		{ }

		public static void doMailEventBindSucceeded()
		{ }

		public static void doMailEventBindFailed(int status)
		{ }

		public static void doMailEventRepeat()
		{ }

		public static void doMailEventVerifyCodeTimeout()
		{ }

		public static void doMailEventBindFailLock()
		{ }

		public static void doMailEventVerifyCodeReady()
		{ }

		public static void doEventHasBinded()
		{ }

		public static void doEventBindSucceeded()
		{ }

		public static void doEventBindFailed(int status)
		{ }

		public static void doMsgProcessModifySuccess(string[] args)
		{ }

		public static void doMsgProcessPasswordHadBeenModified(string[] args)
		{ }

		public static void doEventTelephoneNumberReachBindLimit()
		{ }

		public static void doEventVerifyCodeSendFailed(int platformCode)
		{ }

		public static void doEventVerifyCodeTimeout()
		{ }

		public static void doEventBindFailLock()
		{ }

		public static void doEventRequestPassSucceeded()
		{ }

		public static void doEventRequestPassFailed(int status)
		{ }

		public static void doEventVerifyCodeReady()
		{ }

		public static void doEventVerifyCodeExist()
		{ }

		public static void doEventVerifyCodeCanceled()
		{ }

		public static void doTLEventCheckIsRobot()
		{ }

		public static void doTLEventVerifyRobotCodeNotMatch()
		{ }

		public static void doTLEventTelephoneNumberFormatIncorrect()
		{ }

		public static void doTLEventSendMessageFailed(string[] args)
		{ }

		public static void doTLEventVerifyCodeReady()
		{ }

		public static void doTLEventVerifyCodeTimeout()
		{ }

		public static void doTLEventVerifyMessageFailed()
		{ }

		public static void doTLEventDailyMessageLimit()
		{ }

		public static void doTLEventAccountIsLocked()
		{ }

		public static void doTLEventAccountIsReset()
		{ }

		public static void doSelectPhotoSuccess(byte[] bytes, int width, int height)
		{ }

		public static void doSelectPhotoFailed(string errorCode)
		{ }

		public static void doSelectPhotoCancel()
		{ }

		public static void doMoJoyMailEventHasBinded()
		{ }

		public static void doMoJoyMailEventBindSucceeded()
		{ }

		public static void doMoJoyMailEventBindFailed(int status)
		{ }

		public static void doMoJoyMailEventRepeat()
		{ }

		public static void doMoJoyMailEventVerifyCodeTimeout()
		{ }

		public static void doMoJoyMailEventBindFailLock()
		{ }

		public static void doMoJoyMailEventVerifyCodeReady()
		{ }

		public static void doEventRedeemCodeResult(int status, string[] args)
		{ }

		public static void doEventGiftCheckResult(int status, string[] args)
		{ }

		public static void doEventNewGiftIsComin(string[] args)
		{ }

		public static void doEventGiftUIClose(string[] args)
		{ }

		public static void doWebServiceTelephoneVerifyReady(string[] args)
		{ }

		public static void doWebServiceMailVerifyReady(string[] args)
		{ }

		public void doWebServiceModifyPasswordReady(string[] args)
		{ }

		public void doWebServiceDeleteAccountReady(string[] args)
		{ }

		public void InitDemoMarsStandAloneWeb()
		{ }

		public void DemoMarsStandAloneWeb()
		{ }

		public void DemoMobileMailLogin()
		{ }

		public void MoJoyDemo()
		{ }

		public void DemoMSDKUI()
		{ }

		public void DemoNative()
		{ }

		public void NotificationDemo()
		{ }

		private void InitNotification()
		{ }

		public void onNotificationAllow()
		{ }

		public void onNotificationDontAllow()
		{ }

		public void onNotificationDenied()
		{ }

		public void onNotificationCancel()
		{ }

		public void onNotificationIOSStatusCallback(string status)
		{ }

		private Texture2D createReadabeTexture2D(Texture2D texture2d)
		{ return default; }

		public void DemoOther()
		{ }

		private void DoRequestPermission()
		{ }

		public void DemoSelectPhoto()
		{ }

		private void DetectClickOutsideWindow()
		{ }

		public static void SetPhotoSelected(byte[] bytes, int width, int height)
		{ }

		public static void SetPhotoToDownload(string url)
		{ }

		public Dictionary<string, Dictionary<string, Action>> CustomSelectPhotoGUI()
		{ return default; }

		[IteratorStateMachine(typeof(_003CSelectPhotoCoroutine_003Ed__325))]
		private IEnumerator SelectPhotoCoroutine(float quality)
		{ return default; }

		public void DemoSignInWithAppleWeb()
		{ }

		public void DemoTelephoneLogin()
		{ }

		public void DemoTelephoneVerify()
		{ }

		public Dictionary<string, Dictionary<string, Action>> CustomTelephoneVerifyGUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> CustomPhoneSendVerifyGUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> CustomPassVerifyGUI()
		{ return default; }

		public void DemoTwitter()
		{ }

		public void DemoUserCenter()
		{ }

		public static void CheckAndAddtoPersonalVoiceList(int voiceKey, string downLoadkey)
		{ }

		private static Dictionary<int, string> InitializeDictionary(int count)
		{ return default; }

		public static int PersonalVoiceListDownLoadkeyCount()
		{ return default; }

		public static void ShowPersonalVoiceList()
		{ }

		public static void SetVoiceDownloadKey(string key)
		{ }

		public Dictionary<string, Dictionary<string, Action>> PersionalVoiceUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> MessageVoiceUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> SetupVoiceUI()
		{ return default; }

		public Dictionary<string, Dictionary<string, Action>> PersonalVoiceListUI()
		{ return default; }

		public void DemoVoiceRecord()
		{ }

		public void DemoWebBilling()
		{ }

		public Dictionary<string, Dictionary<string, Action>> CustomWebBillingDirectGUI()
		{ return default; }

		public Demo()
		{ }

		static Demo()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
