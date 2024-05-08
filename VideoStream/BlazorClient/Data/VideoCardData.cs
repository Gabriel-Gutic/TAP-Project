namespace BlazorClient.Data
{
    public class VideoCardData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }

        public string Creator { get; set; }

        public VideoCardData(Guid id, string title, string imagePath, string creator)
        {
            Id = id;
            Title = title;
            ImagePath = imagePath;
            Creator = creator;
        }
    }
}
