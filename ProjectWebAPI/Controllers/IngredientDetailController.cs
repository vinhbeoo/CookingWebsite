using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientDetailController : ControllerBase
    {
        private IIngredientsDetailRepository repository = new IngredientsDetailRepository();

        // GET: api/<TypeController>
        [HttpGet]
        public ActionResult<IEnumerable<IngredientsDetail>> GetIngredientsDetails() => repository.GetIngredientsDetails();

        // GET api/<TypeController>/5
        [HttpGet("{id}")]
        public ActionResult<IngredientsDetail> GetIngredientsDetailById(int id)
        {
            var ind = repository.GetIngredientsDetailById(id);
            if (ind == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return ind;
        }

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
                Description = ingredientsDetailDTO.Description
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveIngredientsDetail(newIngredientsDetail);

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
            var existingIngredientsDetail = repository.GetIngredientsDetailById(id);
            if (existingIngredientsDetail == null)
            {
                return NotFound("IngredientsDetail not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingIngredientsDetail.IngredientId = updatedIngredientsDetailDTO.IngredientId;
            existingIngredientsDetail.Stt = updatedIngredientsDetailDTO.Stt;
            existingIngredientsDetail.Description = updatedIngredientsDetailDTO.Description;

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateIngredientsDetail(existingIngredientsDetail);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("IngredientsDetail updated successfully");
        }

        // DELETE api/<TypeController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIngredientsDetail(int id)
        {
            var temp = repository.GetIngredientsDetailById(id);
            if (temp == null)
            {
                return NotFound();
            }
            repository.DeleteIngredientsDetail(temp);
            return NoContent();
        }
    }
}
