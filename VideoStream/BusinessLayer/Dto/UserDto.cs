using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
	public class UserDto
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public byte[]? Image { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }

		public UserDto(string username, string email, string password, byte[]? image, bool isAdmin, bool isActive, DateTime createdAt) 
		{ 
			Username = username;
			Email = email;
			Password = password;
			Image = image;
			IsAdmin = isAdmin;
			IsActive = isActive;
			CreatedAt = createdAt;
		}

		public UserDto(string username, string email, string password, byte[]? image, bool isAdmin, bool isActive)
			:this(username, email, password, image, isAdmin, isActive, DateTime.Now)
		{
		}
	}
}
