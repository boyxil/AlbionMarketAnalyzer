using System;
using System.Collections.Generic;
using System.Text;

namespace AlbionMarket.Model
{
	public class DataEntity<T>
	{
		public T Entity { get; set; }
		public Localization Localization { get; set; }

		public DataEntity(T entity, Localization localization)
		{
			Entity = entity;
			Localization = localization;
		}
	}
}
