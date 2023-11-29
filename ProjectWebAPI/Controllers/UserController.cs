using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repository = new UserRepository();
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers() => repository.GetUsers();

        // GET api/<UserController>/5
        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {
            var user = repository.GetUserById(userId);
            if (user == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }

            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult PostUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid User data");
            }

            var _user = new User
            {
                UserId = userDTO.UserId,
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                Email = userDTO.Email,
                RoleId = userDTO.RoleId,
                Status = userDTO.Status,
            };

            var _check = repository.CheckAddUser(_user);
            if (_check == null)
            {
                repository.SaveUser(_user);
                return Ok("User created successfully");
            }
            return BadRequest(_check);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDTO updateUserDTO)
        {
            if (updateUserDTO == null || id != updateUserDTO.UserId)
            {
                return BadRequest("Invalid User data");
            }
            var user = repository.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.UserId = updateUserDTO.UserId;
            user.UserName = updateUserDTO.UserName;
            user.Password = updateUserDTO.Password;
            user.Email = updateUserDTO.Email;
            user.RoleId = updateUserDTO.RoleId;
            user.Status = updateUserDTO.Status;

            repository.UpdateUser(user);
            return Ok("Update User successfully");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = repository.GetUserById(userId);
            if (user == null)
                return NotFound("User not found");

            repository.DeleteUser(user);
            return Ok("Delete User successfully");
        }
    }
}
