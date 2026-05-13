using System;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK
{
	public class PlayerCharactor : MarsBrigeSingleton<PlayerCharactor>
	{
		// Source: dump.cs EOperationAgent.PlayerCharactor  // matches class name
		public const string CB_MSG_SET_RESULT = "1";

		public static dEventProcessWithStatus doEventSetDataResult;

		private string _characterId;

		private string _serverId;

		private string _characterName;

		private bool _isInit;

		private static AndroidJavaClass mJc;

		private static AndroidJavaObject mJo;

		public string characterId
		{
			get
			{ return default; }
			private set
			{ }
		}

		public string serverId
		{
			get
			{ return default; }
			private set
			{ }
		}

		public string characterName
		{
			get
			{ return default; }
			private set
			{ }
		}

		public PlayerCharactor() : base(EOperationAgent.PlayerCharactor)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static AndroidJavaObject getJavaInstance()
		{ return default; }

		public bool isSet()
		{ return default; }

		internal void clearCharacterData()
		{ }

		[Obsolete("Use [ setCharacterData(serverId, characterId, characterName) ] instead ", true)]
		public bool initCharacterData(string serverId, string characterId, string characterName)
		{ return default; }

		public int setCharacterData(string serverId, string characterId, string characterName)
		{ return default; }

		internal void overrideCharacterData(string serverId, string characterId, string characterName)
		{ }

		public bool checkCharacterData(string serverID, string characterID, string characterName)
		{ return default; }

		public bool isNewCharacter(string serverID, string characterID, string characterName)
		{ return default; }

		private void MsgProcessInitResult(string[] args)
		{ }
	}
}
