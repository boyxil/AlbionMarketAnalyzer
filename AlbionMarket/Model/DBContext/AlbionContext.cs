using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace AlbionMarket.Model.DBContext
{
	public class AlbionContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=albion.db");
		}

		public static dynamic GetItemsWithDescription()
		{
			using (var db = new AlbionContext())
			{
				var query = from item in db.Items
				join localization in db.Localizations.Include(d => d.Descriptions)
				on ("@ITEMS_" + item.UniqueName) equals localization.UniqueName
				select new { item, localization };

				return query.ToArray();
			}
		}

		public static dynamic GetItemsWithDescription(string name)
		{
			using (var db = new AlbionContext())
			{
				var query = from item in db.Items
							join localization in db.Localizations.Include(d => d.Descriptions)
							on ("@ITEMS_" + item.UniqueName) equals localization.UniqueName
							where localization.Descriptions[0].DescriptionText.Contains(name)
							select new { item, localization };

				return query.ToArray();
			}
		}

		public static void UpDataBase()
		{
			var localizations = ConvertFileContentToObject<LocalizationXmls>("XmlFiles/localization.xml");
			var items = ConvertFileContentToObject<ItemsRawXml>("XmlFiles/items.xml");

			using (var db = new AlbionContext())
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

		public AlbionContext(DbContextOptions options): base(options) { }

		public AlbionContext() : base() { }
		public DbSet<Localization> Localizations { get; set; }
		public DbSet<ItemRawXml> Items { get; set; }
	}
}
