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
			var location = new Location[] { Location.Caerleon };
			var expectTires = new ItemTire[] { ItemTire.T1, ItemTire.T2 };
			var expectEnchantment = new string[] { "1", "2", "3" };
			var items = ItemsBuilder.GetItems(location, expectTires, expectEnchantment);
			items = items.OrderByDescending(e => e.Revenue).Where(b => b.Revenue < 70).Take(20);
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
