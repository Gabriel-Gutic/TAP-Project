using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Tools
{
	public interface IVideoHandler
	{
		public byte[] Read(string videoPath);
		public byte[] Read(string location, string name);
		public bool Read(string location, string name, out byte[]? video);
	}
}
