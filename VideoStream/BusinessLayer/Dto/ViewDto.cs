using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
	public class ViewDto
	{
		public Guid UserId { get; set; }
		public Guid VideoId { get; set; }
		public DateTime CreatedAt { get; set; }

		public ViewDto(Guid userId, Guid videoId, DateTime createdAt) 
		{ 
			UserId = userId;
			VideoId = videoId;
			CreatedAt = createdAt;
		}

		public ViewDto(Guid userId, Guid videoId) 
			:this(userId, videoId, DateTime.Now)
		{ 
		}
	}
}
