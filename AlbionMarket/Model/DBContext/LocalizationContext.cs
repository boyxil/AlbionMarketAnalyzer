using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AlbionMarket.Model.DBContext
{
	public class LocalizationContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=albion.db");
		}

		public LocalizationContext(DbContextOptions options): base(options) { }

		public LocalizationContext() : base() { }
		public DbSet<Localization> Localizations { get; set; }
		public DbSet<ItemRawXml> Items { get; set; }
	}
}
