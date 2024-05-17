using BlazorClient.Contracts;
using BlazorClient.Events;

namespace BlazorClient.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IHttpService _httpService;
        private readonly IUserService _userService;
        private readonly IEventController _eventController;

        public SubscriberService(IHttpService httpService, IUserService userService, IEventController eventController)
        {
            _httpService = httpService;
            _userService = userService;
            _eventController = eventController;
        }

        public async Task<int> Count(Guid creatorId)
        {
            try
            {
                return await _httpService.Get<int>("api/Subscriber/Count", "creatorId", creatorId);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> IsSubscriber(Guid creatorId, Guid subscriberId)
        {
            try
            {
                return await _httpService.Post<bool>("api/Subscriber/IsSubscriber", new
                {
                    CreatorId = creatorId,
                    SubscriberId = subscriberId,
                });
            }
            catch
            {
                return false;
            }
        }

        public async Task Subscribe(Guid creatorId, Guid subscriberId)
        {
            try
            {
                var data = new
                {
                    CreatorId = creatorId,
                    SubscriberId = subscriberId,
                };
                await _httpService.Post<string>("api/Subscriber/Subscribe", data);
                await _eventController.Invoke("Subscribe", data);
            }
            catch
            {

            }
        }

        public async Task Unsubscribe(Guid creatorId, Guid subscriberId)
        {
            try
            {
                await _httpService.DeleteContent<string>("api/Subscriber/Unsubscribe", new
                {
                    CreatorId = creatorId,
                    SubscriberId = subscriberId,
                });
            }
            catch
            {

            }
        }
    }
}
