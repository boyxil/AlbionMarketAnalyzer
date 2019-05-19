using System;
using System.Collections.Generic;
using System.Text;

namespace AlbionMarket.Model
{
	public class ItemRawXml
	{
		//uniquename//
		public string UniqueName { get; set; }
		//shopcategory//
		public string ShopCategory { get; set; }
		//shopsubcategory1//
		public string ShopSubCategory { get; set; }
		//kind
		public string Kind { get; set; }
		//wight
		public string Weight { get; set; }
		//shopcategory = "farmables" shopsubcategory1="seed" kind="plant" weight=
	}
}
