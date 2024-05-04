﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class FullVideoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public byte[] Data { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public FullVideoDto(Guid id, string title, string description, byte[] image, byte[] data, bool isPublic, DateTime createdAt, Guid userId, Guid categoryId) 
        { 
            Id = id;
            Title = title;
            Description = description;
            Image = image;
            Data = data;
            IsPublic = isPublic;
            UserId = userId;
            CategoryId = categoryId;
            CreatedAt = createdAt;
        }

        public FullVideoDto(Guid id, string title, string description, byte[] image, byte[] data, bool isPublic, Guid userId, Guid categoryId)
            : this(id, title, description, image, data, isPublic, DateTime.Now, userId, categoryId)
        {
        }
    }
}