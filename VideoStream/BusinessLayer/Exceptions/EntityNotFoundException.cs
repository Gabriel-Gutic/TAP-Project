﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(string message = "Entity not found")
			:base(message)
		{ 
			
		}
	}
}