using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Linq;
using AlbionMarket.Extensions;

namespace AlbionMarket.Model
{
	[XmlRoot("items")]
	public class ItemsRawXml : IXmlSerializable
	{
		public IEnumerable<ItemRawXml> Items = new List<ItemRawXml>();

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			List<ItemRawXml> result = new List<ItemRawXml>();
			while (reader.Read())
			{
				if (reader.Name == "farmableitem")
					result.AddRange(reader.GetListOf<ItemRawXml>("farmableitem"));
			}
			Items = result;
		}

		public void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}

	[XmlRoot("farmableitem")]
	public class ItemRawXml
	{
		//uniquename//
		[XmlAttribute("uniquename")]
		public string UniqueName { get; set; }
		//shopcategory//
		[XmlAttribute("shopcategory")]
		public string ShopCategory { get; set; }
		//shopsubcategory1//
		[XmlAttribute("shopsubcategory1")]
		public string ShopSubCategory { get; set; }
		//kind
		[XmlAttribute("kind")]
		public string Kind { get; set; }
		//weight
		[XmlAttribute("weight")]
		public string Weight { get; set; }
		//shopcategory = "farmables" shopsubcategory1="seed" kind="plant" weight=
	}
}
