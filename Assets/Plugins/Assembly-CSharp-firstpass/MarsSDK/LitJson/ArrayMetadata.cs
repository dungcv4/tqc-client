using System;
using Cpp2IlInjected;

namespace MarsSDK.LitJson
{
	internal struct ArrayMetadata
	{
		private Type element_type;

		private bool is_array;

		private bool is_list;

		public Type ElementType
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

		public bool IsArray
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

		public bool IsList
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
	}
}
