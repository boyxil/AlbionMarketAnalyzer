using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using AlbionMarket.Model;
using System.Reflection;
using System.Linq;

namespace AlbionMarket.Extensions
{
	public static class XmlReaderExtensions
	{
		public static IEnumerable<T> GetListOf<T>(this XmlReader reader)
		{
			var result = new List<T>();
			Type type = typeof(T);

			var classAttribute =
				type.CustomAttributes?.FirstOrDefault(a => a.AttributeType == typeof(System.Xml.Serialization.XmlRootAttribute))
				?.ConstructorArguments?.FirstOrDefault().Value.ToString() ?? type.Name.ToString();

			while (reader.Name == classAttribute)
			{
				var a = reader.ReadOuterXml();
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				result.Add((T)xmlSerializer.Deserialize(a.ToStream()));
			}
			return result;
		}

		public static bool TrySkipUntilNotEmpty(this XmlReader reader)
		{
			try
			{
				while (reader.AttributeCount == 0 && reader.Value != string.Empty)
					reader.Skip();
			}
			catch { return false; }
			return true;
		}
	}
}
