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
	public class ItemPriceJsonConverter : JsonConverter<List<ItemPriceJson>>
	{
		public override List<ItemPriceJson> ReadJson(JsonReader reader, Type objectType, List<ItemPriceJson> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var names = objectType.GenericTypeArguments[0].GetJsonPropertyAtrribut();
			List<ItemPriceJson> result = new List<ItemPriceJson>();
			var item = new ItemPriceJson();
			while (reader.Read())
			{
				var valuee = reader.Value;
				var name = names.FirstOrDefault(e => e.AttributeName.Equals(valuee)) ?? names.FirstOrDefault(e => e.Name.Equals(valuee));
				if (name != null)
				{
					reader.Read();
					Type type = item.GetType().GetProperty(name.Name).PropertyType;

					var value = type.GetValue(reader.Value.ToString().Trim());

					var currentValue = item.GetType().GetProperty(name.Name).GetValue(item);

					if (currentValue == null || (currentValue.Equals(type.GetValue())))
						item.GetType().GetProperty(name.Name).SetValue(item, value);
					else
					{
						result.Add(item);
						item = new ItemPriceJson();
						item.GetType().GetProperty(name.Name).SetValue(item, value);
					}
				}
			}
			return result;
		}

		public override void WriteJson(JsonWriter writer, List<ItemPriceJson> value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
