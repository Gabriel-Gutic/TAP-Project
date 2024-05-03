using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class Video : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public byte[] Data { get; set; }
        public bool IsPublic { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CategoryId { get; set; }
        public VideoCategory Category { get; set; }

		public ICollection<Feedback> Feedback { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<View> Views { get; set; }

		public Video()
        {
            Title = "";
            Description = "";
            Image = null;

            Feedback = new List<Feedback>();
            Comments = new List<Comment>();
            Views = new List<View>();
        }
    }
}
