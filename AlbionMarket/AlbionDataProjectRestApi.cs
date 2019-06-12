using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using AlbionMarket.Model;
using Newtonsoft.Json;
using AlbionMarket.CustomJsonConverter;

namespace AlbionMarket {
	public static class AlbionDataProjectRestApi
	{
		private static readonly HttpClient client = new HttpClient();

		/// <summary>
		/// Gets a json containing all items available in Albion
		/// </summary>
		/// <returns></returns>
		public static string GetItems()
		{
			string url = "https://raw.githubusercontent.com/broderickhyman/ao-bin-dumps/master/formatted/items.json";
			return client.GetStringAsync(url).Result;
		}

		/// <summary>
		/// Get a json containing prices for specific items in specific locations
		/// </summary>
		/// <param name="items">Array of items that we are searching for (maximum 300 intems)</param>
		/// <param name="locations">Array of locations that we are searching for</param>
		/// <returns>Json</returns>
		public static string GetPrices(string[] items, Location[] locations = null)
		{
			if (items?.Length > 250)
				throw new Exception("To many items in a request");

			string url = "https://www.albion-online-data.com/api/v1/stats/Prices/{0}?locations={1}";

			string itemsUrl = items[0];
			for (int i = 1; i < items.Length; i++)
				itemsUrl += $"%2C{items[i]}";

			string locationsUrl = locations != null ? $"{locations[0]}" : string.Empty;
			for (int i = 1; i < locations?.Length; i++)
				locationsUrl += $"%2C{locations[i]}";

			url = String.Format(url, new string[] { itemsUrl, locationsUrl });
			return client.GetStringAsync(url).Result;
		}

		/// <summary>
		/// Get a objects which represents a item prices
		/// </summary>
		/// <param name="items">Array of items that we are searching for (maximum 250 intems)</param>
		/// <param name="locations">Array of locations that we are searching for</param>
		/// <returns>IEnumerable of ItemPrcieJson</returns>
		public static IEnumerable<ItemPriceJson> GetItemPrices(IEnumerable<string> items, IEnumerable<Location> locations = null)
		{
			int iterations = (int)Math.Ceiling(items.Count() / 250M);
			List<ItemPriceJson> results = new List<ItemPriceJson>();
			for (int i = 0; i < iterations; i++)
			{
				var partOfItems = items.Skip(250 * i).Take(250);
				string response = GetPrices(partOfItems.ToArray(), locations.ToArray());
				results.AddRange(JsonConvert.DeserializeObject<List<ItemPriceJson>>(response, new ItemPriceJsonConverter()));
			}
			return results;
		}

		/// <summary>
		/// Gets a json which describes a price changes of a particular item in particular location
		/// </summary>
		/// <param name="itemName">Item of interest</param>
		/// <param name="location">Location of interest</param>
		/// <param name="date">Date of interest</param>
		/// <returns></returns>
		public static ItemChartJson GetItemChart(string itemName, Location location, DateTime date)
		{
			string url = "https://www.albion-online-data.com/api/v1/stats/Charts/{0}?locations={1}&date={2}";
			string dateFormated = $"{date.Month}%2F{date.Day}%2F{date.Year}";
			url = String.Format(url, itemName, location, dateFormated);
			var response = client.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<IEnumerable<ItemChartJson>>(response).FirstOrDefault();
		}
	}
}
