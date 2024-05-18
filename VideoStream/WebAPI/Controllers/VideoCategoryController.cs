using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoCategoryController : ControllerBase
	{
		private readonly IVideoCategoryService _videoCategoryService;

		public VideoCategoryController(IVideoCategoryService videoCategoryService)
		{
			_videoCategoryService = videoCategoryService;
		}

		// Get information about each category
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var entities = _videoCategoryService.GetAll();
			return Ok(entities);
		}

        // Get information about a specific category
        [HttpGet("Get")]
		public IActionResult Get(Guid id)
		{
			var videoCategory = _videoCategoryService.Get(id);

			if (videoCategory == null)
			{
				return NotFound("Video Category not found");
			}
			return Ok(videoCategory);
		}

        // Insert a new category in the database
		[HttpPost("Insert")]
        public IActionResult Insert(VideoCategoryDtoInput videoCategoryDtoInput)
		{
			VideoCategoryDto videoCategoryDto = new VideoCategoryDto(videoCategoryDtoInput.Name);

			_videoCategoryService.Insert(videoCategoryDto);

			return Ok("Video Category successfully inserted");
		}

        // Update an existing category
        // BadRequest if the category doesn't exist 
        [HttpPut("Update")]
        public IActionResult Update(Guid id, VideoCategoryDtoInput videoCategoryDtoInput)
		{
			try
			{
				VideoCategoryDto videoCategoryDto = new VideoCategoryDto(videoCategoryDtoInput.Name);	

				_videoCategoryService.Update(id, videoCategoryDto);
				return Ok("Video Category successfully updated");
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

        // Delete an existing category
        // BadRequest if the category doesn't exist 
        [HttpDelete("Delete")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				_videoCategoryService.Delete(id);
				return Ok("Video Category successfully deleted");
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
