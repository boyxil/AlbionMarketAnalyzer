using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AlbionMarket.Extensions;
using System.Xml.Linq;
using System.Data.Entity;
using AlbionMarket.Model;

namespace AlbionMarket.Model
{
	[XmlRoot("tmx")]
	public class LocalizationXmls : IXmlSerializable	{
		[XmlArray("body")]
		[XmlArrayItem("tu")]
		public Localization[] Localizations { get; set; }

		public void ReadXml(XmlReader reader)
		{
			List<Localization> result = new List<Localization>();

			while (!reader.EOF)
			{
				string name = reader.Name;
				if (name == "tu")
				{
					XElement localization = XElement.Parse(reader.ReadOuterXml());
					var item = localization.ToString().SerializeXmlToObject<Localization>();
					var elements = localization.Elements("tuv");
					var descriptions = new List<Description>();
					foreach (var element in elements)
						descriptions.Add(element.ToString().SerializeXmlToObject<Description>());
					item.Descriptions = descriptions.ToArray();
					result.Add(item);
				}
				else
					reader.Read();
			}
			Localizations = result.ToArray();
		}

		public void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}
	}

}
