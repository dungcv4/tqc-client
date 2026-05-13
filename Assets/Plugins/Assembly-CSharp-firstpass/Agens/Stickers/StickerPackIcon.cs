using System;
using Cpp2IlInjected;
using UnityEngine;

namespace Agens.Stickers
{
	[Serializable]
	public class StickerPackIcon
	{
		[Serializable]
		public class IconExportSettings
		{
			public Color BackgroundColor;

			[Range(0f, 100f)]
			public int FillPercentage;

			public FilterMode FilterMode;

			public ScaleMode ScaleMode;

			public IconExportSettings()
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		[Header("1024 x 1024 px")]
		[SerializeField]
		private Texture2D appStore;

		[Header("1024 x 768 px")]
		[SerializeField]
		private Texture2D messagesAppStore;

		public IconExportSettings Settings;

		public bool Override;

		[Header("148 x 110 px")]
		[SerializeField]
		private Texture2D messagesiPadPro2;

		[Header("134 x 100 px")]
		[SerializeField]
		private Texture2D messagesiPad2;

		[Header("120 x 90 px")]
		[SerializeField]
		private Texture2D messagesiPhone2;

		[Header("180 x 135 px")]
		[SerializeField]
		private Texture2D messagesiPhone3;

		[Header("54 x 40 px")]
		[SerializeField]
		private Texture2D messagesSmall2;

		[Header("81 x 60 px")]
		[SerializeField]
		private Texture2D messagesSmall3;

		[Header("64 x 48 px")]
		[SerializeField]
		private Texture2D messages2;

		[Header("96 x 72 px")]
		[SerializeField]
		private Texture2D messages3;

		[Header("58 x 58 px")]
		[SerializeField]
		private Texture2D iPhoneSettings2;

		[Header("87 x 87 px")]
		[SerializeField]
		private Texture2D iPhoneSettings3;

		[Header("58 x 58 px")]
		[SerializeField]
		private Texture2D iPadSettings2;

		public StickerIcon[] Icons
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D[] Textures
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Vector2[] Sizes
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D AppStore
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesAppStore
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon AppStoreIcon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesAppStoreIcon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesIpadPro2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesIpadPro2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesIpad2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesIpad2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesiPhone2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesiPhone2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesiPhone3
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesiPhone3Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesSmall2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesSmall2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D MessagesSmall3
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon MessagesSmall3Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D Messages2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon Messages2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D Messages3
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon Messages3Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D IPhoneSettings2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon IPhoneSettings2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D IPhoneSettings3
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon IPhoneSettings3Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D IPadSettings2
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public StickerIcon IPadSettings2Icon
		{
			get
			{
				throw new AnalysisFailedException("No IL was generated.");
			}
		}

		public Texture2D GetDefaultTexture(int width, int height)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public StickerPackIcon()
		{
			throw new AnalysisFailedException("No IL was generated.");
		}
	}
}
