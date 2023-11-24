using Azure;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.DataAccess;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;
using ProjectWebAPI.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationRepository _response = new NotificationRepository();
        // GET: api/<NotificationController>
        [HttpGet]
        public ActionResult<IEnumerable<Notification>> GetNotifications() => _response.GetNotifications();
        
        

        // GET api/<NotificationController>/5
        [HttpGet("{id}")]
        public ActionResult<Notification> GetNotificationById(int id)
        {
            var notification = _response.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return notification;
        }

        // POST api/<NotificationController>
        [HttpPost]
        public IActionResult PostNotification(NotificationViewModel notificationViewModel)
        {
            if (ModelState.IsValid)
            {
                var newNotification = new Notification
                {
                    NotificationID = notificationViewModel.NotificationID,
                    Title = notificationViewModel.Title,
                    Content = notificationViewModel.Content,
                    Date = notificationViewModel.Date,
                    UserId = notificationViewModel.UserId,
                };

                _response.SaveNotification(newNotification);

                return Ok("Notification created successfully");
            }

            return BadRequest("Invalid notification data");
        }

        // PUT api/<NotificationController>/5
        [HttpPut("{id}")]
        public IActionResult PutNotification(int id, NotificationDTO nDTO)
        {
            var existingNotification = _response.GetNotificationById(id);

            if (existingNotification == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính của existingNotification từ nDTO
            existingNotification.Title = nDTO.Title;
            existingNotification.Content = nDTO.Content;
            existingNotification.Date = nDTO.Date;
            existingNotification.UserId = nDTO.UserId;

            _response.UpdateNotification(existingNotification);

            return Ok("Notification updated successfully");
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var temp = _response.GetNotificationById(id);
            if (temp == null)
            {
                return NotFound();
            }
            _response.DeleteNotification(temp);
            return Ok("Notification dalete successfully");
        }
    }
}
