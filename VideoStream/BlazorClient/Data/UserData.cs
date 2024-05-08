using BlazorClient.Dto;

namespace BlazorClient.Data
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserData(Guid id, string username, string email, string password, string? imagePath, bool isAdmin, bool isActive, DateTime createdAt)
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
    }
}
