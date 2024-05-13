using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class SubscriberDto
    {
        public Guid CreatorId {  get; set; }
        public string CreatorUsername { get; set; }
        public Guid SubscriberId { get; set;}
        public string SubscriberUsername { get; set; }
    }
}
