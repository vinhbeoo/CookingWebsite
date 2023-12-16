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
    }
}
