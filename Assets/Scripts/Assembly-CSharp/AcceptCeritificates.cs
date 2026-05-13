using Cpp2IlInjected;
using UnityEngine.Networking;

public class AcceptCeritificates : CertificateHandler
{
	private static string PUB_KEY;

	protected override bool ValidateCertificate(byte[] certificateData)
	{ return default; }

	public AcceptCeritificates()
	{ }

	static AcceptCeritificates()
	{
		throw new AnalysisFailedException("No IL was generated.");
	}
}
