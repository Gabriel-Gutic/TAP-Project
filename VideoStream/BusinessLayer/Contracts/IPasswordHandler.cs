﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IPasswordHandler
	{
		public string Hash(string password);

		public bool Verify(string password, string hash);
	}
}