using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AlbionMarket.Extensions
{
	public static class EntityExtensions
	{
		public static void Clear<T>(this DbSet<T> dbSet) where T : class
		{
			if(dbSet.Local.Count != 0)
				dbSet.RemoveRange(dbSet);
		}
	}
}
