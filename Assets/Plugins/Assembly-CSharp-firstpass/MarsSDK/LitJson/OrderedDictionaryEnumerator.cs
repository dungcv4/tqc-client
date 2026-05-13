using System.Collections;
using System.Collections.Generic;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;

		public object Current
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public DictionaryEntry Entry
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public object Key
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public object Value
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool MoveNext()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Reset()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
