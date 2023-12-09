using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private IContestRepository repository = new ContestRepository();
        // GET: api/<ContestController>
        [HttpGet]
        public ActionResult<IEnumerable<Contest>> GetProducts() => repository.GetContests();

        // GET api/<ContestController>/5
        [HttpGet("{contestId}")]
        public ActionResult<Contest> GetContestById(int contestId)
        {
            var contest = repository.GetContestById(contestId);
            if (contest == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return contest;
        }

        [HttpPost]
        public IActionResult CreateContest([FromBody] ContestDTO contestDTO)
        {
            if (contestDTO == null)
            {
                return BadRequest("Invalid contest data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newContest = new Contest
            {
                ContestId = contestDTO.ContestId,
                ContestName = contestDTO.ContestName,
                Description = contestDTO.Description,
                StartTime = contestDTO.StartTime,
                EndTime = contestDTO.EndTime,
                OwnerUserId = contestDTO.OwnerUserId,
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveContest(newContest);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Contest created successfully");
        }

        // PUT api/<ContestController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateContest(int id, [FromBody] ContestDTO updatedContestDTO)
        {
            if (updatedContestDTO == null || id != updatedContestDTO.ContestId)
            {
                return BadRequest("Invalid contest data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingContest = repository.GetContestById(id);
            if (existingContest == null)
            {
                return NotFound("Contest not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingContest.ContestName = updatedContestDTO.ContestName;
            existingContest.Description = updatedContestDTO.Description;
            existingContest.StartTime = updatedContestDTO.StartTime;
            existingContest.EndTime = updatedContestDTO.EndTime;
            existingContest.OwnerUserId = updatedContestDTO.OwnerUserId;

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateContest(existingContest);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Contest updated successfully");
        }

        // DELETE api/<ContestController>/5
        [HttpDelete("{contestId}")]
        public IActionResult DeleteContest(int contestId, int userId)
        {
            var contest = repository.GetContestById(contestId);
            if (contest == null)
            {
                return NotFound();
            }
            repository.DeleteContest(contest);
            return Ok("Contest Delete successfully");
        }
    }
}
