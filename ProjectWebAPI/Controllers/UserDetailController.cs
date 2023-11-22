using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.DataAccess;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private IUserDetailRepository repository = new UserDetailRepository();
        // GET: api/<UserDetailController>
        [HttpGet]
        public ActionResult<IEnumerable<UserDetail>> GetUserDetails() => repository.GetUserDetails();

        // GET api/<UserDetailController>/5
        [HttpGet("{id}")]
        public ActionResult<UserDetail> GetUserDetailById(int id)
        {
            var userdetail = repository.GetUserDetailById(id);
            if (userdetail == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }

            return userdetail;
        }

        // POST api/<UserDetailController>
        [HttpPost]
        public IActionResult PostUserDetail(UserDetailDTO udvm)
        {
            var ud = new UserDetail
            {
                UserId = udvm.UserId,
                FullName = udvm.FullName,
                Gender = udvm.Gender,
                Phone = udvm.Phone,
                Address = udvm.Address,
                Avatar = udvm.Avatar,
            };
            repository.SaveUserDetail(ud);
            return NoContent();

        }

        // PUT api/<UserDetailController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUserDetail(int id, UserDetailDTO udvm)
        {
            var temp = repository.GetUserDetailById(id);
            if (temp == null)
            {
                return NotFound();
            }
            temp.UserId = udvm.UserId;
            temp.FullName = udvm.FullName;
            temp.Gender = udvm.Gender;
            temp.Phone = udvm.Phone;
            temp.Address = udvm.Address;
            temp.Avatar = udvm.Avatar;
            repository.UpdateUserDetail(temp);
            return NoContent();
        }

        // DELETE api/<UserDetailController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserDetail(int id)
        {
            var u = repository.GetUserDetailById(id);
            if (u == null)
                return NotFound();
            repository.DeleteUserDetail(u);
            return NoContent();
        }
    }
}
