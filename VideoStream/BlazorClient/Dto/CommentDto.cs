﻿namespace BlazorClient.Dto
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid VideoId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
