using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

namespace AlbionMarket.Model
{
	/// <summary>
	/// This class is used only during Json deserialization
	/// </summary>
	public class ItemRawJson
	{
		public string LocalizationNameVariable;
		public string LocalizationDescriptionVariable;
		public string UniqueName;
		public IEnumerable<Record> LocalizedNames;
		public IEnumerable<Record> LocalizedDescriptions;
	}

	public class Record
	{
		[DefaultValue(" ")]
		public string Key { get; set; }
		[DefaultValue(" ")]
		public string Value { get; set; }
	}
}
