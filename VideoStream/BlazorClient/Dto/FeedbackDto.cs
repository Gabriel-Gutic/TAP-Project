namespace BlazorClient.Dto
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public bool IsPositive { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
