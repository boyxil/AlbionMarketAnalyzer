using AlbionMarket.Extensions;
using AlbionMarket.Model;
using AlbionMarket.Model.DBContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AlbionMarket
{
	public static class UpdateDataBase
	{
		public static void UpDataBase()
		{
			var localizations = ConvertFileContentToObject<LocalizationXmls>("XmlFiles/localization.xml");
			var items = ConvertFileContentToObject<ItemsRawXml>("XmlFiles/items.xml");

			using (var db = new LocalizationContext())
			{
				db.Localizations.Clear();
				db.Items.Clear();
			}

			using (var db = new LocalizationContext())
			{
				db.Localizations.AddRange(localizations.Localizations);
				db.Items.AddRange(items.Items);
				db.SaveChanges();
			}
		}

		private static T ConvertFileContentToObject<T>(string filePath)
		{
			FileStream myFileStream2 = new FileStream(filePath, FileMode.Open);
			XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(T));
			return (T)xmlSerializer2.Deserialize(myFileStream2);
		}
	}
}
