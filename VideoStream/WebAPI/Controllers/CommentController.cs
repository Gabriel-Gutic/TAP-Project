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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var entities = _commentService.GetAll();
            return Ok(entities);
        }

        [HttpGet("Get")]
        public IActionResult Get(Guid id)
        {
            var comment = _commentService.Get(id);

            if (comment == null)
            {
                return NotFound("View not found");
            }
            return Ok(comment);
        }

        [HttpGet("GetForVideo")]
        public IActionResult GetForVideo(Guid videoId)
        {
            var entities = _commentService.GetForVideo(videoId);
            return Ok(entities);
        }

        [HttpPost("Insert")]
        public IActionResult Insert(CommentDtoInput commentDtoInput)
        {
            CommentDto commentDto = new CommentDto()
            {
                Message = commentDtoInput.Message,
                UserId = commentDtoInput.UserId,
                VideoId = commentDtoInput.VideoId,
            };

            _commentService.Insert(commentDto);

            return Ok("View successfully inserted");
        }

        [HttpPut("Update")]
        public IActionResult Update(Guid id, CommentDtoInput commentDtoInput)
        {
            try
            {
                CommentDto commentDto = new CommentDto()
                {
                    Message = commentDtoInput.Message,
                    UserId = commentDtoInput.UserId,
                    VideoId = commentDtoInput.VideoId,
                };

                _commentService.Update(id, commentDto);
                return Ok("Comment successfully updated");
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
                _commentService.Delete(id);
                return Ok("Comment successfully deleted");
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
