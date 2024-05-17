using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Notifications.Types;

namespace BusinessLayer.Notifications.Senders
{
    public class EmailNotificationSender : INotificationSender
    {
        public void Send(BaseNotification notification)
        {
            // This Sender will do nothing because I don't have real emails in 
            // my application
        }
    }
}
