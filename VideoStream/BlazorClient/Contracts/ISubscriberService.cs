namespace BlazorClient.Contracts
{
    public interface ISubscriberService
    {
        public Task<int> Count(Guid creatorId);

        public Task Subscribe(Guid creatorId, Guid subscriberId);

        public Task Unsubscribe(Guid creatorId, Guid subscriberId);

        public Task<bool> IsSubscriber(Guid creatorId, Guid subscriberId);
    }
}
