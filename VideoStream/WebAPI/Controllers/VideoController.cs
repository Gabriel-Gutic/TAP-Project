using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
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
				.Select(v => new VideoDtoOutput(
						v.Title,
						v.Description,
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

			var videoOut = new VideoDtoOutput(
					video.Title,
					video.Description,
					video.IsPublic,
					video.CreatedAt,
					video.UserId,
					video.CategoryId
				);

			return Ok(videoOut);
		}

		[HttpGet("GetImage")]
		public IActionResult GetImage(Guid id)
		{
			var video = _videoService.Get(id);

			if (video == null)
			{
				return NotFound("Video not found");
			}

			return File(video.Image, "image/png");
		}

		[HttpGet("GetVideo")]
		public IActionResult GetVideo(Guid id)
		{
			var video = _videoService.Get(id);

			if (video == null)
			{
				return NotFound("Video not found");
			}

			return File(video.Data, "video/mp4", "video.mp4");
		}

		[HttpPost("Insert")]
		public async Task<IActionResult> Insert(VideoDtoInput videoDtoInput)
		{
			byte[]? video = null;
			try
			{
				video = await _fileManager.Read(videoDtoInput.Video);
			}
			catch
			{
				return BadRequest("An error occured in video uploading");
			}

			byte[]? image = null;
			try
			{
				image = await _fileManager.Read(videoDtoInput.Image);
			}
			catch
			{
				return BadRequest("An error occured in image uploading");
			}

			VideoDto videoDto = new VideoDto(
					videoDtoInput.Title,
					videoDtoInput.Description,
					image,
					video,
					videoDtoInput.IsPublic,
					videoDtoInput.UserId,
					videoDtoInput.CategoryId
				);

			_videoService.Insert(videoDto);

			return Ok("Video successfully inserted");
		}

		// TODO: Update, Delete
	}
}
