using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AlbionMarket.Model
{
	[XmlRoot("tmx")]
	public class LocalizationXmls
	{
		[XmlArray("body")]
		[XmlArrayItem("tu")]
		public LocalizationXml[] localizationXmls { get; set; }
	}

	public class LocalizationXml
	{
		[XmlAttribute("tuid")]
		public string UniqueName { get; set;}
		[XmlArrayItem("tuv")]
		public Description[] Descriptions { get; set; }
	}

	public class Description
	{
		[XmlAttribute("xml:lang")]
		public string Language { get; set; }
		[XmlElement("seg")]
		public string DescriptionText { get; set; }
	}
}
