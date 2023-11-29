using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnerInfoController : ControllerBase
    {
        private IWinnerInfoRepository repository = new WinnerInfoRepository();
        // GET: api/<WinnerInfoController>
        [HttpGet]
        public ActionResult<IEnumerable<WinnerInfo>> GetWinnerInfos() => repository.GetWinnerInfos();


        // GET api/<WinnerInfoController>/5
        [HttpGet("{winnerinfoId}")]
        public ActionResult<WinnerInfo> GetWinnerInfoById(int winnerinfoId)
        {
            var winnerinfo = repository.GetWinnerInfoById(winnerinfoId);
            if (winnerinfo == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }

            return winnerinfo;
        }

        // POST api/<WinnerInfoController>
        [HttpPost]
        public IActionResult PostWinnerInfo(WinnerInfoDTO winnerInfoDTO)
        {
            if (winnerInfoDTO == null)
            {
                return BadRequest("Invalid WinnerInfo data");
            }

            var wi = new WinnerInfo
            {
                WinnerId = winnerInfoDTO.WinnerId,
                ContestId = winnerInfoDTO.ContestId,
                WinnerUserId = winnerInfoDTO.WinnerUserId,
                WinningDate = winnerInfoDTO.WinningDate,
                Prize = winnerInfoDTO.Prize,
                Vote = winnerInfoDTO.Vote,
            };
            repository.SaveWinnerInfo(wi);
            return Ok("WinnerInfo created successfully");
        }

        // PUT api/<WinnerInfoController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateWinnerInfo(int id, WinnerInfoDTO updateWinnerInfoDTO)
        {
            if (updateWinnerInfoDTO == null || id != updateWinnerInfoDTO.WinnerId)
            {
                return BadRequest("Invalid WinnerInfo data");
            }
            var winnerInfo = repository.GetWinnerInfoById(id);
            if (winnerInfo == null)
            {
                return NotFound("WinnerInfo not found");
            }
            winnerInfo.WinnerId = updateWinnerInfoDTO.WinnerId;
            winnerInfo.ContestId = updateWinnerInfoDTO.ContestId;
            winnerInfo.WinnerUserId = updateWinnerInfoDTO.WinnerUserId;
            winnerInfo.WinningDate = updateWinnerInfoDTO.WinningDate;
            winnerInfo.Prize = updateWinnerInfoDTO.Prize;
            winnerInfo.Vote = updateWinnerInfoDTO.Vote;

            repository.UpdateWinnerInfo(winnerInfo);
            return Ok("WinnerInfo Updated successfully");
        }

        // DELETE api/<WinnerInfoController>/5
        [HttpDelete("{winnerinfoId}")]
        public IActionResult DeleteWinnerInfo(int winnerinfoId)
        {
            var winnerInfo = repository.GetWinnerInfoById(winnerinfoId);
            if (winnerInfo == null)
                return NotFound();

            repository.DeleteWinnerInfo(winnerInfo);
            return Ok("WinnerInfo Delete successfully");
        }
    }
}
