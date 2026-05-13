using Cpp2IlInjected;
using UnityEngine;

namespace Agens.Stickers
{
	public class StickerIcon
	{
		public enum Idiom
		{
			Iphone = 0,
			Ipad = 1,
			Universal = 2,
			IosMarketing = 3
		}

		public enum Scale
		{
			Original = 1,
			Double = 2,
			Triple = 3
		}

		public Vector2 size;

		public Idiom idiom;

		public string filename;

		public Scale scale;

		public string platform;

		public StickerIcon(Texture2D texture, int width, int height, Idiom idiom, Scale scale = Scale.Double, string platform = null)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public string GetIdiom()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public string GetScale()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
