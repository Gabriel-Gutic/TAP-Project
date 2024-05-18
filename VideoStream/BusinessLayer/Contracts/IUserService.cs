using BusinessLayer.Dto;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IUserService
	{
		public IEnumerable<UserDto> GetAll();

		public UserDto? Get(Guid id);
		public UserDto? Get(string? username);

        public bool IsUsernameUsed(string username);
		public bool IsEmailUsed(string email);

        public void Insert(UserDto userDto);

		public void Update(Guid id, UserDto userDto);

		public void Delete(Guid id);
	}
}
