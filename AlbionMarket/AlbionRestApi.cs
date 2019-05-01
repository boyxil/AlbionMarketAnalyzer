using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using AlbionMarket.Model

namespace AlbionMarket
{
	public static class AlbionHttpClient
	{
		private static readonly HttpClient client = new HttpClient();

		public static string GetItems()
		{
			string url = "https://raw.githubusercontent.com/broderickhyman/ao-bin-dumps/master/formatted/items.json";
			string result = client.GetStringAsync(url).Result;
			return result;
		}

		public static string GetPrices(string[] items, Locations[] locations)
		{
			string url = "www.albion-online-data.com/api/v1/stats/Prices/{0}?locations={1}";
			string itemsUrl = items[0];
			for (int i = 1; i < items.Length; i++)
				itemsUrl += $"%{i}C{items[i]}";
			string locationsUrl = locations.ToString();
			for (int i = 1; i < locations.Length; i++)
				itemsUrl += $"%{i}C{locations[i]}";
			url = String.Format(url, new string[] { itemsUrl, locationsUrl });
			return client.GetStringAsync(url).Result;
		}
	}
}
