using DataAccessLayer.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
	public class ViewDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid VideoId { get; set; }
		public DateTime CreatedAt { get; set; }

		public ViewDto(Guid id, Guid userId, Guid videoId, DateTime createdAt) 
		{
			Id = id;
			UserId = userId;
			VideoId = videoId;
			CreatedAt = createdAt;
		}

		public ViewDto(Guid id, Guid userId, Guid videoId) 
			:this(id, userId, videoId, DateTime.Now)
		{ 
		}
        public ViewDto(Guid userId, Guid videoId)
            : this(Guid.NewGuid(), userId, videoId, DateTime.Now)
        {
        }

    }
}
