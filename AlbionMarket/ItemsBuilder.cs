using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AlbionMarket.Model;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using AlbionMarket.Model;


namespace AlbionMarket
{
	public static class ItemsBuilder
	{

		public static IEnumerable<Item> GetItems(Location[] locations = null, ItemTire[] expectTirs = null, string[] expectEnchantemts = null)
		{
			var itemsNames = GetItemsNames();
			var rawItems = GetRawItems(itemsNames, expectTirs, expectEnchantemts);
			var itemsPrices = AlbionRestApi.GetItemPrices(rawItems.Select(e => e.UniqueName).ToArray(), locations);
			return AggregateItems(rawItems, itemsPrices);
		}
		//Not the best solution, because this function every time is casted, reads a files content
		private static IEnumerable<ItemRawJson> GetRawItems(IEnumerable<string> itemsNames, ItemTire[] expectTirs = null, string[] expectEnchantemts = null)
		{
			string fileContent = File.ReadAllText("JsonFiles/Items.json");
			List<ItemRawJson> items = JsonConvert.DeserializeObject<IEnumerable<ItemRawJson>>(fileContent).ToList();
			List<ItemRawJson> results = new List<ItemRawJson>();

			foreach (string itemName in itemsNames)
				results.AddRange(
					items.Where(a => a.LocalizedNames != null
					&& a.LocalizedNames.Any(e => e.Value.Contains(itemName)
					)));

			if (expectTirs != null)
				foreach (var tear in expectTirs)
					results.RemoveAll(e => e.UniqueName.Contains($"{tear}"));

			if (expectEnchantemts != null)
				foreach (var enchantment in expectEnchantemts)
					results.RemoveAll(e => e.UniqueName.Contains($"@{enchantment}"));

			return results;
		}

		//Not the best solution, because this function every time is casted, reads a files content
		private static IEnumerable<string> GetItemsNames()
		{
			string fileContent = File.ReadAllText("JsonFiles/ItemOfInterest2.json");
			var itemsNames = JsonConvert.DeserializeObject<List<string>>(fileContent);

			foreach (var itemName in itemsNames)
				yield return itemName;
		}

		private static IEnumerable<Item> AggregateItems(IEnumerable<ItemRawJson> itemsRawJson, IEnumerable<ItemPriceJson> itemsPricesJson)
		{
			List<Item> result = new List<Item>();
			foreach (var itemPrice in itemsPricesJson)
			{
				result.Add(new Item
				{
					Name = itemsRawJson.Single(e => e.UniqueName.Equals(itemPrice.UniqueName))
							.LocalizedNames.First(a => a.Key.Equals("EN-US")).Value,
					Locations = itemPrice.Location,
					UniqueName = itemPrice.UniqueName,
					Revenue = itemPrice.Revenue,
					BuyPrice = itemPrice.BuyPriceMax,
					SellPrice = itemPrice.SellPriceMin
				});
			}

			return result;
		}
	}
}
