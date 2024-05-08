using BlazorClient.Data;

namespace BlazorClient.Contracts
{
    public interface ICommentService
    {
        public Task SendComment(string message, Guid videoId);
        public Task DeleteComment(Guid id);
        public Task<IEnumerable<CommentData>> GetForVideo(Guid videoId); 
    }
}
