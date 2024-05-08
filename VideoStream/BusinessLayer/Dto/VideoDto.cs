using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public VideoDto(Guid id, string title, string description, string imagePath, string path, bool isPublic, DateTime createdAt, Guid userId, Guid categoryId) 
        { 
            Id = id;
            Title = title;
            Description = description;
            ImagePath = imagePath;
            Path = path;
            IsPublic = isPublic;
            UserId = userId;
            CategoryId = categoryId;
            CreatedAt = createdAt;
        }

        public VideoDto(Guid id, string title, string description, string imagePath, string path, bool isPublic, Guid userId, Guid categoryId)
            : this(id, title, description, imagePath, path, isPublic, DateTime.Now, userId, categoryId)
        {
        }

        public VideoDto(string title, string description, string imagePath, string path, bool isPublic, Guid userId, Guid categoryId)
            : this(Guid.NewGuid(), title, description, imagePath, path, isPublic, DateTime.Now, userId, categoryId)
        {
        }
    }
}
