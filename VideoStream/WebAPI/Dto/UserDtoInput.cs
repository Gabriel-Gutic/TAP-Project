namespace WebAPI.Dto
{
	public class UserDtoInput
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }

		public IFormFile? Image { get; set; }
	}
}
