using System.IO;
using AlbionMarket.Model;
using System.Linq;
using System;

namespace AlbionMarket
{
	class Program
	{
		static void Main(string[] args)
		{

		}

		public static void BlackMarketRevenue()
		{
			var location = new Location[] { Location.BlackMarket, Location.Caerleon };
			var expectTires = new ItemTire[] { ItemTire.T1, ItemTire.T2, ItemTire.T3 };
			var expectEnchantment = new string[] { "2"};
			var items = ItemsBuilder.GetItems(location, expectTires, expectEnchantment);
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
			var a = AlbionRestApi.GetItems();
			File.WriteAllText("Items.json", a);
		}
	}
}
