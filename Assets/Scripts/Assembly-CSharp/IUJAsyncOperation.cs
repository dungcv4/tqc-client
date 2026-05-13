public interface IUJAsyncOperation
{
	bool isDone { get; }

	float progress { get; }

	string error { get; }
}
