using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

namespace ProjectWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientDetailController : ControllerBase
	{
		private IIngredientsDetailRepository repository = new IngredientsDetailRepository();

		// GET: api/<TypeController>
		[HttpGet]
		public ActionResult<IEnumerable<IngredientsDetail>> GetIngredientsDetails() => repository.GetIngredientDetails();

		// GET api/<TypeController>/5
		[HttpGet("{id}")]
		public ActionResult<IngredientsDetail> GetIngredientsDetailById(int id)
		{
			var ind = repository.GetIngredientDetailById(id);
			if (ind == null)
			{
				return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
			}
			return ind;
		}

		[HttpGet("GetByRecipeId/{recipeid}")]
		public ActionResult<IEnumerable<IngredientsDetail>> GetIngredientsDetailByRecipeId(int recipeid) => repository.GetIngredientsDetailByRecipeId(recipeid);

		// POST api/<TypeController>
		[HttpPost]
		public IActionResult CreateIngredientsDetail([FromBody] IngredientsDetailDTO ingredientsDetailDTO)
		{
			if (ingredientsDetailDTO == null)
			{
				return BadRequest("Invalid Detail data");
			}

			// Chuyển đổi từ DTO sang đối tượng Contest
			var newIngredientsDetail = new IngredientsDetail
			{
				IngredientId = ingredientsDetailDTO.IngredientId,
				Stt = ingredientsDetailDTO.Stt,
				Description = ingredientsDetailDTO.Description,
				RecipeId = ingredientsDetailDTO.RecipeId
			};

			// Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
			repository.SaveIngredientDetail(newIngredientsDetail);

			// Trả về một thông báo thành công hoặc các thông tin khác cần thiết
			return Ok("IngredientsDetail created successfully");
		}

		// PUT api/<TypeController>/5
		[HttpPut("{id}")]
		public IActionResult UpdateIngredientsDetail(int id, [FromBody] IngredientsDetailDTO updatedIngredientsDetailDTO)
		{
			if (updatedIngredientsDetailDTO == null || id != updatedIngredientsDetailDTO.IngredientId)
			{
				return BadRequest("Invalid IngredientsupdatedIngredientsDetailDTO data");
			}

			// Kiểm tra xem cuộc thi có tồn tại không
			var existingIngredientsDetail = repository.GetIngredientDetailById(id);
			if (existingIngredientsDetail == null)
			{
				return NotFound("IngredientsDetail not found");
			}

			// Cập nhật thông tin cuộc thi từ DTO
			existingIngredientsDetail.IngredientId = updatedIngredientsDetailDTO.IngredientId;
			existingIngredientsDetail.Stt = updatedIngredientsDetailDTO.Stt;
			existingIngredientsDetail.Description = updatedIngredientsDetailDTO.Description;
			existingIngredientsDetail.RecipeId = updatedIngredientsDetailDTO.RecipeId;

			// Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
			repository.UpdateIngredientDetail(existingIngredientsDetail);

			// Trả về một thông báo thành công hoặc các thông tin khác cần thiết
			return Ok("IngredientsDetail updated successfully");
		}

		// DELETE api/<TypeController>/5
		[HttpDelete("{id}")]
		public IActionResult DeleteIngredientsDetail(int id)
		{
			var temp = repository.GetIngredientDetailById(id);
			if (temp == null)
			{
				return NotFound();
			}
			repository.DeleteIngredientDetail(temp);
			return NoContent();
		}
	}
}
