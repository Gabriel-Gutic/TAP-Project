using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public class UserService : IUserService
	{
		private readonly IRepository<User> _userRepository;
		private readonly IAppLogger _logger;
		private readonly IPasswordHandler _passwordHandler;

		public UserService(IRepository<User> userRepository, IAppLogger logger, IPasswordHandler passwordHandler) 
		{ 
			_userRepository = userRepository;
			_logger = logger;
			_passwordHandler = passwordHandler;
		}

		public IEnumerable<UserDto> GetAll()
		{
			return _userRepository.GetAll()
				.Select(u => new UserDto(
					u.Id,
					u.Username,
					u.Email,
					u.Password,
					u.ImagePath,
					u.IsAdmin,
					u.IsActive,
					u.CreatedAt
				));
		}

		public UserDto? Get(Guid id)
		{
			var user = _userRepository.GetById(id);
			if (user == null)
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
				user.IsActive,
				user.CreatedAt
			);
		}

		public void Insert(UserDto userDto)
		{
			if (_userRepository.Contains(u => u.Username == userDto.Username))
			{
				throw new Exception("Username already used");
			}
			if (_userRepository.Contains(u => u.Email == userDto.Email))
			{
				throw new Exception("Email already used");
			}

			_userRepository.Add(new User()
			{
				Username = userDto.Username,
				Email = userDto.Email,
				Password = _passwordHandler.Hash(userDto.Password),
                ImagePath = userDto.ImagePath,
				IsAdmin = userDto.IsAdmin,
				IsActive = userDto.IsActive
			});

			_userRepository.SaveChanges();

			_logger.Info("New item inserted in User Table");
		}

		public void Update(Guid id, UserDto userDto)
		{
			if (_userRepository.Count(u => u.Username == userDto.Username) > 1)
			{
				throw new Exception("Username already used");
			}
			if (_userRepository.Count(u => u.Email == userDto.Email) > 1)
			{
				throw new Exception("Email already used");
			}

			User user = _userRepository.GetById(id);
			if (user == null)
			{
				throw new EntityNotFoundException("User not found");
			}

			user.Username = userDto.Username;
			user.Email = userDto.Email;
			user.Password = _passwordHandler.Hash(userDto.Password);
			user.ImagePath = userDto.ImagePath;
			user.IsAdmin = userDto.IsAdmin;
			user.IsActive = userDto.IsActive;

			_userRepository.SaveChanges();

			_logger.Info("Item updated in User Table");
		}

		public void Delete(Guid id)
		{
			if (!_userRepository.Delete(id))
			{
				throw new EntityNotFoundException("User not found");
			}
			_userRepository.SaveChanges();

			_logger.Info("Item deleted from User Table");
		}
    }
}
