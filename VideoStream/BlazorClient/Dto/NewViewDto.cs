namespace BlazorClient.Dto
{
    public class NewViewDto
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public NewViewDto(Guid userId, Guid videoId) 
        { 
            UserId = userId;
            VideoId = videoId;
        }
    }
}
