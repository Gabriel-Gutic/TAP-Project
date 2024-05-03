using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.File;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

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
				.Select(u => new UserDtoOutput(
						u.Username,
						u.Email,
						u.Password, 
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

			var userOut = new UserDtoOutput(
					user.Username,
					user.Email,
					user.Password,
					user.IsAdmin,
					user.IsActive,
					user.CreatedAt
				);

			return Ok(userOut);
		}

		[HttpGet("GetImage")]
		public IActionResult GetImage(Guid id)
		{
			var user = _userService.Get(id);

			if (user == null)
			{
				return NotFound("User not found");
			}

			if (user.Image == null)
			{
				return NotFound("Selected user does not have an image");
			}

			return File(user.Image, "image/png");
		}

		[HttpPost("Insert")]
		public async Task<IActionResult> Insert(UserDtoInput userDtoInput)
		{
			byte[]? bytes = null;
			try
			{
				bytes = await _fileManager.Read(userDtoInput.Image);
			}
			catch
			{
				return BadRequest("An error occured in image uploading");
			}

			UserDto userDto = new UserDto(
				userDtoInput.Username,
				userDtoInput.Email,
				userDtoInput.Password,
				bytes,
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
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, UserDtoInput userDtoInput)
		{
			byte[]? bytes = null;
			try
			{
				bytes = await _fileManager.Read(userDtoInput.Image);
			}
			catch
			{
				return BadRequest("An error occured in image uploading");
			}

			UserDto userDto = new UserDto(
				userDtoInput.Username,
				userDtoInput.Email,
				userDtoInput.Password,
				bytes,
				userDtoInput.IsAdmin,
				userDtoInput.IsActive
			);

			try
			{
				_userService.Update(id, userDto);
				return Ok("User successfully updated");
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

		[HttpDelete("Delete")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				_userService.Delete(id);
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
