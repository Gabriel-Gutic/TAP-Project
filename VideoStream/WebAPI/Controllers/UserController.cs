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

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IFileManager _fileManager;

		public UserController(IUserService userService, IFileManager fileManager)
		{
			_userService = userService;
			_fileManager = fileManager;
		}

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

		[HttpPost("Insert")]
		public async Task<IActionResult> Insert(UserDtoInput userDtoInput)
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
				userDtoInput.IsAdmin,
				userDtoInput.IsActive
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

		[HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, UserDtoInput userDtoInput)
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
				userDtoInput.IsAdmin,
				userDtoInput.IsActive
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
