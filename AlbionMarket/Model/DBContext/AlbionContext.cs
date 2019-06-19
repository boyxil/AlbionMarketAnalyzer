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
		public DbSet<Localization> Localizations { get; set; }
		public DbSet<ItemRawXml> Items { get; set; }
		public DbSet<Description> Description { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=albion.db");
		}

		public DataEntity<ItemRawXml>[] GetItemsWithDescription(string[] name)
		{
			var descriptions = from description in Description
							   where name.Any(c => description.DescriptionText.Contains(c))
							   select description;

			var items = from localization in Localizations.Include(l => l.Descriptions)
						where descriptions.Any(c => c.LocalizationId == localization.LocalizationId)
						join item in Items
						on localization.UniqueName equals ("@ITEMS_" + item.UniqueName)
						select new DataEntity<ItemRawXml>(item, localization);

			return items.ToArray();
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
	}
}
