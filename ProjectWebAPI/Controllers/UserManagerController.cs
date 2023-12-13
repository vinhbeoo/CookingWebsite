using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private IUserRepository repository = new UserRepository();
        // GET: api/<UserManagerController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers() => repository.GetUsers();

        // GET api/<UserManagerController>/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = repository.GetUserById(id);
            if (user == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }

            return user;
        }

        // POST api/<UserManagerController>
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
                EmailConfirmed = userDTO.EmailConfirmed,
                EmailConfirmationToken = null,
                RoleId = userDTO.RoleId,
                Status = userDTO.Status,
                UserType = userDTO.UserType,
            };

            var _check = repository.CheckAddUser(_user);
            if (_check == null)
            {
                repository.SaveUser(_user);
                return Ok("User created successfully");
            }
            return BadRequest(_check);

        }

        // PUT api/<UserManagerController>/5
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
            user.EmailConfirmed = updateUserDTO.EmailConfirmed;
            user.EmailConfirmationToken = updateUserDTO.EmailConfirmationToken;
            user.RoleId = updateUserDTO.RoleId;
            user.Status = updateUserDTO.Status;
            user.UserType = updateUserDTO.UserType;

            repository.UpdateUser(user);
            return Ok("Update User successfully");
        }

        // DELETE api/<UserManagerController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = repository.GetUserById(id);
            if (user == null)
                return NotFound("User not found");

            repository.DeleteUser(user);
            return Ok("Delete User successfully");
        }
    }
}
