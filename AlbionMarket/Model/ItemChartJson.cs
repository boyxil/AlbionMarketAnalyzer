using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AlbionMarket.Model
{

	public class ItemChartJson
	{
		public Data data;
	}

	[JsonObject("data")]
	public class Data
	{
		[JsonProperty("prices_min")]
		public List<decimal> PriceMin { get; set; }
		[JsonProperty("prices_max")]
		public List<decimal> PriceMax { get; set; }
		[JsonProperty("prices_avg")]
		public List<decimal> PriceAvg { get; set; }
	}
}
