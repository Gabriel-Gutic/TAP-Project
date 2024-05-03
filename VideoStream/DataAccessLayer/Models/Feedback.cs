using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class Feedback : BaseEntity
    {
        public bool IsPositive { get; set; }

        public Guid UserId {  get; set; }
        public User User { get; set; }

        public Guid VideoId { get; set; }
        public Video Video { get; set; }

        public Feedback() 
        { 
            IsPositive = true;
        }
	}
}
