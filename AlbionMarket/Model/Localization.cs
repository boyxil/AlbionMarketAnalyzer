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
		public int Id { get; set; }
		[XmlAttribute("tuid")]
		public string UniqueName { get; set; }
		[XmlArrayItem("tuv")]
		public List<Description> Descriptions { get; set; }
	}

	[XmlRoot("tuv")]
	public class Description
	{
		[Key]
		public int Id { get; set; }
		[XmlAttribute("xml:lang")]
		public string Language { get; set; }
		[XmlElement("seg")]
		public string DescriptionText { get; set; }
	}
}
