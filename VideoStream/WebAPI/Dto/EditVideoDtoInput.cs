namespace WebAPI.Dto
{
    public class EditVideoDtoInput
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
