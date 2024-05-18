using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ViewController : ControllerBase
	{
		private readonly IViewService _viewService;
		private readonly IAppCache _appCache;

		public ViewController(IViewService viewService, IAppCache appCache)
		{
			_viewService = viewService;
			_appCache = appCache;
		}

		// Get information about each view
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var entities = _viewService.GetAll();
			return Ok(entities);
		}

		// Get information about a specific view
		[HttpGet("Get")]
        public IActionResult Get(Guid id)
		{
			var view = _viewService.Get(id);

			if (view == null)
			{
				return NotFound("View not found");
			}
			return Ok(view);
		}

        // Get the view count for a specific video.
		// This function is using cache, the count will be 
		// up to date at each 20 seconds
        // Cache
        [HttpGet("Count")]
        public IActionResult Count(Guid videoId)
        {
            string cacheKey = "views-" + videoId;

            if (_appCache.TryGet(cacheKey, out object? cache))
            {
				return Ok(cache);
            }

			int count = _viewService.Count(videoId);

            _appCache.Store(cacheKey, count, 20);

            return Ok(count);
        }

		// Insert a new view in the database
        [HttpPost("Insert")]
		public IActionResult Insert(ViewDtoInput viewDtoInput)
		{
			ViewDto viewDto = new ViewDto(
					viewDtoInput.UserId,
					viewDtoInput.VideoId
				);

			_viewService.Insert(viewDto);

			return Ok("View successfully inserted");
		}

        // Update an existing view
        // BadRequest if the view doesn't exist 
        [HttpPut("Update")]
		public IActionResult Update(Guid id, ViewDtoInput viewDtoInput)
		{
			try
			{
				ViewDto viewDto = new ViewDto(
						viewDtoInput.UserId,
						viewDtoInput.VideoId
					);

				_viewService.Update(id, viewDto);
				return Ok("View successfully updated");
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

        // Remove an existing view
        // BadRequest if the view doesn't exist 
        [HttpDelete("Delete")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				_viewService.Delete(id);
				return Ok("View successfully deleted");
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
