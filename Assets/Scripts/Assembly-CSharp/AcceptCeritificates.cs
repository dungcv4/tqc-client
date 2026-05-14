// Source: Ghidra work/06_ghidra/decompiled_full/AcceptCeritificates/ValidateCertificate.c RVA 0x1909124
// 1-1: build X509Certificate2 from data; compare GetPublicKeyString() to static PUB_KEY (cert pinning);
//      if equal → accept, else fall back to default validation.

using System.Security.Cryptography.X509Certificates;
using Cpp2IlInjected;
using UnityEngine.Networking;

public class AcceptCeritificates : CertificateHandler
{
	// Source: Ghidra PTR_DAT_03462bf8 → static field PUB_KEY initialized in .cctor.
	private static string PUB_KEY;

	protected override bool ValidateCertificate(byte[] certificateData)
	{
		X509Certificate2 cert = new X509Certificate2(certificateData);
		string pk = cert.GetPublicKeyString();
		if (pk == PUB_KEY) return true;
		return base.ValidateCertificate(certificateData);
	}

	public AcceptCeritificates() { }

	// Source: Ghidra .cctor (not emitted) — PUB_KEY = "" (no pinning in Editor diag).
	static AcceptCeritificates()
	{
		PUB_KEY = "";
	}
}
