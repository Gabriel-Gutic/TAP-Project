using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Notifications
{
    public class SubscriberNotificationDto
    {
        public Guid CreatorId { get; set; }
        public Guid SubscriberId { get; set; }
    }
}
