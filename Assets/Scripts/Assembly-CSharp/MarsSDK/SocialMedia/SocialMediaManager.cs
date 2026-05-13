using System.Collections.Generic;
using Cpp2IlInjected;
using MarsSDK.LitJson;

namespace MarsSDK.SocialMedia
{
	public class SocialMediaManager
	{
		private List<SocialMediaData> socialMediaDataList;

		private static SocialMediaManager mInstance;

		public static SocialMediaManager Instance()
		{ return default; }

		public void SetServerData(JsonData jd)
		{ }

		public SocialMediaData GetSocialMediaData(string key)
		{ return default; }

		public SocialMediaData GetSocialMediaDataByIndex(int index)
		{ return default; }

		public string GetSocialMediaURL(string key)
		{ return default; }

		public string GetSocialMediaURLByIndex(int index)
		{ return default; }

		public int GetSocialMediaDataCount()
		{ return default; }

		public string GetSocialMediaNameListJsonString()
		{ return default; }

		public SocialMediaManager()
		{ }
	}
}
