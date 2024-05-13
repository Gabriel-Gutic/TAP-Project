using BlazorClient.Dto;

namespace BlazorClient.Contracts
{
    public interface IFeedbackService
    {
        public Task<int> CountPositive(Guid videoId);
        public Task<int> CountNegative(Guid videoId);
        public Task<bool?> FindFeedback(Guid userId, Guid videoId);
        public Task SendFeedback(Guid userId, Guid videoId, bool feedback);
        public Task ChangeFeedback(Guid userId, Guid videoId, bool feedback);
    }
}
