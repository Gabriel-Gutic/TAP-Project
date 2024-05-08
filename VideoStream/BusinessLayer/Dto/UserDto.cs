using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserDto(Guid id, string username, string email, string password, string? imagePath, bool isAdmin, bool isActive, DateTime createdAt)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            ImagePath = imagePath;
            IsAdmin = isAdmin;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public UserDto(Guid id, string username, string email, string password, string? imagePath, bool isAdmin, bool isActive)
            : this(id, username, email, password, imagePath, isAdmin, isActive, DateTime.Now)
        {
        }

        public UserDto(string username, string email, string password, string? imagePath, bool isAdmin, bool isActive)
            : this(Guid.NewGuid(), username, email, password, imagePath, isAdmin, isActive, DateTime.Now)
        {
        }
    }
}
