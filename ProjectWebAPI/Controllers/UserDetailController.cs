using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

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
        [HttpGet("{userId}")]
        public ActionResult<UserDetail> GetUserDetailById(int userId)
        {
            var userdetail = repository.GetUserDetailById(userId);
            if (userdetail == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }

            return userdetail;
        }

        // POST api/<UserDetailController>
        [HttpPost]
        public IActionResult PostUserDetail(UserDetailDTO userDetailDTO)
        {
            if (userDetailDTO == null)
            {
                return BadRequest("Invalid UserDetail data");
            }

            var ud = new UserDetail
            {
                UserId = userDetailDTO.UserId,
                FullName = userDetailDTO.FullName,
                Gender = userDetailDTO.Gender,
                Phone = userDetailDTO.Phone,
                Address = userDetailDTO.Address,
                Avatar = userDetailDTO.Avatar,
            };
            repository.SaveUserDetail(ud);
            return Ok("UserDetail created successfully");
        }

        // PUT api/<UserDetailController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUserDetail(int id, UserDetailDTO updateUserDetailDTO)
        {
            if (updateUserDetailDTO == null || id != updateUserDetailDTO.UserId)
            {
                return BadRequest("Invalid UserDetail data");
            }
            var userDetail = repository.GetUserDetailById(id);
            if (userDetail == null)
            {
                return NotFound("UserDetail not found");
            }
            userDetail.UserId = updateUserDetailDTO.UserId;
            userDetail.FullName = updateUserDetailDTO.FullName;
            userDetail.Gender = updateUserDetailDTO.Gender;
            userDetail.Phone = updateUserDetailDTO.Phone;
            userDetail.Address = updateUserDetailDTO.Address;
            userDetail.Avatar = updateUserDetailDTO.Avatar;

            repository.UpdateUserDetail(userDetail);
            return Ok("UserDetail Updated successfully");
        }

        // DELETE api/<UserDetailController>/5
        [HttpDelete("{userId}")]
        public IActionResult DeleteUserDetail(int userId)
        {
            var userDetail = repository.GetUserDetailById(userId);
            if (userDetail == null)
                return NotFound();

            repository.DeleteUserDetail(userDetail);
            return Ok("UserDetail Delete successfully");
        }
    }
}
