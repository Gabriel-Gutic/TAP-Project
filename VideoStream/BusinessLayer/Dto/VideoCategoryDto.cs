using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
	public class VideoCategoryDto
	{
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }

		public VideoCategoryDto(string name, DateTime createdAt) 
		{ 
			Name = name;
			CreatedAt = createdAt;
		}

		public VideoCategoryDto(string name)
			:this(name, DateTime.Now)
		{
		}
	}
}
