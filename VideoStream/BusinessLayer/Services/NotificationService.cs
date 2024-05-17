using BusinessLayer.Contracts;
using BusinessLayer.Dto.Notifications;
using BusinessLayer.Exceptions;
using BusinessLayer.Notifications.Manager;
using BusinessLayer.Notifications.Types;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IUserService _userService;
        private readonly ISubscriberService _subscriberService;
        private readonly INotificationManager _notificationManager;

        public NotificationService(IUserService userService, INotificationManager notificationManager, IRepository<Notification> notificationRepository, ISubscriberService subscriberService)
        {
            _userService = userService;
            _notificationManager = notificationManager;
            _notificationRepository = notificationRepository;
            _subscriberService = subscriberService;
        }

        public IEnumerable<NotificationDto> GetAll()
        {
            return _notificationRepository.GetAll()
                .Select(n => new NotificationDto()
                {
                    Id = n.Id,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    UserId = n.UserId,
                    CreatedAt = n.CreatedAt,
                });
        }

        public void PushSubscriber(SubscriberNotificationDto data)
        {
            var subscriber = _userService.Get(data.SubscriberId);
            if (subscriber == null)
            {
                throw new EntityNotFoundException();
            }

            _notificationManager.Send(new SubscriberNotification(
                data.CreatorId,
                subscriber.Username
                ));
        }

        public void PushVideoUpload(VideoUploadNotificationDto data)
        {
            var creator = _userService.Get(data.CreatorId);
            if (creator == null)
            {
                throw new EntityNotFoundException();
            }

            var subscribers = _subscriberService.GetSubscribers(data.CreatorId);
            if (subscribers == null)
            {
                throw new EntityNotFoundException();
            }

            Queue<Guid> sendTo = new Queue<Guid>();
            foreach (var subscriber in subscribers)
            {
                sendTo.Enqueue(subscriber.SubscriberId);
            }

            while (sendTo.Count > 0)
            {
                Guid id = sendTo.Dequeue();
                _notificationManager.Send(new VideoUploadNotification(id, creator.Username));
            }
        }
    }
}
