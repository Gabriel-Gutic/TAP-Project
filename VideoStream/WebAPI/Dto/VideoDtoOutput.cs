namespace WebAPI.Dto
{
	public class VideoDtoOutput
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
		public DateTime CreatedAt { get; set; }

		public Guid UserId { get; set; }
		public Guid CategoryId { get; set; }

		public VideoDtoOutput(string title, string description, bool isPublic, DateTime createdAt, Guid userId, Guid categoryId) 
		{ 
			Title = title;
			Description = description;
			IsPublic = isPublic;
			CreatedAt = createdAt;
			UserId = userId;
			CategoryId = categoryId;
		}
	}
}
