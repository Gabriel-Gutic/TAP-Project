using BusinessLayer.Contracts;
using BusinessLayer.Dto.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_notificationService.GetAll());
        }

        [HttpPost("Subscriber")]
        public IActionResult SendSubscriberNotification(SubscriberNotificationDto data)
        {
            try
            {
                _notificationService.PushSubscriber(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Notification successufully sended");
        }

        [HttpPost("VideoUpload")]
        public IActionResult SendVideoUploadNotification(VideoUploadNotificationDto data)
        {
            try
            {
                _notificationService.PushVideoUpload(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Notification successufully sended");
        }
    }
}
