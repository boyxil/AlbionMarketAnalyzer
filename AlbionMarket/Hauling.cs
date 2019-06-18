using System;
using System.Collections.Generic;
using System.Text;
using AlbionMarket.Model.DBContext;
using System.Linq;

namespace AlbionMarket
{
	public static class Hauling
	{
		public static void Run()
		{
			var items = ItemsBuilder.GetItemsNames("JsonFiles/ItemsOfInterest.json").ToList();
			var items2 = new string[] { "Scholar Robe", "Cleric Robe" };
				var a = AlbionContext.GetItemsWithDescription(items.ToArray());
				foreach (var b in a)
				{
					Console.WriteLine(b.item.UniqueName + "  " + b.localization.Descriptions[0].DescriptionText);
				}
			//var quary = AlbionContext.GetItemsWithDescription();

			//foreach (var item in quary)
			//	Console.WriteLine(item.item.UniqueName + "  " + item.localization.Descriptions[0].DescriptionText);
		}
	}
}
