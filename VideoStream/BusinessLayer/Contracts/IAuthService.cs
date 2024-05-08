using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IAuthService
    {
        public UserDto? FindUser(string username, string password);
        public UserDto? FindUser(string username);
    }
}
