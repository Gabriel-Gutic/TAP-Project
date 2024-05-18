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

        // Get information about all the subscribers
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var entities = _subscriberService.GetAll();
            return Ok(entities);
        }

        // Get all the users a user has subscribed to
        [HttpGet("GetCreators")]
        public IActionResult GetCreators(Guid subscriberId)
        {
            var entities = _subscriberService.GetCreators(subscriberId);
            return Ok(entities);
        }

        // Get information about a specific subscriber
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

        // Check if a user is subscriber for another user
        [HttpPost("IsSubscriber")]
        public IActionResult IsSubscriber(SubscriberDtoInput subscriberDtoInput)
        {
            return Ok(_subscriberService.IsSubscriber(subscriberDtoInput.CreatorId, subscriberDtoInput.SubscriberId));
        }

        // Get the subscriber count for a creator
        [HttpGet("Count")]
        public IActionResult Count(Guid creatorId)
        {
            return Ok(_subscriberService.Count(creatorId));
        }

        // Get information about all the subscibers for a specific creator
        [HttpGet("GetSubscribers")]
        public IActionResult GetSubscribers(Guid creatorId)
        {
            var entities = _subscriberService.GetSubscribers(creatorId);
            return Ok(entities);
        }

        // Insert a new subscriber in the database
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

        // Delete a subscriber from the database
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
