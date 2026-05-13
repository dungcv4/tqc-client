using System.IO;
using System.Text;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	internal class Lexer
	{
		private delegate bool StateHandler(FsmContext ctx);

		private static int[] fsm_return_table;

		private static StateHandler[] fsm_handler_table;

		private bool allow_comments;

		private bool allow_single_quoted_strings;

		private bool end_of_input;

		private FsmContext fsm_context;

		private int input_buffer;

		private int input_char;

		private TextReader reader;

		private int state;

		private StringBuilder string_buffer;

		private string string_value;

		private int token;

		private int unichar;

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

		public bool EndOfInput
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public int Token
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public string StringValue
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		static Lexer()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public Lexer(TextReader reader)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static int HexValue(int digit)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void PopulateFsmTables()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static char ProcessEscChar(int esc_char)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State1(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State2(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State3(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State4(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State5(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State6(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State7(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State8(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State9(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State10(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State11(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State12(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State13(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State14(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State15(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State16(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State17(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State18(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State19(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State20(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State21(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State22(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State23(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State24(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State25(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State26(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State27(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool State28(FsmContext ctx)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private bool GetChar()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private int NextChar()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public bool NextToken()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private void UngetChar()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
