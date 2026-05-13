using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	public class JsonMapper
	{
		private static int max_nesting_depth;

		private static IFormatProvider datetime_format;

		private static IDictionary<Type, ExporterFunc> base_exporters_table;

		private static IDictionary<Type, ExporterFunc> custom_exporters_table;

		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> base_importers_table;

		private static IDictionary<Type, IDictionary<Type, ImporterFunc>> custom_importers_table;

		private static IDictionary<Type, ArrayMetadata> array_metadata;

		private static readonly object array_metadata_lock;

		private static IDictionary<Type, IDictionary<Type, MethodInfo>> conv_ops;

		private static readonly object conv_ops_lock;

		private static IDictionary<Type, ObjectMetadata> object_metadata;

		private static readonly object object_metadata_lock;

		private static IDictionary<Type, IList<PropertyMetadata>> type_properties;

		private static readonly object type_properties_lock;

		private static JsonWriter static_writer;

		private static readonly object static_writer_lock;

		static JsonMapper()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void AddArrayMetadata(Type type)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void AddObjectMetadata(Type type)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void AddTypeProperties(Type type)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static MethodInfo GetConvOp(Type t1, Type t2)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static object ReadValue(Type inst_type, JsonReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static IJsonWrapper ReadValue(WrapperFactory factory, JsonReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void ReadSkip(JsonReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void RegisterBaseExporters()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void RegisterBaseImporters()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void RegisterImporter(IDictionary<Type, IDictionary<Type, ImporterFunc>> table, Type json_type, Type value_type, ImporterFunc importer)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void WriteValue(object obj, JsonWriter writer, bool writer_is_private, int depth)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static string ToJson(object obj)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void ToJson(object obj, JsonWriter writer)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static JsonData ToObject(JsonReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static JsonData ToObject(TextReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static JsonData ToObject(string json)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static T ToObject<T>(JsonReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static T ToObject<T>(TextReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static T ToObject<T>(string json)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static IJsonWrapper ToWrapper(WrapperFactory factory, JsonReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static IJsonWrapper ToWrapper(WrapperFactory factory, string json)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void RegisterExporter<T>(ExporterFunc<T> exporter)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void RegisterImporter<TJson, TValue>(ImporterFunc<TJson, TValue> importer)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void UnregisterExporters()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public static void UnregisterImporters()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonMapper()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
