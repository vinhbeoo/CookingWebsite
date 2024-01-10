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
    public class RatingController : ControllerBase
    {
        private IRatingRepository repository = new RatingRepository();
        // GET: api/<RatingController>
        [HttpGet]
        public ActionResult<IEnumerable<Rating>> GetRatings() => repository.GetRatings();


        [HttpGet("rating/{recipeId}")]
        public ActionResult<List<Rating>> GetRatingByRecipeId(int recipeId)
        {
            var rating = repository.GetRatingByRecipeId(recipeId);
            if (rating == null)
            {
                return NotFound(); // Return a 404 Not Found response
            }

            return Ok(rating); // Wrap the user activities in an Ok result
        }

        // GET api/<RatingController>/5
        [HttpGet("{id}")]
        public ActionResult<Rating> GetRatingById(int id)
        {
            var rating = repository.GetRatingById(id);
            if (rating == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return rating;
        }

        // POST api/<RatingController>
        [HttpPost]
        public IActionResult CreateRating([FromBody] RatingDTO ratingDTO)
        {
            if (ratingDTO == null)
            {
                return BadRequest("Invalid contest data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newRating = new Rating
            {
                RatingId = ratingDTO.RatingId,
                UserId = ratingDTO.UserId,
                RecipeId = ratingDTO.RecipeId,
                Vote = ratingDTO.Vote,
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveRating(newRating);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Rating created successfully");
        }

        // PUT api/<RatingController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRating(int id, [FromBody] RatingDTO updatedRatingDTO)
        {
            if (updatedRatingDTO == null || id != updatedRatingDTO.RatingId)
            {
                return BadRequest("Invalid contest data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingRating = repository.GetRatingById(id);
            if (existingRating == null)
            {
                return NotFound("Contest not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingRating.RatingId = updatedRatingDTO.RatingId;
            existingRating.UserId = updatedRatingDTO.UserId;
            existingRating.RecipeId = updatedRatingDTO.RecipeId;
            existingRating.Vote = updatedRatingDTO.Vote;

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateRating(existingRating);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Rating updated successfully");
        }

        // DELETE api/<RatingController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            var rating = repository.GetRatingById(id);
            if (rating == null)
            {
                return NotFound();
            }
            repository.DeleteRating(rating);

            return Ok("Rating Delete successfully");
        }

        // DELETE by user and recid
        [HttpDelete("{user}/{recipeId}")]
        public IActionResult DeleteRatingByUserAndRecipeId(int user, int recipeId)
        {
            var rating = repository.GetRatingByUserAndRecipeId(user, recipeId);
            if (rating == null)
            {
                return NotFound();
            }
            repository.DeleteRating(rating);

            return Ok("Rating Delete successfully");
        }
    }
}
