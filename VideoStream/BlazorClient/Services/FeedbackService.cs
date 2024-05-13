using BlazorClient.Contracts;
using BlazorClient.Dto;

namespace BlazorClient.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IHttpService _httpService;

        public FeedbackService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task SendFeedback(Guid userId, Guid videoId, bool feedback)
        {
            await _httpService.Post<string>("api/Feedback/Insert", new 
            {
                UserId = userId, 
                VideoId = videoId, 
                IsPositive = feedback 
            });
        }

        public async Task ChangeFeedback(Guid userId, Guid videoId, bool feedback)
        {
            await _httpService.Patch<string>("api/Feedback/Edit", new
            {
                UserId = userId,
                VideoId = videoId,
                IsPositive = feedback
            });
        }

        public async Task<int> CountPositive(Guid videoId)
        {
            return await _httpService.Get<int>("api/Feedback/CountPositive", "videoId", videoId);
        }

        public async Task<int> CountNegative(Guid videoId)
        {
            return await _httpService.Get<int>("api/Feedback/CountNegative", "videoId", videoId);
        }

        public async Task<bool?> FindFeedback(Guid userId, Guid videoId)
        {
            try
            {
                var feedbackDto = await _httpService.Get<FeedbackDto?>("api/Feedback/Find", "userId", userId, "videoId", videoId);
                return feedbackDto.IsPositive;
            }
            catch 
            {
                return null;
            }
        }
    }
}
