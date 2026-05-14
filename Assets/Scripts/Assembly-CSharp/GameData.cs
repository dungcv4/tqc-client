// Source: Ghidra work/06_ghidra/decompiled_full/GameData/ — singleton holding Lua design-table metadata.

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
		public LuaDesignDataDefine() { }
	}

	protected static GameData _instance;

	[NoToLua]
	public List<LuaDesignDataDefine> _LuaDesignTables;

	public static GameData Instance
	{
		get
		{
			if (_instance == null) _instance = new GameData();
			return _instance;
		}
	}

	public GameData()
	{
		_LuaDesignTables = new List<LuaDesignDataDefine>();
	}

	public void OnInit()
	{
		// Subclasses (e.g., GameDataMgr) register design tables here.
	}

	[NoToLua]
	protected void RegistLuaDesignData(string filename, string nodeTag, bool init = true, string[] arrayTags = null, string srcName = "", Dictionary<string, string> DefaultTagValue = null, string[] onlyTags = null, string[] ignoreTags = null)
	{
		_LuaDesignTables.Add(new LuaDesignDataDefine
		{
			fileName = filename,
			nodeTag = nodeTag,
			srcName = srcName,
			arrayTags = arrayTags,
			DefaultTagValue = DefaultTagValue,
			onlyTags = onlyTags,
			ignoreTags = ignoreTags,
			bInit = init,
		});
	}

	private static void SwitchDesignDataFromXMLToLuaFile()
	{
		// Legacy migration helper — no-op in current build.
	}

	public static string CheckStringFile(string fileName)
	{
		return fileName;
	}
}
