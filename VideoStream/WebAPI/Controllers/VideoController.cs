using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebAPI.Dto;
using WebAPI.Exceptions;
using WebAPI.File;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoController : ControllerBase
	{
		private readonly IVideoService _videoService;
		private readonly IFileManager _fileManager;


		public VideoController(IVideoService videoService, IFileManager fileManager)
		{
			_videoService = videoService;
			_fileManager = fileManager;
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var entities = _videoService.GetAll()
				.Select(v => new VideoDto(
					v.Id,
					v.Title,
					v.Description,
					v.ImagePath,
					v.Path,
					v.IsPublic,
					v.CreatedAt,
					v.UserId,
					v.CategoryId
				));

			return Ok(entities);
		}

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

		[HttpPost("Insert")]
		public async Task<IActionResult> Insert(VideoDtoInput videoDtoInput)
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

			_videoService.Insert(videoDto);

			return Ok("Video successfully inserted");
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, VideoDtoInput videoDtoInput)
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
