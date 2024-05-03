using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class VideoCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Video> Videos { get; }

        public VideoCategory(string name = "") 
        { 
            Name = name;
            Videos = new List<Video>();
        }
    }
}
