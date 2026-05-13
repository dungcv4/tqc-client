using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Platform
{
	public class PlayGameServicePlatform : BasePlatform
	{
		public delegate void dEventProcess();

		public const string PGSPLATFORM_SUPPORT_PGS_MSG = "1";

		public const string PGSPLATFORM_SUPPORT_GAME_PROMOTION_MSG = "2";

		public const int STATUS_NONE = 0;

		public const int STATUS_EXIST = 1;

		public const int STATUS_NOT_EXIST = 2;

		private int _isPlatformExist;

		private static AndroidJavaClass mJc;

		private static AndroidJavaObject mJo;

		private static PlayGameServicePlatform mInstance;

		public static dEventProcess doEventPGSSupported;

		public static dEventProcess doEventGamePromotionSupported;

		public PlayGameServicePlatform() : base((EOperationAgent)default!)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override Type GetPlatformClassType()
		{ return default; }

		public static PlayGameServicePlatform Instance()
		{ return default; }

		public bool CheckPlayGameServicePlatformExist()
		{ return default; }

		public void InitPGSSdk()
		{ }

		public void SyncPGSData()
		{ }

		public void CheckGamesPromotionSupport()
		{ }

		public bool IsSupported()
		{ return default; }

		public void ShowGamesPromotions()
		{ }

		private void MsgPGSSupported(string[] args)
		{ }

		private void MsgGamePromotionSupported(string[] args)
		{ }
	}
}
