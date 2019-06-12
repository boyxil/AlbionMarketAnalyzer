using System.IO;
using AlbionMarket.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using AlbionMarket.Model.DBContext;

namespace AlbionMarket {
	public static class LongTermInvestments {

		public static void Run(Location location, DateTime date, int days = 7)
		{
			var items = ItemsBuilder.GetRawItems(ItemsBuilder.GetItemsNames("JsonFiles/ItemsOfInterest.json"));
			List<dynamic> listOfItems = new List<dynamic>();

			foreach (var item in items)
				listOfItems.Add(GetItemPricesChartData(item, Location.Caerleon, date, days));

			var sortedItems = listOfItems.OrderBy(e => e.totalRevenue);
			PrintAndSaveToFile(sortedItems, location, date);
		}

		private static void PrintAndSaveToFile(IOrderedEnumerable<dynamic> items, Location location, DateTime date)
		{
			using (var fileStream = File.Create($"C:/AlbionHistory/{location}{date.ToString("dd_MM_yyyy")}.txt"))
			{
				foreach (var item in items)
				{
					string response = $"Buy {item.name} in {item.location} for price: {item.averagePrice} sell for {item.maximumPrice} revenue : {item.totalRevenue} \n";
					fileStream.Write(new UTF8Encoding(true).GetBytes(response));
					Console.WriteLine(response);
				}
			}
		}

		private static dynamic GetItemPricesChartData(ItemRawJson itemRawJson, Location location, DateTime todayDate, int days = 7)
		{
			List<decimal> prices = new List<decimal>();
			decimal maximumPrice = 0;
			for (int i = 0; i < days; i++)
			{
				var date = todayDate.Date - TimeSpan.FromDays(i);
				var itemPrices = AlbionDataProjectRestApi.GetItemChart(itemRawJson.UniqueName, location, date);
				if (itemPrices != null)
				{
					itemPrices.data.PriceMin.Sort();
					prices.Add(itemPrices.data.PriceMin[itemPrices.data.PriceMin.Count() / 3]);
					var index = itemPrices.data.PriceMin.Count - 2 > 0 ? itemPrices.data.PriceMin.Count - 2 : 0;
					var maximumPriceOfDay = itemPrices.data.PriceMin[index];
					maximumPrice = maximumPriceOfDay > maximumPrice ? maximumPriceOfDay : maximumPrice;
				}
			}
			var averagePrice = Math.Round(prices.Sum() / (prices.Count() > 0 ? prices.Count() : 1));
			decimal totalRevenue = ((maximumPrice - averagePrice) * 100) / (averagePrice > 0 ? averagePrice : 1);
			return new { name = itemRawJson.LocalizedNames.First().Value, location = location, averagePrice = averagePrice, maximumPrice = maximumPrice, totalRevenue = totalRevenue };
		}
	}
}
