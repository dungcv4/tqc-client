using System.Collections.Generic;
using System.IO;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	public class JsonReader
	{
		private static IDictionary<int, IDictionary<int, int[]>> parse_table;

		private Stack<int> automaton_stack;

		private int current_input;

		private int current_symbol;

		private bool end_of_json;

		private bool end_of_input;

		private Lexer lexer;

		private bool parser_in_string;

		private bool parser_return;

		private bool read_started;

		private TextReader reader;

		private bool reader_is_owned;

		private bool skip_non_members;

		private object token_value;

		private JsonToken token;

		public bool AllowComments
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

		public bool AllowSingleQuotedStrings
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

		public bool SkipNonMembers
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

		public bool EndOfInput
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public bool EndOfJson
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public JsonToken Token
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

		static JsonReader()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonReader(string json_text)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public JsonReader(TextReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private JsonReader(TextReader reader, bool owned)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void PopulateParseTable()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void TableAddCol(ParserToken row, int col, params int[] symbols)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void TableAddRow(ParserToken rule)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void ProcessNumber(string number)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void ProcessSymbol()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private bool ReadToken()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public void Close()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool Read()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
