using BusinessLayer.Logger;
using BusinessLayer.Notifications.Types;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Notifications.Senders
{
    public class DatabaseNotificationSender : INotificationSender
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IAppLogger _logger;

        public DatabaseNotificationSender(IRepository<Notification> notificationRepository, IAppLogger logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public void Send(BaseNotification notification)
        {
            _notificationRepository.Add(new Notification()
            {
                UserId = notification.UserId,
                Message = notification.Message,
            });
            _notificationRepository.SaveChanges();
            _logger.Info("New item inserted in Notification Table");
        }
    }
}
