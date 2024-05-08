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
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }

		public VideoCategoryDto(Guid id, string name, DateTime createdAt) 
		{ 
			Id = id;
			Name = name;
			CreatedAt = createdAt;
		}

		public VideoCategoryDto(Guid id, string name)
			:this(id, name, DateTime.Now)
		{
		}

        public VideoCategoryDto(string name)
            : this(Guid.NewGuid(), name, DateTime.Now)
        {
        }
    }
}
