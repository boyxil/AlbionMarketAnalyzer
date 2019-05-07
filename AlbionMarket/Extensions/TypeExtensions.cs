using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace AlbionMarket.Extensions
{
	public static class TypeExtensions
	{
		public static IEnumerable<VariableNames> GetJsonPropertyAtrribut(this Type type)
		{
			var listOfProperties = type.GetProperties();
			List<VariableNames> result =  new List<VariableNames>();
			foreach (var property in listOfProperties)
			{
				var propertyName = property.Name;
				var jsonAttributeName = (property?.GetCustomAttributes(typeof(JsonPropertyAttribute), false) as JsonPropertyAttribute[])?.FirstOrDefault()?.PropertyName;
				result.Add(new VariableNames { Name = propertyName, AttributeName = jsonAttributeName ?? string.Empty});
			}
			return result;
		}

		public static PropertyInfo GetProperty(this Type type, string name) => type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance)
	}

	public class VariableNames
	{
		public string Name;
		public string AttributeName;
	}
}
