namespace BlazorClient.Dto
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
