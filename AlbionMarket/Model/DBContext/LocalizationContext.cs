using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AlbionMarket.Model.DBContext
{
	public class LocalizationContext : DbContext
	{
		public DbSet<Localization> Localizations { get; set; }
	}
}
