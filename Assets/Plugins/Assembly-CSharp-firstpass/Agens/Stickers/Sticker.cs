using System.Collections.Generic;
using Cpp2IlInjected;
using UnityEngine;

namespace Agens.Stickers
{
	public class Sticker : ScriptableObject
	{
		[Tooltip("Name of the sticker")]
		public string Name;

		[Tooltip("Frames per second. Apple recommends 15+ FPS")]
		public int Fps;

		[Tooltip("Number of repetitions (0 being infinite cycles")]
		public int Repetitions;

		public int Index;

		public bool Sequence;

		public List<Texture2D> Frames;

		public void CopyFrom(Sticker sticker, int i)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public Sticker()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
