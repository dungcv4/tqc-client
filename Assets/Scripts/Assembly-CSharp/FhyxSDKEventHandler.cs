using Cpp2IlInjected;

public class FhyxSDKEventHandler
{
	public delegate void StringArgEvent(string[] args);

	public delegate void NoArgEvent();

	public static StringArgEvent OnLoginSuccess;

	public static StringArgEvent OnLoginFail;

	public static NoArgEvent OnLogout;

	public static StringArgEvent OnSendRegisterVerificationCodeResult;

	public static StringArgEvent OnBindingPersonalIdentityResult;

	public static StringArgEvent OnGetUserBoundInfoSuccess;

	public static StringArgEvent OnGetUserBoundInfoFail;

	public static StringArgEvent OnGetUserInfoSuccess;

	public static StringArgEvent OnGetUserInfoFail;

	public static StringArgEvent OnGetLoginVerificationCodeResult;

	public static StringArgEvent OnChangePasswordResult;

	public static StringArgEvent OnResetPasswordSendVerificationCodeResult;

	public static StringArgEvent OnResetPasswordCheckVerificationCodeResult;

	public static StringArgEvent OnResetPasswordResult;

	public static StringArgEvent OnRebindPhoneSendVerificationCodeToOldResult;

	public static StringArgEvent OnRebindPhoneCheckVerificationCodeResult;

	public static StringArgEvent OnRebindPhoneSendVerificationCodeToNewResult;

	public static StringArgEvent OnRebindPhoneResult;

	public static StringArgEvent OnRegisterResult;

	public static StringArgEvent OnWebViewClose;

	public static StringArgEvent OnBindActivationCodeResult;

	public static StringArgEvent OnCheckAccountActivationResult;

	public static StringArgEvent OnSendLogResult;

	public static StringArgEvent OnPayResult;

	public static StringArgEvent OnCheckWordResult;

	public static void OnFhyxSDKInitResult(string args)
	{ }

	public static void OnFhyxSDKSendRegisterVerificationCodeResult(string args)
	{ }

	public static void OnFhyxSDKBindingPersonalIdentityResult(string args)
	{ }

	public static void OnFhyxSDKGetUserBoundInfoResult(string args)
	{ }

	public static void OnFhyxSDKLoginResult(string args)
	{ }

	public static void OnFhyxSDKGetUserInfoResult(string args)
	{ }

	public static void OnFhyxSDKGetLoginVerificationCodeResult(string args)
	{ }

	public static void OnFhyxSDKChangePasswordResult(string args)
	{ }

	public static void OnFhyxSDKResetPasswordSendVerificationCodeResult(string args)
	{ }

	public static void OnFhyxSDKResetPasswordCheckVerificationCodeResult(string args)
	{ }

	public static void OnFhyxSDKResetPasswordResult(string args)
	{ }

	public static void OnFhyxSDKRebindPhoneSendVerificationCodeToOldResult(string args)
	{ }

	public static void OnFhyxSDKRebindPhoneCheckVerificationCodeResult(string args)
	{ }

	public static void OnFhyxSDKRebindPhoneSendVerificationCodeToNewResult(string args)
	{ }

	public static void OnFhyxSDKRebindPhoneResult(string args)
	{ }

	public static void OnFhyxSDKRegisterResult(string args)
	{ }

	public static void OnFhyxSDKWebViewClose(string args)
	{ }

	public static void OnFhyxSDKBindActivationCodeResult(string args)
	{ }

	public static void OnFhyxSDKCheckAccountActivationResult(string args)
	{ }

	public static void OnFhyxSDKSendLogResult(string args)
	{ }

	public static void OnFhyxSDKPayResult(string args)
	{ }

	public static void OnFhyxSDKCheckWordResult(string args)
	{ }

	public FhyxSDKEventHandler()
	{ }
}
