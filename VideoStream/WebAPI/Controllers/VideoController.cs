using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using WebAPI.Dto;
using WebAPI.Exceptions;
using WebAPI.File;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoController : ControllerBase
	{
		private readonly IVideoService _videoService;
		private readonly IUserService _userService;
		private readonly IFileManager _fileManager;


        public VideoController(IVideoService videoService, IUserService userService, IFileManager fileManager)
		{
			_videoService = videoService;
			_userService = userService;
			_fileManager = fileManager;
		}

		// Get information about each video
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var entities = _videoService.GetAll();

			return Ok(entities);
		}

		// Get information about all the videos uploaded 
		// by an user
		[HttpGet("GetAllForUser")]
        public IActionResult GetAllForUser(Guid userId)
		{
			var entities = _videoService.GetAllForUser(userId);

			return Ok(entities);
		}

		// Get information about a specific video
		[HttpGet("Get")]
        public IActionResult Get(Guid id)
		{
            var video = _videoService.Get(id);

			if (video == null)
			{
				return NotFound("Video not found");
			}

			var videoOut = new VideoDto(
				video.Id,
				video.Title,
				video.Description,
				video.ImagePath,
				video.Path,
				video.IsPublic,
				video.CreatedAt,
				video.UserId,
				video.CategoryId
			);

            return Ok(videoOut);
		}

		// Insert a new video in the database
		[Authorize]
		[HttpPost("Insert")]
		public async Task<IActionResult> Insert([FromForm] VideoDtoInput videoDtoInput)
		{
			var currentUser = HttpContext.User;

			string? username = null;

			if (currentUser.HasClaim(c =>
			{
				if (c.Type == ClaimTypes.NameIdentifier)
				{
					username = c.Value;
					return true;
				}
				return false;
			}))
			{
				var user = _userService.Get(videoDtoInput.UserId);
				if (user == null || user.Username != username)
                {
					return BadRequest("Invalid user data");
                }
            }
			else
			{
                return BadRequest("You don't have permission to upload a video");
            }

            string[] paths;
			try
			{
                paths = await ExtractImageAndVideo(videoDtoInput.Image, videoDtoInput.Video);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

            string imagePath = paths[0];
            string path = paths[1];

            VideoDto videoDto = new VideoDto(
					videoDtoInput.Title,
					videoDtoInput.Description,
					imagePath,
					path,
					videoDtoInput.IsPublic,
					videoDtoInput.UserId,
					videoDtoInput.CategoryId
				);

			_videoService.Insert(videoDto);

			return Ok("Video successfully inserted");
		}

        // Update an existing video
        // BadRequest if the video doesn't exist 
		// All the fields must be specified, including the Video field
        [HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, [FromForm]VideoDtoInput videoDtoInput)
		{
            string[] paths;
            try
            {
                paths = await ExtractImageAndVideo(videoDtoInput.Image, videoDtoInput.Video);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string imagePath = paths[0];
            string path = paths[1];

            VideoDto videoDto = new VideoDto(
					videoDtoInput.Title,
					videoDtoInput.Description,
					imagePath,
					path,
					videoDtoInput.IsPublic,
					videoDtoInput.UserId,
					videoDtoInput.CategoryId
				);

			try
			{
				VideoDto? video = _videoService.Get(id);
				_videoService.Update(id, videoDto);

				_fileManager.Delete(video.ImagePath);
				_fileManager.Delete(video.Path);

				return Ok("Video successfully updated");
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

        // Edit an existing video
        // BadRequest if the video doesn't exist 
        // Only Id, Title, Description, IsPublic and CategoryId must be specified
		// Image is nullable
        [HttpPatch("Edit")]
        public async Task<IActionResult> Edit([FromForm] EditVideoDtoInput editInput)
        {
			EditVideoDto editVideoDto = new EditVideoDto()
			{
				Title = editInput.Title,
				Description = editInput.Description,
				IsPublic = editInput.IsPublic,
				CategoryId = editInput.CategoryId,
				ImagePath = null,
			};

			if (editInput.Image != null)
			{
                try
                {
                    editVideoDto.ImagePath = await _fileManager.ExtractImage(editInput.Image);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            
            try
            {
                VideoDto? video = _videoService.Get(editInput.Id);
                _videoService.Edit(editInput.Id, editVideoDto);

				// Delete old image
				if (editVideoDto.ImagePath != null)
				{
					_fileManager.Delete(video.ImagePath);
				}

                return Ok("Video successfully updated");
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

        // Delete an existing video
        // BadRequest if the video doesn't exist 
        [HttpDelete("Delete")]
		public IActionResult Delete(Guid id)
		{
			try
			{
                VideoDto? video = _videoService.Get(id);

                _videoService.Delete(id);

                _fileManager.Delete(video.ImagePath);
                _fileManager.Delete(video.Path);
                return Ok("Video successfully deleted");
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

		private async Task<string[]> ExtractImageAndVideo(IFormFile image, IFormFile video)
		{
            string imagePath = null;
            string path = null;

            imagePath = await _fileManager.ExtractImage(image);
            if (imagePath == null)
            {
                throw new FileUploadException("Failed to upload image");
            }
            path = await _fileManager.ExtractVideo(video);
            if (path == null)
            {
                throw new FileUploadException("Failed to upload image");
            }

			return [imagePath, path];
        }
	}
}
