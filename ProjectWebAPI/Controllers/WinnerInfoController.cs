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

        private IRatingRepository ratingRepository = new RatingRepository();
        private IRecipeRepository recipeRepository = new RecipeRepository();
        private IUserDetailRepository userDetailRepository = new UserDetailRepository();

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
                RecipeId = winnerInfoDTO.RecipeId,
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
            existingWinnerInfo.RecipeId = updateWinnerInfoDTO.RecipeId;
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

        // Get thông tin người thắng cuộc
        [HttpGet("Recipe/{contestId}")]
        public IActionResult GetWinnerInfo(int contestId)
        {
            // Kiểm tra xem có WinnerInfo nào tồn tại với contestId đã cho hay không
            var existingWinnerInfo = repository.GetWinnerInfoByContestId(contestId);
            if (existingWinnerInfo != null)
            {
                // Nếu có, trả về thông tin WinnerInfo đã tồn tại
                return Ok(existingWinnerInfo);
            }

            // Lấy danh sách những lượt đánh giá theo id cuộc thi truyền vào
            var listRecipeByContest = ratingRepository.GetRatingByContestId(contestId);

            if (listRecipeByContest == null || !listRecipeByContest.Any())
            {
                return NotFound();
            }

            var voteCountByRecipe = listRecipeByContest
                .GroupBy(rating => rating.RecipeId)
                .Select(group => new
                {
                    RecipeId = group.Key,
                    VoteCount = group.Sum(rating => rating.Vote),
                    MinCreateDate = group.Min(rating => rating.CreateDate)
                })
                .ToList();

            // Lấy recipedId có tổng số lượt vote cao nhất và thời gian tạo sớm nhất
            var selectedRecipe = voteCountByRecipe
                .OrderByDescending(x => x.VoteCount)
                .ThenByDescending(x => x.MinCreateDate)
                .FirstOrDefault();
            if (selectedRecipe == null)
            {
                // Nếu không có lượt vote nào, xử lý theo yêu cầu của bạn
                return NotFound("No votes for any recipes in the contest.");
            }

            // Lấy thông tin công thức theo RecipeId
            var recipeEntity = recipeRepository.GetRecipeById(selectedRecipe.RecipeId.GetValueOrDefault());
            if (recipeEntity == null)
            {
                // Xử lý khi không tìm thấy công thức
                return NotFound("Recipe not found.");
            }

            // Chuyển đổi từ selectedRecipe sang WinnerInfoDTO
            var winnerInfoDTO = new WinnerInfoDTO
            {
                ContestId = contestId,
                RecipeId = recipeEntity.RecipeId, // Sử dụng RecipeId từ công thức tìm được
                WinnerUserId = recipeEntity.Creator, 
                WinningDate = DateTime.Now,
                Prize = "1000$", // Thay thế bằng giải thưởng thực tế
                Vote = selectedRecipe.VoteCount
            };

            // Lưu thông tin WinnerInfo mới vào cơ sở dữ liệu
            CreateWinnerInfo(winnerInfoDTO);

            // Gọi phương thức để lấy thông tin WinnerInfo vừa tạo
            var newWinnerInfo = repository.GetWinnerInfoByContestId(contestId);

            // Trả về thông tin WinnerInfo
            return Ok(newWinnerInfo);
        }
    }
}
