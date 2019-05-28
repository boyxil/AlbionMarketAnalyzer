using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AlbionMarket.Model
{
	[XmlRoot("tu")]
	public class Localization
	{
		[XmlAttribute("tuid")]
		public string UniqueName { get; set; }
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
