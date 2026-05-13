using System.IO;
using Cpp2IlInjected;
using UnityEngine;

namespace MarsSDK.Audio
{
	public class WaveCreator
	{
		private const int HEADER_SIZE = 44;

		public static void Save(string filePath, AudioClip clip)
		{ }

		private static FileStream CreateEmpty(string filePath)
		{ return default; }

		private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
		{ }

		private static void WriteHeader(FileStream fileStream, AudioClip clip)
		{ }

		public static AudioClip CreateNoiseAudioClip(int sec)
		{ return default; }

		private static void WriteWav(AudioClip clip, Stream stream)
		{ }

		public static byte[] SaveToBytes(AudioClip clip)
		{ return default; }

		public static byte[] ConvertFloatArrayToByteArray(float[] _record)
		{ return default; }

		public static byte[] AudioClipToByteArray(AudioClip clip)
		{ return default; }

		public WaveCreator()
		{ }
	}
}
