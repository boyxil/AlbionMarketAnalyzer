using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AlbionMarket.Model
{
	[XmlRoot("items")]
	public class ItemsRawXml : IXmlSerializable
	{
		[XmlAnyElementAttribute]
		public ItemRawXml[] Items;

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		public void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}

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
