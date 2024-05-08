namespace BlazorClient.Data
{
    public class CommentData
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public Guid VideoId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public CommentData(Guid id, string message, Guid userId, string username, Guid videoId, DateTime createdAt) 
        { 
            Id = id;
            Message = message;
            UserId = userId;
            Username = username;
            VideoId = videoId;
            CreatedAt = createdAt;
        }
    }
}
