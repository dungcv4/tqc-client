using Cpp2IlInjected;
using UnityEngine;

namespace Agens.Stickers
{
	public static class TextureScale
	{
		public static Texture2D ScaledResized(Texture2D src, int width, int height, Color backgroundColor, float fillPercentage, FilterMode mode = FilterMode.Trilinear, ScaleMode anchor = ScaleMode.ScaleToFit)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static RenderTexture CreateScaledTexture(Texture2D src, int width, int height, Color backgroundColor, float fillPercentage, FilterMode fmode = FilterMode.Trilinear, ScaleMode scaleMode = ScaleMode.ScaleToFit)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, float imageAspect = 0f)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		private static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
