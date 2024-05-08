using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHandler _passwordHandler;

        public AuthService(IRepository<User> userRepository, IPasswordHandler passwordHandler)
        {
            _userRepository = userRepository;
            _passwordHandler = passwordHandler;
        }

        public UserDto? FindUser(string username, string password)
        {
            var users = _userRepository.Find(u => u.Username == username);
            if (users == null || users.Count() == 0)
            {
                return null;
            }

            User user = users.First();

            if (!_passwordHandler.Verify(password, user.Password))
            {
                return null;
            }

            return new UserDto(
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Password,
                    user.ImagePath,
                    user.IsAdmin,
                    user.IsActive
                );
        }

        public UserDto? FindUser(string username)
        {
            var users = _userRepository.Find(u => u.Username == username);
            if (users == null || users.Count() == 0)
            {
                return null;
            }
            User user = users.First();

            return new UserDto(
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Password,
                    user.ImagePath,
                    user.IsAdmin,
                    user.IsActive
                );
        }
    }
}
