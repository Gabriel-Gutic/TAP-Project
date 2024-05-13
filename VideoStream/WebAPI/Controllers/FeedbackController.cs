using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var entities = _feedbackService.GetAll();
            return Ok(entities);
        }

        [HttpGet("Get")]
        public IActionResult Get(Guid id)
        {
            var feedback = _feedbackService.Get(id);

            if (feedback == null)
            {
                return NotFound("Feedback not found");
            }
            return Ok(feedback);
        }

        [HttpGet("CountPositive")]
        public IActionResult GetCountPositive(Guid videoId)
        {
            return Ok(_feedbackService.CountPositive(videoId));
        }

        [HttpGet("CountNegative")]
        public IActionResult GetCountNegative(Guid videoId)
        {
            return Ok(_feedbackService.CountNegative(videoId));
        }

        [HttpGet("Find")]
        public IActionResult Find(Guid userId, Guid videoId)
        {
            return Ok(_feedbackService.FindFeedback(userId, videoId));
        }

        [HttpPost("Insert")]
        public IActionResult Insert(FeedbackDtoInput feedbackDtoInput)
        {
            FeedbackDto feedbackDto = new FeedbackDto()
            {
                IsPositive = feedbackDtoInput.IsPositive,
                UserId = feedbackDtoInput.UserId,
                VideoId = feedbackDtoInput.VideoId,
            };

            try
            {
                _feedbackService.Insert(feedbackDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Feedback successfully inserted");
        }

        [HttpPut("Update")]
        public IActionResult Update(Guid id, FeedbackDtoInput feedbackDtoInput)
        {
            try
            {
                FeedbackDto feedbackDto = new FeedbackDto()
                {
                    IsPositive = feedbackDtoInput.IsPositive,
                    UserId = feedbackDtoInput.UserId,
                    VideoId = feedbackDtoInput.VideoId,
                };

                _feedbackService.Update(id, feedbackDto);
                return Ok("Feedback successfully updated");
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

        [HttpPatch("Edit")]
        public IActionResult Edit(FeedbackDtoInput feedbackDtoInput)
        {
            try
            {
                FeedbackDto feedbackDto = new FeedbackDto()
                {
                    IsPositive = feedbackDtoInput.IsPositive,
                    UserId = feedbackDtoInput.UserId,
                    VideoId = feedbackDtoInput.VideoId,
                };

                _feedbackService.Edit(feedbackDtoInput.UserId, feedbackDtoInput.VideoId, feedbackDtoInput.IsPositive);
                return Ok("Feedback successfully edited");
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
                _feedbackService.Delete(id);
                return Ok("Feedback successfully deleted");
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
