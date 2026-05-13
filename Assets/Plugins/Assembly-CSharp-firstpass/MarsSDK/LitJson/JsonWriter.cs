using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	public class JsonWriter
	{
		private static NumberFormatInfo number_format;

		private WriterContext context;

		private Stack<WriterContext> ctx_stack;

		private bool has_reached_end;

		private char[] hex_seq;

		private int indentation;

		private int indent_value;

		private StringBuilder inst_string_builder;

		private bool pretty_print;

		private bool validate;

		private TextWriter writer;

		public int IndentValue
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

		public bool PrettyPrint
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

		public TextWriter TextWriter
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool Validate
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

		static JsonWriter()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonWriter()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonWriter(StringBuilder sb)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonWriter(TextWriter writer)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void DoValidation(Condition cond)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void Init()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void IntToHex(int n, char[] hex)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void Indent()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void Put(string str)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void PutNewline()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void PutNewline(bool add_comma)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void PutString(string str)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void Unindent()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override string ToString()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Reset()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(bool boolean)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(decimal number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(double number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(int number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(long number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(string str)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Write(ulong number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void WriteArrayEnd()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void WriteArrayStart()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void WriteObjectEnd()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void WriteObjectStart()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void WritePropertyName(string property_name)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
