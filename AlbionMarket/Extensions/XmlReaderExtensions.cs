using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using AlbionMarket.Model;

namespace AlbionMarket.Extensions
{
	public static class XmlReaderExtensions
	{
		/// TODO: Use reflection to retrieve a name of a variable
		public static IEnumerable<T> GetListOf<T>(this XmlReader reader, string name)
		{
			var result = new List<T>();
			while (reader.Name == name)
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
				while (reader.AttributeCount == 0)
					reader.Skip();
			}
			catch { return false; }
			return true;
		}
	}
}
