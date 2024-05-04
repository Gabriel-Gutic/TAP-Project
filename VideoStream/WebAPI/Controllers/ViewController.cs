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
	public class ViewController : ControllerBase
	{
		private readonly IViewService _viewService;

		public ViewController(IViewService viewService)
		{
			_viewService = viewService;
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var entities = _viewService.GetAll();
			return Ok(entities);
		}

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
