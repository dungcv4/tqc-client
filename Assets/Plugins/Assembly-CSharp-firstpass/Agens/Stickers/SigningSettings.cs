using System;
using Cpp2IlInjected;
using UnityEngine;

namespace Agens.Stickers
{
	[Serializable]
	public class SigningSettings
	{
		public bool AutomaticSigning;

		[Tooltip("The UUID of the provisioning profile")]
		public string ProvisioningProfile;

		[Tooltip("The name of the provisioning profile")]
		public string ProvisioningProfileSpecifier;

		public SigningSettings()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
