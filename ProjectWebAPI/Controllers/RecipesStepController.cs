using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesStepController : ControllerBase
    {
        private IRecipeStepRepository repository = new RecipeStepRepository();

        // GET: api/<TypeController>
        [HttpGet]
        public ActionResult<IEnumerable<RecipesStep>> GetRecipesStep() => repository.GetRecipesStep();

        // GET api/<TypeController>/5
        [HttpGet("{id}")]
        public ActionResult<RecipesStep> GetRecipesStepById(int id)
        {
            var rs = repository.GetRecipesStepById(id);
            if (rs == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return rs;
        }

        // POST api/<TypeController>
        [HttpPost]
        public IActionResult CreateRecipesStep([FromBody] RecipesStepDTO recipesStepDTO)
        {
            if (recipesStepDTO == null)
            {
                return BadRequest("Invalid recipe step data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newRecipesStep = new RecipesStep
            {
                Step = recipesStepDTO.Step,
                RecipeId = recipesStepDTO.RecipeId,
                Description = recipesStepDTO.Description,
                ImageUrl = recipesStepDTO.ImageUrl,
                VideoUrl = recipesStepDTO.VideoUrl

            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveRecipesStep(newRecipesStep);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Recipe Step created successfully");
        }

        // PUT api/<TypeController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRecipesStep(int id, [FromBody] RecipesStepDTO updatedRecipesStepDTO)
        {
            if (updatedRecipesStepDTO == null || id != updatedRecipesStepDTO.RecipeId)
            {
                return BadRequest("Invalid Recipe Step data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingRecipesStep = repository.GetRecipesStepById(id);
            if (existingRecipesStep == null)
            {
                return NotFound("Recipe Step not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingRecipesStep.Step = updatedRecipesStepDTO.Step;
            existingRecipesStep.RecipeId = updatedRecipesStepDTO.RecipeId;
            existingRecipesStep.Description = updatedRecipesStepDTO.Description;
            existingRecipesStep.ImageUrl = updatedRecipesStepDTO.ImageUrl;
            existingRecipesStep.VideoUrl = updatedRecipesStepDTO.VideoUrl;
           

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateRecipesStep(existingRecipesStep);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Recipe step updated successfully");
        }

        // DELETE api/<TypeController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipesStep(int id)
        {
            var temp = repository.GetRecipesStepById(id);
            if (temp == null)
            {
                return NotFound();
            }
            repository.DeleteRecipesStep(temp);
            return NoContent();
        }
    }
}
