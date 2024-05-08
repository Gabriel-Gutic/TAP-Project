using BlazorClient.Contracts;
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserService _userService;
        private readonly IHttpService _httpService;

        public CommentService(IUserService userService, IHttpService httpService)
        {
            _userService = userService;
            _httpService = httpService;
        }

        public async Task SendComment(string message, Guid videoId)
        {
            UserData? user = await _userService.GetCurrent();

            if (user != null)
            {
                await _httpService.Post<string>("api/Comment/Insert", new NewCommentDto(
                    message, 
                    user.Id,
                    videoId
                ));
            }
        }

        public async Task<IEnumerable<CommentData>> GetForVideo(Guid videoId)
        {
            UserData user = await _userService.GetCurrent();

            try
            {
                var comments = await _httpService.Get<IEnumerable<CommentDto>>("api/Comment/GetForVideo", "videoId", videoId);


                if (user == null)
                {
                    return comments.Select(c => new CommentData(
                            c.Id,
                            c.Message,
                            c.UserId,
                            c.Username,
                            c.VideoId,
                            c.CreatedAt
                        ));
                }

                LinkedList<CommentData> result = new LinkedList<CommentData>();
                // Bring to front the comments that belongs to the current user
                foreach (var comment in comments)
                {
                    CommentData commentData = new CommentData(
                            comment.Id,
                            comment.Message,
                            comment.UserId,
                            comment.Username,
                            comment.VideoId,
                            comment.CreatedAt
                        );

                    if (comment.Username == user.Username)
                    {
                        result.AddFirst(commentData);
                    }
                    else
                    {
                        result.AddLast(commentData);
                    }
                }

                return result.ToList();
            }
            catch
            {
                return Enumerable.Empty<CommentData>();
            }
        }

        public async Task DeleteComment(Guid id)
        {
            await _httpService.Delete<string>("api/Comment/Delete", id);
        }
    }
}
