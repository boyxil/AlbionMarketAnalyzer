using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using AlbionMarket.Model;
using System.Linq;
using AlbionMarket.Extensions;
using System.Reflection;

namespace AlbionMarket.CustomJsonConverter
{
	class ItemPriceJsonConverter : JsonConverter<List<ItemPriceJson>>
	{
		public override List<ItemPriceJson> ReadJson(JsonReader reader, Type objectType, List<ItemPriceJson> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var names = objectType.GenericTypeArguments[0].GetJsonPropertyAtrribut();
			var item = new ItemPriceJson();
			while (reader.Read())
			{
				var valuee = reader.Value;
				var name = names.FirstOrDefault(e => e.AttributeName.Equals(valuee)) ?? names.FirstOrDefault(e => e.Name.Equals(valuee));
				if (name != null)
				{
					reader.Read();
					var value = reader.Value;
					Type type = item.GetType().GetProperty(name.Name).PropertyType;

					var currentValue = Activator.CreateInstance(objectType);
					currentValue = item.GetType().GetProperty(name.Name).GetValue(item);

					if (currentValue == type.GetDefault())
						item.GetType().GetProperty(name.Name).SetValue(item, value);
					else
					{
						existingValue.Add(item);
						item = new ItemPriceJson();
						item.GetType().GetProperty(name.Name).SetValue(item, value);
					}
				}
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, List<ItemPriceJson> value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
