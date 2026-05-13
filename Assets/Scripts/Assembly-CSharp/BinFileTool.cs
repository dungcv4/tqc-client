using System.Collections.Generic;
using System.IO;
using Cpp2IlInjected;
using UnityEngine;

public static class BinFileTool
{
	public static string MapInfoMgr_DATA_PATH;

	public static void backToFile(int levelID)
	{ }

	public static ushort readMapConnectionPoints(string fileName, out Dictionary<ushort, List<MapConnectionPoint>> dicMapConnection) { dicMapConnection = default; return default; }

	public static tageventDATA[] readFile(int levelID, tageventHEADER headerClass)
	{ return default; }

	public static void writeToFile(BinData binData)
	{ }

	public static ushort FileNameToLevelID(string sFileName)
	{ return default; }

	public static string getFileName(int levelID)
	{ return default; }

	public static string getPath(int levelID, bool fullPath = false)
	{ return default; }

	public static bool checkBinFileExist(int levelID)
	{ return default; }

	public static void printHeader(tageventHEADER header)
	{ }

	public static void printData(tageventDATA[] dataAry)
	{ }

	public static void printToFile(StreamWriter wr, tageventHEADER header, tageventDATA[] dataAry)
	{ }

	private static TextAsset LoadMapDataFromBundle(string filename)
	{ return default; }

	static BinFileTool()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
