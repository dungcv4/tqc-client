using System.IO;
using Cpp2IlInjected;
using UnityEngine;

public static class WrdFileTool
{
	public static string MapInfoMgr_DATA_PATH;

	public static void backToFile(int levelID)
	{ }

	public static void writeToFile(WrdData wrdData)
	{ }

	public static tagmapCODEDATA[] readWRDFile(int levelID, ref tagmapHEADER headerStruct)
	{ return default; }

	public static uint getServerMapHeight(int levelID)
	{ return default; }

	public static void _printHeader(tagmapHEADER header)
	{ }

	public static void _printCode(tagmapCODEDATA[] codeAry)
	{ }

	public static string getPath(int levelID, bool fullPath = false)
	{ return default; }

	public static bool checkWrdFileExist(int levelID)
	{ return default; }

	public static void printToFile(StreamWriter wr, tagmapHEADER header, tagmapCODEDATA[] dataAry)
	{ }

	private static TextAsset LoadMapDataFromBundle(string filename)
	{ return default; }

	static WrdFileTool()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
