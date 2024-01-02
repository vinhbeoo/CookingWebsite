using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.App.Code;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnerInfoController : ControllerBase
    {
        private IWinnerInfoRepositoty repository = new WinnerInfoRepositoty();
        // GET: api/<WinnerInfoController>
        [HttpGet]
        public ActionResult<IEnumerable<WinnerInfo>> GetWinnerInfos() => repository.GetWinnerInfos();

        // GET api/<WinnerInfoController>/5
        [HttpGet("{winnerId}")]
        public ActionResult<WinnerInfo> GetWinnerInfoById(int winnerId)
        {
            var winner = repository.GetWinnerInfoById(winnerId);
            if (winner == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return winner;
        }

        // POST api/<WinnerInfoController>
        [HttpPost]
        public IActionResult CreateWinnerInfo([FromBody] WinnerInfoDTO winnerInfoDTO)
        {
            if (winnerInfoDTO == null)
            {
                return BadRequest("Invalid WinnerInfo data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newWinnerInfo = new WinnerInfo
            {
                WinnerId = winnerInfoDTO.WinnerId,
                ContestId = winnerInfoDTO.ContestId,
                WinnerUserId = winnerInfoDTO.WinnerUserId,
                WinningDate = winnerInfoDTO.WinningDate,
                Prize = winnerInfoDTO.Prize,
                Vote = winnerInfoDTO.Vote,
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveWinnerInfo(newWinnerInfo);


            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("WinnerInfo created successfully");
        }

        // PUT api/<WinnerInfoController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateWinnerInfo(int id, [FromBody] WinnerInfoDTO updateWinnerInfoDTO)
        {
            if (updateWinnerInfoDTO == null || id != updateWinnerInfoDTO.ContestId)
            {
                return BadRequest("Invalid WinnerInfo data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingWinnerInfo = repository.GetWinnerInfoById(id);
            if (existingWinnerInfo == null)
            {
                return NotFound("Contest not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingWinnerInfo.ContestId = updateWinnerInfoDTO.ContestId;
            existingWinnerInfo.WinnerUserId = updateWinnerInfoDTO.WinnerUserId;
            existingWinnerInfo.WinningDate = updateWinnerInfoDTO.WinningDate;
            existingWinnerInfo.Prize = updateWinnerInfoDTO.Prize;
            existingWinnerInfo.Vote = updateWinnerInfoDTO.Vote;

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateWinnerInfo(existingWinnerInfo);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("WinnerInfo updated successfully");
        }

        // DELETE api/<WinnerInfoController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWinnerInfo(int id)
        {
            var winnerInfo = repository.GetWinnerInfoById(id);
            if (winnerInfo == null)
            {
                return NotFound();
            }
            repository.DeleteWinnerInfo(winnerInfo);

            return Ok("WinnerInfo Delete successfully");
        }
    }
}
