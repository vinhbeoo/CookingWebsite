using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private ITypeRepository repository = new TypeRepository();

        // GET: api/<TypeController>
        [HttpGet]
        public ActionResult<IEnumerable<ProjectLibrary.ObjectBussiness.Type>> GetTypes() => repository.GetTypes();

        // GET api/<TypeController>/5
        [HttpGet("{id}")]
        public ActionResult<ProjectLibrary.ObjectBussiness.Type> GetTypeById(int id)
        {
            var type = repository.GetTypeById(id);
            if (type == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return type;
        }

        // POST api/<TypeController>
        [HttpPost]
        public IActionResult CreateType([FromBody] TypeDTO typeDTO)
        {
            if (typeDTO == null)
            {
                return BadRequest("Invalid type data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newType = new ProjectLibrary.ObjectBussiness.Type
            {
                TypeId = typeDTO.TypeId,
                TypeName = typeDTO.TypeName
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveType(newType);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Type created successfully");
        }

        // PUT api/<TypeController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateType(int id, [FromBody] TypeDTO updatedTypeDTO)
        {
            if (updatedTypeDTO == null || id != updatedTypeDTO.TypeId)
            {
                return BadRequest("Invalid type data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingType = repository.GetTypeById(id);
            if (existingType == null)
            {
                return NotFound("Type not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingType.TypeName = updatedTypeDTO.TypeName;
            

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateType(existingType);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Type updated successfully");
        }

        // DELETE api/<TypeController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteType(int id)
        {
            var temp = repository.GetTypeById(id);
            if (temp == null)
            {
                return NotFound();
            }
            repository.DeleteType(temp);
            return NoContent();
        }
    }
}
