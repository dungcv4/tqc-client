using UnityEngine;

public interface IUJObjectOperation : IUJAsyncOperation
{
	long bundleBytes { get; }

	Object[] values { get; }

	void ImmDestroy();
}
