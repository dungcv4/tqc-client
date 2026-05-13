using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Cpp2IlInjected;

namespace MiniJSON
{
	public static class Json
	{
		private sealed class Parser : IDisposable
		{
			private enum TOKEN
			{
				NONE = 0,
				CURLY_OPEN = 1,
				CURLY_CLOSE = 2,
				SQUARED_OPEN = 3,
				SQUARED_CLOSE = 4,
				COLON = 5,
				COMMA = 6,
				STRING = 7,
				NUMBER = 8,
				TRUE = 9,
				FALSE = 10,
				NULL = 11
			}

			private class SetPropMethod
			{
				private MethodInfo _method;

				private Type _paramType;

				private object[] _params;

				public Type type
				{
					get
					{ return default; }
				}

				public SetPropMethod(MethodInfo mi)
				{
					throw new AnalysisFailedException("No IL was generated.");
				}

				public void SetValue(ref object owner, object value)
				{ }
			}

			private const string WORD_BREAK = "{}[],:\"";

			private StringReader json;

			private Type tpIDictionary;

			private StringBuilder _strBuilder;

			private Dictionary<Type, Dictionary<string, SetPropMethod>> _classProps;

			private Type _lastClassType;

			private Dictionary<string, SetPropMethod> _lastPropDict;

			private char PeekChar
			{
				get
				{ return default; }
			}

			private char NextChar
			{
				get
				{ return default; }
			}

			private string NextWord
			{
				get
				{ return default; }
			}

			private TOKEN NextToken
			{
				get
				{ return default; }
			}

			public static bool IsWordBreak(char c)
			{ return default; }

			private Parser(string jsonString)
			{
				throw new AnalysisFailedException("No IL was generated.");
			}

			public static object Parse(string jsonString, Type t)
			{ return default; }

			public void Dispose()
			{ }

			private object ParseDictionary(Type t)
			{ return default; }

			private object ParseList(Type t)
			{ return default; }

			private object ParseValue(Type t)
			{ return default; }

			private object ParseByToken(TOKEN token, Type t)
			{ return default; }

			private Dictionary<string, SetPropMethod> CreateClassDelegate(Type t)
			{ return default; }

			private object ParseClass(Type t)
			{ return default; }

			private object ParseArray(Type t)
			{ return default; }

			private string ParseString()
			{ return default; }

			private object ParseNumber()
			{ return default; }

			private void EatWhitespace()
			{ }
		}

		private sealed class Serializer
		{
			private class GetPropMethod
			{
				private MethodInfo _method;

				private object[] _params;

				public GetPropMethod(MethodInfo mi)
				{
					throw new AnalysisFailedException("No IL was generated.");
				}

				public object GetValue(ref object owner)
				{ return default; }
			}

			private StringBuilder builder;

			private Dictionary<Type, Dictionary<string, GetPropMethod>> _classProps;

			private Type _lastClassType;

			private Dictionary<string, GetPropMethod> _propDict;

			private Serializer()
			{ }

			public static string Serialize(object obj)
			{ return default; }

			private void SerializeValue(object value)
			{ }

			private void SerializeDictionary(IDictionary obj)
			{ }

			private Dictionary<string, GetPropMethod> CreateClassDelegate(Type t)
			{ return default; }

			private void SerializeClass(object obj, Type t)
			{ }

			private void SerializeList(IList anArray)
			{ }

			private void SerializeArray(Array anArray)
			{ }

			private void SerializeString(string str)
			{ }

			private void SerializeOther(object value)
			{ }
		}

		private static Dictionary<Type, Type> s_RepGetTypes;

		private static Dictionary<Type, Type> s_RepSetTypes;

		public static object Deserialize(string json, Type t)
		{ return default; }

		public static void SetDelegateType(Type org, Type gType, Type sType)
		{ }

		public static string Serialize(object obj)
		{ return default; }

		private static bool IsStruct(Type t)
		{ return default; }

		static Json()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
