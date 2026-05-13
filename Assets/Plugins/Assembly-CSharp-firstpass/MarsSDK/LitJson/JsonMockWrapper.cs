using System;
using System.Collections;
using System.Collections.Specialized;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	public class JsonMockWrapper : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary
	{
		public bool IsArray
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsBoolean
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsDouble
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsInt
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsLong
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsObject
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsString
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool IsNone
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		object IList.this[int index]
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

		int ICollection.Count
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		object IDictionary.this[object key]
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

		object IOrderedDictionary.this[int idx]
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

		public bool GetBoolean()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public double GetDouble()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public int GetInt()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonType GetJsonType()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public long GetLong()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public string GetString()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetBoolean(bool val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetDouble(double val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetInt(int val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetJsonType(JsonType type)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetLong(long val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetString(string val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public string ToJson()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void ToJson(JsonWriter writer)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		int IList.Add(object value)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IList.Clear()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		bool IList.Contains(object value)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		int IList.IndexOf(object value)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IList.Insert(int i, object v)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IList.Remove(object value)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IList.RemoveAt(int index)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void ICollection.CopyTo(Array array, int index)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IDictionary.Add(object k, object v)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IDictionary.Clear()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		bool IDictionary.Contains(object key)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IDictionary.Remove(object key)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IOrderedDictionary.Insert(int i, object k, object v)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IOrderedDictionary.RemoveAt(int i)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonMockWrapper()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
