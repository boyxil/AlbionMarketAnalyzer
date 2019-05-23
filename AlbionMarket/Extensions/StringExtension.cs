using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AlbionMarket.Extensions
{
	public static class StringExtension
	{
		public static Stream ToStream(this string value)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(value);
			return new MemoryStream(byteArray);
		}
	}
}
