using BusinessLayer.Notifications.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Notifications.Factory
{
    public interface INotificationFactory
    {
        INotificationSender CreateSender(string name);
    }
}
