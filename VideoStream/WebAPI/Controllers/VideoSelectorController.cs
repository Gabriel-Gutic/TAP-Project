using BusinessLayer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoSelectorController : ControllerBase
	{
		private readonly IVideoSelector _videoSelector;
		private readonly int _videoCount;

		public VideoSelectorController(IVideoSelector videoSelector)
		{
			_videoSelector = videoSelector;
			_videoCount = 20;
		}

		[HttpGet("Select")]
		public IActionResult Select()
		{
			IEnumerable<FileContentResult> videos;
			videos = _videoSelector.SelectForUser(new Guid("0F4316C5-4142-4D48-B8ED-1A19B2821EA8"), _videoCount)
				.Select(v => File(v.Data, "video/mp4"));
			
			return Ok(videos);
		}
	}
}
