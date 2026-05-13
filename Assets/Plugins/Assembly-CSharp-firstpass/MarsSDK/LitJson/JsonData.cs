using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	public class JsonData : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary, IEquatable<JsonData>
	{
		private IList<JsonData> inst_array;

		private bool inst_boolean;

		private double inst_double;

		private int inst_int;

		private long inst_long;

		private IDictionary<string, JsonData> inst_object;

		private string inst_string;

		private string json;

		private JsonType type;

		private IList<KeyValuePair<string, JsonData>> object_list;

		public int Count
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

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

		public ICollection<string> Keys
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

		bool IJsonWrapper.IsArray
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsBoolean
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsDouble
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsInt
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsLong
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsObject
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsString
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		bool IJsonWrapper.IsNone
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

		public JsonData this[string prop_name]
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

		public JsonData this[int index]
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

		public bool Contains(string key)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool Contains(int key)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData(bool boolean)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData(double number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData(int number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData(long number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData(object obj)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonData(string str)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static implicit operator JsonData(bool data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static implicit operator JsonData(double data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static implicit operator JsonData(int data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static implicit operator JsonData(long data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static implicit operator JsonData(string data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static explicit operator bool(JsonData data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static explicit operator double(JsonData data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static explicit operator int(JsonData data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static explicit operator long(JsonData data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static explicit operator string(JsonData data)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void ICollection.CopyTo(Array array, int index)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IDictionary.Add(object key, object value)
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

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IDictionary.Remove(object key)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		bool IJsonWrapper.GetBoolean()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		double IJsonWrapper.GetDouble()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		int IJsonWrapper.GetInt()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		long IJsonWrapper.GetLong()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		string IJsonWrapper.GetString()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IJsonWrapper.SetBoolean(bool val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IJsonWrapper.SetDouble(double val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IJsonWrapper.SetInt(int val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IJsonWrapper.SetLong(long val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IJsonWrapper.SetString(string val)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		string IJsonWrapper.ToJson()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IJsonWrapper.ToJson(JsonWriter writer)
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

		void IList.Insert(int index, object value)
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

		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		void IOrderedDictionary.RemoveAt(int idx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private ICollection EnsureCollection()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private IDictionary EnsureDictionary()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private IList EnsureList()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private JsonData ToJsonData(object obj)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public int Add(object value)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Clear()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool Equals(JsonData x)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonType GetJsonType()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void SetJsonType(JsonType type)
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

		public override string ToString()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
