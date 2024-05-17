using BusinessLayer.Logger;
using BusinessLayer.Notifications.Senders;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Notifications.Factory
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IAppLogger _logger;


        public NotificationFactory(IRepository<Notification> notificationRepository, IAppLogger logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public INotificationSender CreateSender(string name)
        {
            switch (name)
            {
                case "Database":
                    return new DatabaseNotificationSender(_notificationRepository, _logger);
                case "Email":
                    return new EmailNotificationSender();
            }
            return new UnknownNotificationSender();
        }
    }
}
