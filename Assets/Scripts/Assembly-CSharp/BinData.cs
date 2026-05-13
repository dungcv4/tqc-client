using System.Collections.Generic;
using Cpp2IlInjected;

public class BinData
{
	private int _levelID;

	private tageventHEADER _headerClass;

	private tageventDATA[] _dataAry;

	public tageventHEADER headerClass
	{
		get
		{ return default; }
	}

	public tageventDATA[] dataAry
	{
		get
		{ return default; }
	}

	public int levelID
	{
		get
		{ return default; }
	}

	public BinData(int levelID)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public BinData(int levelID, List<tageventDATA> eventList)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}

	public bool loadFile()
	{ return default; }
}
