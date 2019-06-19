using System;
using System.Collections.Generic;
using System.Text;
using AlbionMarket.Model.DBContext;
using System.Linq;
using AlbionMarket.Model;

namespace AlbionMarket
{
	public static class Hauling
	{
		public static void Run(/*Location beginingCity, Location destinationCity*/)
		{
			var itemsIds = GetItemsIds();
			var beginingCityData = AlbionDataProjectRestApi.GetItemPrices(itemsIds, Location.Martlock);
			var destinationCityData = AlbionDataProjectRestApi.GetItemPrices(itemsIds, Location.FortSterling);

		}

		private static string[] GetItemsIds()
		{
			var items = ItemsBuilder.GetItemsNames("JsonFiles/ItemsOfInterest.json").ToArray();
			List<string> results = new List<string>();
			using (var db = new AlbionContext())
			{
				var entities = db.GetItemsWithDescription(items);
				foreach (var entitie in entities)
					results.Add(entitie.Entity.UniqueName);
			}
			return results.ToArray();
		}
	}
}
