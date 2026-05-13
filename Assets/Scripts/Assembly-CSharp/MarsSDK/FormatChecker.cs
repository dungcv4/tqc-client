using Cpp2IlInjected;

namespace MarsSDK
{
	public class FormatChecker
	{
		public const string MatchEmailPattern = "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\\w-]+\\.)+[a-zA-Z]{2,4})$";

		public const string MatchPasswordPattern = "^[a-zA-Z0-9]{6,16}$";

		public const string MatchPlayerIdPattern = "^[0-9]{9,16}$";

		public static bool IsEmail(string email)
		{ return default; }

		public static bool IsPassword(string pwd)
		{ return default; }

		public static bool IsPlayerId(string pid)
		{ return default; }

		public FormatChecker()
		{ }
	}
}
