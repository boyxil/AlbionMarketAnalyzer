using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AlbionMarket.Model
{
	public class ItemPriceJson
	{
		[JsonProperty("item_id")]
		public string UniqueName { get; set; }
		[JsonProperty("city")]
		public Location Location { get; set; }
		[JsonProperty("quality")]
		public int Quality { get; set; }
		[JsonProperty("sell_price_min")]
		public decimal SellPriceMin { get; set; }
		[JsonProperty("sell_price_min_date")]
		public string SellPriceMinDate { get; set; }
		[JsonProperty("sell_price_max")]
		public decimal SellPriceMax { get; set; }
		[JsonProperty("sell_price_max_date")]
		public string SellPriceMaxDate { get; set; }
		[JsonProperty("buy_price_min")]
		public decimal BuyPriceMin { get; set; }
		[JsonProperty("buy_price_min_date")]
		public string BuyPriceMinDate { get; set; }
		[JsonProperty("buy_price_max")]
		public decimal BuyPriceMax { get; set; }
		[JsonProperty("buy_price_max_date")]
		public string ButPriceMaxDate { get; set; }
		public decimal Revenue => BuyPriceMin > 0 ? ((SellPriceMin - BuyPriceMin) * 100) / SellPriceMin : 0;
	}
}
