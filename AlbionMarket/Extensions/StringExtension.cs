using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AlbionMarket.Extensions
{
	public static class StringExtension
	{
		public static Stream ToStream(this string value)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(value);
			return new MemoryStream(byteArray);
		}

		public static T SerializeXmlToObject<T>(this string xml)
		{
			Type type = typeof(T);

			var classAttribute =
				type.CustomAttributes?.FirstOrDefault(a => a.AttributeType == typeof(System.Xml.Serialization.XmlRootAttribute))
				?.ConstructorArguments?.FirstOrDefault().Value.ToString() ?? type.Name.ToString();

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			return (T)xmlSerializer.Deserialize(xml.ToStream());
		}
	}
}
