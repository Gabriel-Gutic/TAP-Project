using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var entities = _subscriberService.GetAll();
            return Ok(entities);
        }

        [HttpGet("GetCreators")]
        public IActionResult GetCreators(Guid subscriberId)
        {
            var entities = _subscriberService.GetCreators(subscriberId);
            return Ok(entities);
        }

        [HttpGet("Get")]
        public IActionResult Get(Guid id)
        {
            var entity = _subscriberService.Get(id);
            if (entity == null)
            {
                return NotFound("Subscriber not found");
            }
            return Ok(entity);
        }

        [HttpPost("IsSubscriber")]
        public IActionResult IsSubscriber(SubscriberDtoInput subscriberDtoInput)
        {
            return Ok(_subscriberService.IsSubscriber(subscriberDtoInput.CreatorId, subscriberDtoInput.SubscriberId));
        }

        [HttpGet("Count")]
        public IActionResult Count(Guid creatorId)
        {
            return Ok(_subscriberService.Count(creatorId));
        }

        [HttpPost("Subscribe")]
        public IActionResult Subscribe(SubscriberDtoInput subscriberDtoInput)
        {
            try
            {
                _subscriberService.Subscribe(subscriberDtoInput.CreatorId, subscriberDtoInput.SubscriberId);
                return Ok("Successfully subscription");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("Unsubscribe")]
        public IActionResult Unsubscribe(SubscriberDtoInput subscriberDtoInput)
        {
            try
            {
                _subscriberService.Unsubscribe(subscriberDtoInput.CreatorId, subscriberDtoInput.SubscriberId);
                return Ok("Successfully unsubscription");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
