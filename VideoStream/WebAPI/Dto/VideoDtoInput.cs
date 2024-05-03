namespace WebAPI.Dto
{
	public class VideoDtoInput
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }

		public Guid UserId { get; set; }
		public Guid CategoryId { get; set; }

		public IFormFile Video {  get; set; }
		public IFormFile Image { get; set; }
	}
}
