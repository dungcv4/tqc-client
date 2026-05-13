namespace tss
{
	public interface TssInfoReceiver
	{
		void onReceive(int tssInfoType, string info);
	}
}
