namespace WebAPI.Dto
{
    public class CommentDtoInput
    {
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
    }
}
