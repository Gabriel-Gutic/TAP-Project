using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;


namespace DataAccessLayer.Tools
{
	public class ImageHandler : IImageHandler
	{
		public bool IsValidFormat(byte[] image)
		{
			throw new NotImplementedException();
		}

		public byte[] Read(string imagePath)
		{
			return File.ReadAllBytes(imagePath);
		}

		public byte[] Read(string location, string name)
		{
			string[] supportedExtensions =
			{
				"png",
				"jpg",
				"jpeg"
			};

			foreach (string extension in supportedExtensions)
			{
				string imagePath = location + @"\" + name + "." + extension;
				if (File.Exists(imagePath))
				{
					return Read(imagePath);
				}
			}

			throw new FileNotFoundException($"No image with name '{name}' was found in directory '{location}'");
		}

		public bool Read(string location, string name, out byte[]? image)
		{
			try
			{
				image = Read(location, name);
				return true;
			}
			catch
			{
				image = null;
				return false;
			}
		}
	}
}
