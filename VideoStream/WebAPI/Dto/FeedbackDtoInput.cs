namespace WebAPI.Dto
{
    public class FeedbackDtoInput
    {
        public bool IsPositive { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
    }
}
