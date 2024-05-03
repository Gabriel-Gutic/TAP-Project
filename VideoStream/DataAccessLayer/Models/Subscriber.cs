using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class Subscriber : BaseEntity
    {
        public Guid SubscriberUserId { get; set; }
        public User SubscriberUser {  get; set; }

        public Guid CreatorUserId { get; set; }
        public User CreatorUser {  get; set; }
    }
}
