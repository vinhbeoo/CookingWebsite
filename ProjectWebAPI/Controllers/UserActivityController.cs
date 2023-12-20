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

        // GET: api/UserActivity/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<UserActivity>> GetUserActivitiesById(int id)
        {
            var userActivities = repository.GetUserActivitiesById(id);

            if (userActivities == null || userActivities.Count == 0)
            {
                return NotFound(); // Return 404 Not Found if no user activities are found
            }

            return Ok(userActivities);
        }
    }
}
