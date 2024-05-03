using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Tools
{
	public class VideoHandler : IVideoHandler
	{
		public byte[] Read(string videoPath)
		{
			return File.ReadAllBytes(videoPath);
		}

		public byte[] Read(string location, string name)
		{
			string[] supportedExtensions =
{
				"mp4",
				"fov",
				"fav",
				"mov",
			};

			foreach (string extension in supportedExtensions)
			{
				string videoPath = location + @"\" + name + "." + extension;
				if (File.Exists(videoPath))
				{
					return Read(videoPath);
				}
			}

			throw new FileNotFoundException($"No video with name '{name}' was found in directory '{location}'");
		}

		public bool Read(string location, string name, out byte[]? video)
		{
			try
			{
				video = Read(location, name);
				return true;
			}
			catch
			{
				video = null;
				return false;
			}
		}
	}
}
