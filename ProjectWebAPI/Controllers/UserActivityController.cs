using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivityController : ControllerBase
    {
        private IUserActivityRepository repository = new UserActivityRepository();

        // GET: api/<UserActivityController>
        [HttpGet]
        public ActionResult<IEnumerable<UserActivity>> GetUserActivities() => repository.GetUserActivities();


        [HttpGet("{id}")]
        public ActionResult<List<UserActivity>> GetUserActivitiesById(int id)
        {
            var userActivities = repository.FindUserActivitiesByUserId(id);
            if (userActivities == null || userActivities.Count == 0)
            {
                return NotFound(); // Return a 404 Not Found response
            }

            return Ok(userActivities); // Wrap the user activities in an Ok result
        }

    }
}
