using System;
using Cpp2IlInjected;
using MarsSDK.LitJson;

namespace MarsSDK.Platform
{
	[Serializable]
	public class UJBillingProduct
	{
		public enum eProductType
		{
			INAPP = 0,
			SUBS = 1
		}

		private string mOriginalJson;

		private JsonData mParsedJson;

		public string OriginalJson
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetProductId()] instead", true)]
		public string productId
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetTitle()] instead", true)]
		public string title
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetDescription()] instead", true)]
		public string description
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetPrice()] instead", true)]
		public string price
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetCurrencyCode()] instead", true)]
		public string price_currency_code
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetFormattedPrice()] instead", true)]
		public string formatted_price
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated.", true)]
		public string original_price
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated.", true)]
		public int price_amount_micros
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated.", true)]
		public int original_price_micros
		{
			get
			{ return default; }
		}

		[Obsolete("This API is deprecated. Use [GetProductType()] instead", true)]
		public string type
		{
			get
			{ return default; }
		}

		public UJBillingProduct()
		{ }

		public UJBillingProduct(string jsonString)
		{
			throw new AnalysisFailedException("No IL was generated.");
		}

		public override string ToString()
		{ return default; }

		public eProductType GetProductType()
		{ return default; }

		public string GetProductId()
		{ return default; }

		public string GetTitle()
		{ return default; }

		public string GetDescription()
		{ return default; }

		public string GetPrice()
		{ return default; }

		public string GetCurrencyCode()
		{ return default; }

		public string GetFormattedPrice()
		{ return default; }
	}
}
