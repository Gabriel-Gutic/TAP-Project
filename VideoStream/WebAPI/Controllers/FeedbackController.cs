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

        // Get information about every feedback
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var entities = _feedbackService.GetAll();
            return Ok(entities);
        }

        // Get information about a specific feedback
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

        // Get the like count for a specific video 
        [HttpGet("CountPositive")]
        public IActionResult GetCountPositive(Guid videoId)
        {
            return Ok(_feedbackService.CountPositive(videoId));
        }

        // Get the dislike count for a specific video 
        [HttpGet("CountNegative")]
        public IActionResult GetCountNegative(Guid videoId)
        {
            return Ok(_feedbackService.CountNegative(videoId));
        }

        // Find the feedback that a user left on a video
        // Return null if the user didn't leave a feedback to 
        // that video
        [HttpGet("Find")]
        public IActionResult Find(Guid userId, Guid videoId)
        {
            return Ok(_feedbackService.FindFeedback(userId, videoId));
        }

        // Insert a new feedback to the database
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

        // Update an existing feedback
        // BadRequest if the feedback doesn't exist 
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

        // Edit an existing feedback
        // Only the IsPositive field is affected
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

        // Delete an existing feedback
        // BadRequest if the feedback doesn't exist 
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
