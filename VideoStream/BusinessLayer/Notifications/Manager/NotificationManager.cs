using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Notifications.Factory;
using BusinessLayer.Notifications.Senders;
using BusinessLayer.Notifications.Types;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Notifications.Manager
{
    // This I wanted to be a Singleton, but it didn't work, so
    // I let the Singleton in Blazor Project
    public class NotificationManager : INotificationManager
    {
        private IList<INotificationSender> _senders;

        public NotificationManager(INotificationFactory notificationFactory)// INotificationFactory notificationFactory)
        {
            _senders = new List<INotificationSender>();
            SetupFromFactory(notificationFactory);
        }

        private void SetupFromFactory(INotificationFactory notificationFactory)
        {
            _senders.Add(notificationFactory.CreateSender("Database"));
            _senders.Add(notificationFactory.CreateSender("Email"));
        }

        public void Send(BaseNotification notification)
        {
            foreach (var sender in _senders)
            {
                sender.Send(notification);
            }
        }
    }
}
