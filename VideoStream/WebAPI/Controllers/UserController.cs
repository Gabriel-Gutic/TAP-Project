using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.File;
using System.IO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Logger;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IFileManager _fileManager;
		private readonly IAppLogger _logger;

		public UserController(IUserService userService, IFileManager fileManager, IAppLogger appLogger)
		{
			_userService = userService;
			_fileManager = fileManager;
			_logger = appLogger;
		}

        // Get information about all the users
        [HttpGet("GetAll")]
		public IActionResult GetAll() 
		{
			var entities = _userService.GetAll()
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

			return Ok(entities);
		}

        // Get information about a specific user
		[HttpGet("Get")]
        public IActionResult Get(Guid id)
		{
			var user = _userService.Get(id);

			if (user == null)
			{
				return NotFound("User not found");
			}

			var userOut = new UserDto(
				user.Id,
				user.Username,
				user.Email,
				user.Password,
				user.ImagePath,
				user.IsAdmin,
				user.IsActive,
				user.CreatedAt
			);

			return Ok(userOut);
		}

        // Check if a user with the specific username exists
		[HttpGet("IsUsernameValid")]
        public IActionResult IsUsernameValid(string username)
		{
			if (_userService.IsUsernameUsed(username))
			{
				return Ok(false);
			}

			return Ok(true);
		}

        // Check if a user with the specific email exists
        [HttpGet("IsEmailValid")]
        public IActionResult IsEmailValid(string email)
        {
            if (_userService.IsEmailUsed(email))
            {
                return Ok(false);
            }

            return Ok(true);
        }

        // Insert a new user in the database
		// The Image is nullable
        [AllowAnonymous]
        [HttpPost("Insert")]
		public async Task<IActionResult> Insert([FromForm]UserDtoInput userDtoInput)
		{
            string? imagePath = null;
			try
			{
                imagePath = await _fileManager.ExtractImage(userDtoInput.Image);
			}
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
			}

			UserDto userDto = new UserDto(
				userDtoInput.Username,
				userDtoInput.Email,
				userDtoInput.Password,
				imagePath,
				false,
				true
			);
			
			try
			{
				_userService.Insert(userDto);
				return Ok("User successfully inserted");
			}
			catch (Exception ex)
			{
				if (imagePath != null)
				{
					_fileManager.Delete(imagePath);
				}
				return BadRequest(ex.Message);
			}
		}

        // Update an existing user
        // BadRequest if the user doesn't exist 
        [HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, [FromForm]UserDtoInput userDtoInput)
		{
            string? imagePath = null;
            try
            {
                imagePath = await _fileManager.ExtractImage(userDtoInput.Image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            UserDto userDto = new UserDto(
				userDtoInput.Username,
				userDtoInput.Email,
				userDtoInput.Password,
				imagePath,
				false,
				true
			);

			try
			{
				UserDto? user = _userService.Get(id);
				string? oldImagePath = user.ImagePath;

				_userService.Update(id, userDto);

				if (oldImagePath != null)
				{
					_fileManager.Delete(oldImagePath);
				}

				return Ok("User successfully updated");
			}
			catch (EntityNotFoundException ex)
			{
				if (imagePath != null)
				{
					_fileManager.Delete(imagePath);
				}
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
                if (imagePath != null)
                {
                    _fileManager.Delete(imagePath);
                }
                return BadRequest(ex.Message);
			}
		}

        // Delete an existing user
        // BadRequest if the user doesn't exist 
        [HttpDelete("Delete")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				UserDto? user = _userService.Get(id);
				string? imagePath = user.ImagePath;

				_userService.Delete(id);
				_fileManager.Delete(imagePath);
                return Ok("User successfully deleted");
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
