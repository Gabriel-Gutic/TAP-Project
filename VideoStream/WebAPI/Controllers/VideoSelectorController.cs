using BusinessLayer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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

		// Select videos to be shown on the Home Page,
		// for a specific user, send by id 
		[HttpGet("SelectForId")]
		public IActionResult Select(Guid id)
		{
			var videos = _videoSelector.SelectForUser(id, _videoCount);
			
			return Ok(videos);
		}

        // Select videos to be shown on the Home Page,
        // for a specific user, send by username 
		// If the username is null, it is considered that 
		// no user is logged in
        [HttpGet("SelectForUsername")]
        public IActionResult Select(string? username)
        {
            var videos = _videoSelector.SelectForUser(username, _videoCount);

            return Ok(videos);
        }
    }
}
