using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using AlbionMarket.Model;
using System.Linq;

namespace AlbionMarket.CustomJsonConverter
{
	class ItemPriceJsonConverter : JsonConverter<IEnumerable<ItemPriceJson>>
	{
		public override IEnumerable<ItemPriceJson> ReadJson(JsonReader reader, Type objectType, IEnumerable<ItemPriceJson> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var a = (objectType.GenericTypeArguments[0].GetProperties()[0].GetCustomAttributes(typeof(JsonPropertyAttribute), false) as JsonPropertyAttribute[])[0].PropertyName;
			Dictionary<string, string> names = GetPropetysNamesAndIds(objectType);
			var result = new List<ItemPriceJson>();
			var item = new ItemPriceJson();
			while (reader.Read())
			{
				//if(names.Keys.Contains(reader.Value) || names.Values.Contains(reader.Value))
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, IEnumerable<ItemPriceJson> value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		private Dictionary<string,string> GetPropetysNamesAndIds(Type objectType)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			var listOfProperties = objectType.GenericTypeArguments[0].GetProperties();
			foreach (var property in listOfProperties)
			{
				var propertyName = property.Name;
				var jsonAttributeName = (property?.GetCustomAttributes(typeof(JsonPropertyAttribute), false) as JsonPropertyAttribute[])?.FirstOrDefault().PropertyName;
				result.Add(jsonAttributeName, propertyName);
			}
			return result;
		}
	}
}
