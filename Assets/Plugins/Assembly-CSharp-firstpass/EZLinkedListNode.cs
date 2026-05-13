using Cpp2IlInjected;

public class EZLinkedListNode<T> : IEZLinkedListItem<EZLinkedListNode<T>>
{
	public T val;

	private EZLinkedListNode<T> m_prev;

	private EZLinkedListNode<T> m_next;

	public EZLinkedListNode<T> prev
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public EZLinkedListNode<T> next
	{
		get
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
		set
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}

	public EZLinkedListNode(T v)
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
