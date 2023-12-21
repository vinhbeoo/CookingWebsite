using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

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
        public IActionResult PostNotification(NotificationDTO nDTO)
        {
            if (ModelState.IsValid)
            {
                var newNotification = new Notification
                {
                    NotificationId = nDTO.NotificationID,
                    Title = nDTO.Title,
                    Description = nDTO.Description,
                    Date = nDTO.Date,
                    UserId = nDTO.UserId,
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
            existingNotification.Description = nDTO.Description;
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
