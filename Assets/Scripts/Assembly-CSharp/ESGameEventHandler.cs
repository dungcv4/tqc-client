using System;
using Cpp2IlInjected;

public class ESGameEventHandler
{
	public delegate void StringArgEvent(string[] args);

	public delegate void NoArgEvent();

	[Serializable]
	public class User
	{
		public int id;

		public string full_name;

		public string phone;

		public string provider;

		public int active;

		public string national_id;

		public string address;

		public string birthday;

		public bool is_new;

		public string email;

		public string gender;

		public string name;

		public User()
		{ }
	}

	[Serializable]
	public class LoginSuccessData
	{
		public int code;

		public string message;

		public User user;

		public LoginSuccessData()
		{ }
	}

	[Serializable]
	public class LoginFailureData
	{
		public int code;

		public string message;

		public LoginFailureData()
		{ }
	}

	[Serializable]
	public class GGBillingResultData
	{
		public int success;

		public string sku;

		public string orderID;

		public GGBillingResultData()
		{ }
	}

	[Serializable]
	public class WebBillingResultData
	{
		public string itemID;

		public int price;

		public WebBillingResultData()
		{ }
	}

	[Serializable]
	public class AppleBillingResultData
	{
		public string productID;

		public string orderID;

		public AppleBillingResultData()
		{ }
	}

	public static StringArgEvent OnLoginSuccess;

	public static StringArgEvent OnLoginFail;

	public static NoArgEvent OnLogout;

	public static StringArgEvent OnGGBillingResult;

	public static StringArgEvent OnWebBillingResult;

	public static StringArgEvent OnAppleBillingResult;

	public static StringArgEvent OnError;

	public static NoArgEvent OnViewClose;

	public static void OnESGameLoginSuccess(string args)
	{ }

	public static void OnESGameLoginFailure(string args)
	{ }

	public static void OnESGameLogout()
	{ }

	public static void OnESGameGGBillingResult(string args)
	{ }

	public static void OnESGameWebBillingResult(string args)
	{ }

	public static void OnESGameAppleBillingResult(string args)
	{ }

	public static void OnESGameViewClose()
	{ }

	public ESGameEventHandler()
	{ }
}
