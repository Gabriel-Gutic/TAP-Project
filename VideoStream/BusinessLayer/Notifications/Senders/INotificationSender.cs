using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Notifications.Types;

namespace BusinessLayer.Notifications.Senders
{
    public interface INotificationSender
    {
        public void Send(BaseNotification notification);
    }
}
