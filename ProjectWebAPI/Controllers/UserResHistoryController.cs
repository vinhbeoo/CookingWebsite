using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserResHistoryController : ControllerBase
    {
        private IUserRegHistoryRepository repository = new UserRegHistoryRepository();

        // GET: api/<UserRegHistoryController>
        [HttpGet]
        public ActionResult<IEnumerable<UserRegHistory>> GetUserRegHistories() => repository.GetUserRegHistories();


        [HttpGet("{id}")]
        public ActionResult<List<UserRegHistory>> GetUserRegHistoriesById(int id)
        {
            var userResHistory = repository.GetUserRegHistoryById(id);
            if (userResHistory == null)
            {
                return NotFound(); // Return a 404 Not Found response
            }

            return Ok(userResHistory); // Wrap the user activities in an Ok result
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<UserRegHistory>> GetUserRegHistoriesByUserId(int userId)
        {
            var userResHistory = repository.GetUserRegHistoriesByUserId(userId);
            if (userResHistory == null)
            {
                return NotFound(); // Return a 404 Not Found response
            }

            return Ok(userResHistory); // Wrap the user activities in an Ok result
        }

    }
}
