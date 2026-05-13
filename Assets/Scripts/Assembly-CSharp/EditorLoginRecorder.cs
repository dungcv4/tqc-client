using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

public class EditorLoginRecorder : MonoBehaviour
{
	private class AccountData
	{
		public string account;

		public string passwd;

		public string alias;

		public bool showEditAliasField;

		public AccountData(string account)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public string GetDisplayName()
		{ return default; }
	}

	private const string EDITOR_LOGIN_PREF_KEY = "EditorLoginAccount";

	private static string _accountSaveString;

	private static string _cmdAccountSaveString;

	private static List<string> _saveStringList;

	private static List<string> _cmdSaveStringList;

	private static List<string> _accountIDList;

	private static List<string> _cmdAccountIDList;

	private static bool _showUI;

	private static List<AccountData> _accountDataList;

	private static List<AccountData> _cmdAccountDataList;

	private static Vector2 _scrollPos;

	private static Vector2 _cmdScrollPos;

	private void Start()
	{ }

	private static void InitAccountList()
	{ }

	private static void InitCommandAccountList()
	{ }

	public static void Add(string account)
	{ }

	public static void UpdateAccount(string account, string passwd, string alias = "")
	{ }

	public static void UpdateCommandAccount(string account, string alias)
	{ }

	public static void Remove(string account)
	{ }

	public static void RemoveCommandAccount(string account)
	{ }

	private static void FixDuplicate()
	{ }

	public EditorLoginRecorder()
	{ }
}
