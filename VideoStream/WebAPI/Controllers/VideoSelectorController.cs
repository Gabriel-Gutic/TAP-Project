using BusinessLayer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoSelectorController : ControllerBase
	{
		private readonly IVideoSelectorService _videoSelector;
		private readonly int _videoCount;

		public VideoSelectorController(IVideoSelectorService videoSelector)
		{
			_videoSelector = videoSelector;
			_videoCount = 20;
		}

		[HttpGet("SelectForId")]
		public IActionResult Select(Guid id)
		{
			var videos = _videoSelector.SelectForUser(id, _videoCount);
			
			return Ok(videos);
		}

        [HttpGet("SelectForUsername")]
        public IActionResult Select(string? username)
        {
            var videos = _videoSelector.SelectForUser(username, _videoCount);

            return Ok(videos);
        }
    }
}
