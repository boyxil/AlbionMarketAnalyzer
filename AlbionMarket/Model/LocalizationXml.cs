using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AlbionMarket.Extensions;
using System.Xml.Linq;

namespace AlbionMarket.Model
{
	public class LocalizationXmls: IXmlSerializable
	{
		public LocalizationXml[] Localizations { get; set; }

		public void ReadXml(XmlReader reader)
		{
			List<LocalizationXml> result = new List<LocalizationXml>();

			while (!reader.EOF)
			{
				string name = reader.Name;
				if (name == "tu")
				{
					XElement localization = XElement.Parse(reader.ReadOuterXml());
					var item = localization.ToString().SerializeXmlToObject<LocalizationXml>();
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

	[XmlRoot("tu")]
	public class LocalizationXml
	{
		[XmlAttribute("tuid")]
		public string UniqueName { get; set;}
		[XmlArrayItem("tuv")]
		public Description[] Descriptions { get; set; }
	}

	[XmlRoot("tuv")]
	public class Description
	{
		[XmlAttribute("xml:lang")]
		public string Language { get; set; }
		[XmlElement("seg")]
		public string DescriptionText { get; set; }
	}
}
