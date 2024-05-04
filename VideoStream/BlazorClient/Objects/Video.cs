namespace BlazorClient.Objects
{
    public class Video
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public byte[] Data { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public Video(Guid id, string title, string description, byte[] image, byte[] data, bool isPublic, Guid userId, Guid categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            Image = image;
            Data = data;
            IsPublic = isPublic;
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
