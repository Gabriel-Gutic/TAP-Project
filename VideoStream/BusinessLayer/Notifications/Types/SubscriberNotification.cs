using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Notifications.Types
{
    public class SubscriberNotification : BaseNotification
    {
        protected string _subscriber;

        public SubscriberNotification(Guid userId, string subscriber)
            : base(userId)
        {
            _subscriber = subscriber;
        }

        public override string Message
        {
            get
            {
                return $"{_subscriber} just subscribed to the channel!";
            }
        }
    }
}
