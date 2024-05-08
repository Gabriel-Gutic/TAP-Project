using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Video> Videos { get; set; }

        public ICollection<Feedback> Feedback { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<View> Views { get; set; }

        // Subscribers
        public List<User> Creators { get; set; }
        public List<User> Subscribers { get; set; }

        // Notifications
        public ICollection<Notification> Notifications { get; set; }
        public User() 
        {
            Username = "";
            Password = "";
            ImagePath = null;
            IsAdmin = false;
            IsActive = true;

            Videos = new List<Video>();
            Feedback = new List<Feedback>();
            Comments = new List<Comment>();
            Views = new List<View>();
            Notifications = new List<Notification>();
        }
    }
}
