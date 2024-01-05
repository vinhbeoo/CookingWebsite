using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

namespace ProjectWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientGroupController : ControllerBase
	{
		private IIngredientsGroupRepository repository = new IngredientsGroupRepository();

		// GET: api/<TypeController>
		[HttpGet]
		public ActionResult<IEnumerable<IngredientsGroup>> GetIngredientsGroups() => repository.GetIngredientsGroups();

		// GET api/<TypeController>/5
		[HttpGet("{id}")]
		public ActionResult<IngredientsGroup> GetIngredientsGroupById(int id)
		{
			var ig = repository.GetIngredientsGroupById(id);
			if (ig == null)
			{
				return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
			}
			return ig;
		}
		// GET api/<TypeController>/5
		[HttpGet("GetByRecipeId/{recipeid}")]
		public ActionResult<IEnumerable<IngredientsGroup>> GetIngredientsGroupByRecipeId(int recipeid) => repository.GetIngredientsGroupsByRecipeId(recipeid);

		// POST api/<TypeController>
		[HttpPost]
		public IActionResult CreateIngredientsGroup([FromBody] IngredientsGroupDTO ingredientsGroupDTO)
		{
			if (ingredientsGroupDTO == null)
			{
				return BadRequest("Invalid tag data");
			}

			// Chuyển đổi từ DTO sang đối tượng Contest
			var newIngredientsGroup = new IngredientsGroup
			{
				IngredientId = ingredientsGroupDTO.IngredientId,
				NameIngredients = ingredientsGroupDTO.NameIngredients,
				RecipeId = ingredientsGroupDTO.RecipeId,
                Description = ingredientsGroupDTO.Description
            };

			// Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
			repository.SaveIngredientsGroup(newIngredientsGroup);

			// Trả về một thông báo thành công hoặc các thông tin khác cần thiết
			return Ok("IngredientsGroup created successfully");
		}

		// PUT api/<TypeController>/5
		[HttpPut("{id}")]
		public IActionResult UpdateIngredientsGroup(int id, [FromBody] IngredientsGroupDTO updatedIngredientsGroupDTO)
		{
			if (updatedIngredientsGroupDTO == null || id != updatedIngredientsGroupDTO.IngredientId)
			{
				return BadRequest("Invalid IngredientsGroup data");
			}

			// Kiểm tra xem cuộc thi có tồn tại không
			var existingIngredientsGroup = repository.GetIngredientsGroupById(id);
			if (existingIngredientsGroup == null)
			{
				return NotFound("IngredientsGroup not found");
			}

			// Cập nhật thông tin cuộc thi từ DTO
			existingIngredientsGroup.NameIngredients = updatedIngredientsGroupDTO.NameIngredients;
			existingIngredientsGroup.Description = updatedIngredientsGroupDTO.Description;


			// Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
			repository.UpdateIngredientsGroup(existingIngredientsGroup);

			// Trả về một thông báo thành công hoặc các thông tin khác cần thiết
			return Ok("IngredientsGroup updated successfully");
		}

		// DELETE api/<TypeController>/5
		[HttpDelete("{id}")]
		public IActionResult DeleteIngredientsGroup(int id)
		{
			var temp = repository.GetIngredientsGroupById(id);
			if (temp == null)
			{
				return NotFound();
			}
			repository.DeleteIngredientsGroup(temp);
			return NoContent();
		}
	}
}
