using System;
using System.Net.Http;
using System.IO;

namespace AlbionMarket
{
	class Program
	{
		static void Main(string[] args)
		{

		}

		public static void UpdateItemsFile()
		{
			var a = AlbionHttpClient.GetItems();
			File.WriteAllText("Items.json", a);
		}
	}
}
