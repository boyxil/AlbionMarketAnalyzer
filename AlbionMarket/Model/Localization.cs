using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AlbionMarket.Model
{
	[XmlRoot("tu")]
	public class Localization
	{
		[Key]
		public int LocalizationId { get; set; }
		[XmlAttribute("tuid")]
		public string UniqueName { get; set; }
		[XmlArrayItem("tuv")]
		public List<Description> Descriptions { get; set; }

		public Description GetDescription(Languages language) =>
			Descriptions.Single(d => d.Language.Contains(language.ToString()));
	}

	[XmlRoot("tuv")]
	public class Description
	{
		public int DescriptionId { get; set; }
		[XmlAttribute("xml:lang")]
		public string Language { get; set; }
		[XmlElement("seg")]
		public string DescriptionText { get; set; }
		public int LocalizationId { get; set; }
		public Localization Localization { get; set; }
	}
}
