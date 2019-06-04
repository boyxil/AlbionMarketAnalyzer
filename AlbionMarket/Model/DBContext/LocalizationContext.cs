using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace AlbionMarket.Model.DBContext
{
	public class LocalizationContext : DbContext
	{
		public DbSet<Localization> Localizations { get; set; }
	}
}
