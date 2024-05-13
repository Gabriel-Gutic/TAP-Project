using BlazorClient.Contracts;

namespace BlazorClient.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IHttpService _httpService;
        private readonly IUserService _userService;

        public SubscriberService(IHttpService httpService, IUserService userService)
        {
            _httpService = httpService;
            _userService = userService;
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
                await _httpService.Post<string>("api/Subscriber/Subscribe", new
                {
                    CreatorId = creatorId,
                    SubscriberId = subscriberId,
                });
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
