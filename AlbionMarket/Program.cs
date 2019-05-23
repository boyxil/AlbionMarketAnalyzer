using System.IO;
using AlbionMarket.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace AlbionMarket
{
	class Program
	{
		static void Main(string[] args)
		{
			LongTermInvestments();
			//FileStream myFileStream = new FileStream("XmlFiles/items.xml", FileMode.Open);
			//XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemsRawXml));
			//ItemsRawXml itemsRawXml = (ItemsRawXml) xmlSerializer.Deserialize(myFileStream);
		}

		public static void BlackMarketRevenue()
		{
			var location = new Location[] { Location.BlackMarket, Location.Caerleon };
			var expectTires = new ItemTire[] { ItemTire.T1, ItemTire.T2, ItemTire.T3 };
			var expectEnchantment = new string[] { "3"};
			var items = ItemsBuilder.GetItems(location, expectTires, expectEnchantment);
			var blackMarket = items.Where(e => e.Locations == Location.BlackMarket);
			var carleon = items.Where(e => e.Locations == Location.Caerleon);
			List<Item> resultsItems = new List<Item>();
			foreach (var blackmarketItem in blackMarket)
			{
				var carleonItem = carleon.FirstOrDefault(e => e.UniqueName == blackmarketItem.UniqueName);
				if(carleonItem != null)
				resultsItems.Add(new Item
				{
					BuyPrice = carleonItem.BuyPrice,
					Locations = Location.Caerleon,
					Name = carleonItem.Name,
					SellPrice = blackmarketItem.SellPrice,
					Revenue = carleonItem.BuyPrice > 0 && blackmarketItem.SellPrice > 0 ? ((blackmarketItem.SellPrice - carleonItem.BuyPrice) * 100) / blackmarketItem.SellPrice : 0,
					UniqueName = carleonItem.UniqueName
				});
			}
			resultsItems = resultsItems.OrderByDescending(e => e.Revenue).Where(b => b.Revenue < 90).Take(20).ToList();
			foreach (var item in resultsItems)
				Console.WriteLine($"{item.Name} ID: {item.UniqueName} Sell: {item.SellPrice}  Buy: {item.BuyPrice}, Revenue {item.Revenue}");
		}

		public static void LongTermInvestments()
		{
			//Configuration
			int days = 7;
			DateTime todayDate = DateTime.Now;
			var location = Location.Caerleon;
			decimal revenue = 0.16M;

			//Action
			using (var fileStream = File.Create($"C:/AlbionHistory/{location}{todayDate.ToString("dd_MM_yyyy")}.txt"))
			{
				var items = ItemsBuilder.GetItems(new Location[] { location });
				foreach (var item in items)
				{
					List<decimal> prices = new List<decimal>();
					for (int i = 0; i < days; i++)
					{
						var date = todayDate.Date - TimeSpan.FromDays(i);
						var itemPrices = AlbionDataProjectRestApi.GetItemChart(item.UniqueName, item.Locations, date);
						itemPrices.data.PriceMin.Sort();
						prices.Add(itemPrices.data.PriceMin[itemPrices.data.PriceMin.Count() / 3]);
					}
					var averagePrice = Math.Round(prices.Sum() / prices.Count());/*Math.Round(prices.Sum() / prices.Count()) * (1 - (revenue / 3));*/
					var buyPrice = Math.Round(averagePrice * (1 + (revenue /** 2 / 3*/)));
					string output = $"Buy {item.Name} in {item.Locations} for price: {averagePrice} sell for {buyPrice} \n";
					fileStream.Write(new UTF8Encoding(true).GetBytes(output));
					Console.WriteLine(output);
				}
			}
		}

		public static void ItemsToBuy()
		{
			var location = new Location[] { Location.BlackMarket };
			var expectTires = new ItemTire[] { ItemTire.T1, ItemTire.T2, ItemTire.T3 };
			var expectEnchantment = new string[] { "2", "3" };
			var items = ItemsBuilder.GetItems(location, expectTires, expectEnchantment);
			items = items.OrderByDescending(e => e.Revenue).Where(b => b.Revenue < 90).Take(20);
			foreach (var item in items)
				Console.WriteLine($"{item.Name} ID: {item.UniqueName} Sell: {item.SellPrice}  Buy: {item.BuyPrice}, Revenue {item.Revenue}");
		}

		public static void UpdateItemsFile()
		{
			var a = AlbionDataProjectRestApi.GetItems();
			File.WriteAllText("Items.json", a);
		}
	}
}
