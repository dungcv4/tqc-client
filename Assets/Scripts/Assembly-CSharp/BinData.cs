using System.Collections.Generic;
using Cpp2IlInjected;

// Source: dump.cs TypeDefIndex 712. Field layout @ offsets 0x10/0x18/0x20.
public class BinData
{
	// dump.cs: private int _levelID; // 0x10
	private int _levelID;

	// dump.cs: private tageventHEADER _headerClass; // 0x18
	private tageventHEADER _headerClass;

	// dump.cs: private tageventDATA[] _dataAry; // 0x20
	private tageventDATA[] _dataAry;

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinData__get_headerClass.c RVA 0x18D1714
	// Plain field read of _headerClass@0x18.
	public tageventHEADER headerClass
	{
		get { return _headerClass; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinData__get_dataAry.c RVA 0x18D171C
	// Plain field read of _dataAry@0x20.
	public tageventDATA[] dataAry
	{
		get { return _dataAry; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinData__get_levelID.c RVA 0x18D1724
	// Plain field read of _levelID@0x10.
	public int levelID
	{
		get { return _levelID; }
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinData___ctor_int.c RVA 0x18D012C
	// 1. _levelID = levelID
	// 2. _headerClass = new tageventHEADER()
	// 3. _headerClass.eveTotalNumber = 0  (write @ 0x18 within tageventHEADER, dump.cs offset 0x18)
	// 4. _dataAry = new tageventDATA[0]   (FUN_015cb754 = il2cpp_array_new with size 0)
	// Note: dump.cs tageventHEADER offsets: eveTotalNumber=0x18, eveCombineTotal=0x1C, eveCombineOffset=0x20.
	//       Ghidra writes to (header + 0x18) which == eveTotalNumber.
	public BinData(int levelID)
	{
		_levelID = levelID;
		_headerClass = new tageventHEADER();
		if (_headerClass == null) throw new System.NullReferenceException();
		_headerClass.eveTotalNumber = 0;
		_dataAry = new tageventDATA[0];
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinData___ctor_int_list.c RVA 0x18CFF28
	// 1. _levelID = levelID
	// 2. _headerClass = new tageventHEADER()
	// 3. count = eventList.Count (read @ 0x18 of List<tageventDATA>)
	// 4. _headerClass.eveCombineTotal = 0  (write @ 0x1C)
	//    _headerClass.eveCombineOffset = 0 (write @ 0x20)
	//    _headerClass.eveTotalNumber = count (write @ 0x18)
	// 5. _dataAry = new tageventDATA[eventList.Count]
	// 6. For i in [0, count): _dataAry[i] = eventList[i]
	//    Ghidra uses List<object>.get_Item then type-check (thunk_FUN_01560118 = il2cpp_class_is_assignable_from)
	//    to verify the element is assignable to tageventDATA; throws InvalidCastException on mismatch.
	public BinData(int levelID, List<tageventDATA> eventList)
	{
		_levelID = levelID;
		_headerClass = new tageventHEADER();
		if (eventList == null) throw new System.NullReferenceException();
		if (_headerClass == null) throw new System.NullReferenceException();
		int count = eventList.Count;
		_headerClass.eveCombineTotal = 0;
		_headerClass.eveCombineOffset = 0;
		_headerClass.eveTotalNumber = count;
		_dataAry = new tageventDATA[eventList.Count];
		if (eventList.Count > 0)
		{
			for (int i = 0; i < eventList.Count; i++)
			{
				tageventDATA item = eventList[i];
				if (_dataAry == null) throw new System.NullReferenceException();
				_dataAry[i] = item;
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/BinData__loadFile.c RVA 0x18D02D0
	// 1. _headerClass = new tageventHEADER()
	// 2. _dataAry = BinFileTool.readFile(_levelID, _headerClass)
	// 3. If _dataAry == null:
	//      _levelID = -1
	//      _headerClass = new tageventHEADER()  (reset to a fresh empty header)
	// 4. Return _dataAry != null.
	public bool loadFile()
	{
		_headerClass = new tageventHEADER();
		_dataAry = BinFileTool.readFile(_levelID, _headerClass);
		if (_dataAry == null)
		{
			_levelID = -1;
			_headerClass = new tageventHEADER();
		}
		return _dataAry != null;
	}
}
