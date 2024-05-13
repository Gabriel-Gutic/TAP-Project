 namespace BlazorClient.Data
{
    public class VideoData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        // Creator
        public Guid CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public string? CreatorImagePath { get; set; }
        public int CreatorSubscriberCount { get; set; }

        // Views
        public int ViewCount { get; set; }

        // Feedback
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
    }
}
