﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class Notification : BaseEntity
	{
		public string Message { get; set; }
		public bool IsRead { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }

		public Notification() 
		{ 
			Message = "";
			IsRead = false;
		}
	}
}
