using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

namespace Agens.Stickers
{
	[CreateAssetMenu(fileName = "StickerPack", menuName = "Sticker Pack")]
	public class StickerPack : ScriptableObject
	{
		[SerializeField]
		[Tooltip("The Display Name of the Sticker Pack")]
		private string title;

		[SerializeField]
		[Tooltip("Bundle identifier postfix. This will come after the parents app bundle identifier.")]
		private string bundleId;

		public SigningSettings Signing;

		public StickerPackIcon Icons;

		[SerializeField]
		[Tooltip("Small: (300px * 300px), 4 sticker a row \nMedium: (408px * 408px), 3 sticker a row \nLarge: (618px * 618px), 2 sticker a row \n")]
		private StickerSize size;

		public List<Sticker> Stickers;

		public string Title
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

		public string BundleId
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

		public StickerSize Size
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

		public StickerPack()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
