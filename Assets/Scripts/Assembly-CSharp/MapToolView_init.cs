using Cpp2IlInjected;

public class MapToolView_init
{
	private delegate void warningCallback(bool isConfirmed);

	private enum WndState
	{
		select = 0,
		create = 1,
		load = 2,
		warnning = 3,
		mapInfoEdit = 4,
		init = 5
	}

	private string strMapID;

	private string strSceneName;

	private string strMapWrdLevel;

	private string strMapBinLevel;

	private string strMapWidth;

	private string strMapHeight;

	private string warningMsg;

	private warningCallback warningCB;

	private static MapToolView_init instance;

	private WndState _curState;

	public static MapToolView_init Instance
	{
		get
		{ return default; }
		set
		{ }
	}

	public void onViewUpdate()
	{ }

	public void windowHandle(int wndID)
	{ }

	private void onConfirmCreate(bool isConfirmed)
	{ }

	private void onConfirmLoad(bool isConfirmed)
	{ }

	public MapToolView_init()
	{ }
}
