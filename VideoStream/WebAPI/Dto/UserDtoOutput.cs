namespace WebAPI.Dto
{
	public class UserDtoOutput
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }

		public UserDtoOutput(string username, string email, string password, bool isAdmin, bool isActive, DateTime createdAt) 
		{ 
			Username = username;
			Email = email;
			Password = password;
			IsActive = isActive;
			IsAdmin = isAdmin;
			CreatedAt = createdAt;
		}
	}
}
