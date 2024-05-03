using BusinessLayer.Dto;
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

		public void Insert(UserDto userDto);

		public void Update(Guid id, UserDto userDto);

		public void Delete(Guid id);
	}
}
