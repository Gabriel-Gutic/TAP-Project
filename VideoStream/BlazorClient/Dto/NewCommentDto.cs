namespace BlazorClient.Dto
{
    public class NewCommentDto
    {
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public NewCommentDto(string message, Guid userId, Guid videoId)
        {
            Message = message;
            UserId = userId;
            VideoId = videoId;
        }
    }
}
