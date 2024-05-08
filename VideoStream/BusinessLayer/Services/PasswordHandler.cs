using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public class PasswordHandler : IPasswordHandler
	{
		public string Hash(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool Verify(string password, string hash)
		{
			return BCrypt.Net.BCrypt.Verify(password, hash);
		}
	}
}
