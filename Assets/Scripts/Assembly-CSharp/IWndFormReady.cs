public interface IWndFormReady
{
	bool isReady { get; }

	bool hasEvent { get; }

	void FinishEvent(WndForm wnd);
}
