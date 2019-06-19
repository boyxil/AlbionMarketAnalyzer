using System.IO;
using AlbionMarket.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using AlbionMarket.Model.DBContext;

namespace AlbionMarket
{
	class Program
	{
		static void Main(string[] args)
		{
			Hauling.Run();
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

		public static void ItemsToBuy()
		{
			var location = new Location[] { Location.Caerleon };
			var expectTires = new ItemTire[] { ItemTire.T1, ItemTire.T2, ItemTire.T3 };
			var expectEnchantment = new string[] { "1","2","3" };
			var items = ItemsBuilder.GetItems(location, expectTires, expectEnchantment);
			items = items.OrderByDescending(e => e.Revenue)/*Where(b => b.Revenue < 90)*/.Take(30);
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
