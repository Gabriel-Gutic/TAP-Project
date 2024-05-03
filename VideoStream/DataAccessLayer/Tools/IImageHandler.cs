using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Tools
{
	public interface IImageHandler
	{
		public byte[] Read(string imagePath);
		public byte[] Read(string location, string name);
		public bool Read(string location, string name, out byte[]? image);
	}
}
