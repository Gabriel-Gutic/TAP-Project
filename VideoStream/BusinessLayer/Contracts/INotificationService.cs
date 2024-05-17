using BusinessLayer.Dto.Notifications;
using BusinessLayer.Notifications.Types;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface INotificationService
    {
        public IEnumerable<NotificationDto> GetAll();
        public void PushSubscriber(SubscriberNotificationDto data);
        public void PushVideoUpload(VideoUploadNotificationDto data);
    }
}
