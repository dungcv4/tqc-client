using System.Runtime.CompilerServices;
using Cpp2IlInjected;
using MarsSDK.LitJson;

namespace MarsSDK.SocialMedia
{
	public class SocialMediaData
	{
		public string Name
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			private set
			{ }
		}

		public string ID
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			private set
			{ }
		}

		public string URL
		{
			[CompilerGenerated]
			get
			{ return default; }
			[CompilerGenerated]
			private set
			{ }
		}

		public SocialMediaData(JsonData jd)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
