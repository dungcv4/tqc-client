using System.Collections.Generic;
using Cpp2IlInjected;
using LuaInterface;

public class GameData
{
	public class LuaDesignDataDefine
	{
		public string fileName;

		public string nodeTag;

		public string srcName;

		public string[] arrayTags;

		public Dictionary<string, string> DefaultTagValue;

		public string[] onlyTags;

		public string[] ignoreTags;

		public bool bInit;

		public LuaDesignDataDefine()
		{ }
	}

	protected static GameData _instance;

	[NoToLua]
	public List<LuaDesignDataDefine> _LuaDesignTables;

	public static GameData Instance
	{
		get
		{ return default; }
	}

	public GameData()
	{ }

	public void OnInit()
	{ }

	[NoToLua]
	protected void RegistLuaDesignData(string filename, string nodeTag, bool init = true, string[] arrayTags = null, string srcName = "", Dictionary<string, string> DefaultTagValue = null, string[] onlyTags = null, string[] ignoreTags = null)
	{ }

	private static void SwitchDesignDataFromXMLToLuaFile()
	{ }

	public static string CheckStringFile(string fileName)
	{ return default; }
}
