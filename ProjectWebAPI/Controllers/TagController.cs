using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ITagRepository repository = new TagRepository();

        // GET: api/<TypeController>
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTypes() => repository.GetTags();

        // GET api/<TypeController>/5
        [HttpGet("{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            var tag = repository.GetTagById(id);
            if (tag == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }
            return tag;
        }

        // POST api/<TypeController>
        [HttpPost]
        public IActionResult CreateType([FromBody] TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return BadRequest("Invalid tag data");
            }

            // Chuyển đổi từ DTO sang đối tượng Contest
            var newTag = new Tag
            {
                TagId = tagDTO.TagId,
                TagName = tagDTO.TagName
            };

            // Gọi dịch vụ để thêm cuộc thi vào cơ sở dữ liệu
            repository.SaveTag(newTag);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Tage created successfully");
        }

        // PUT api/<TypeController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateTage(int id, [FromBody] TagDTO updatedTagDTO)
        {
            if (updatedTagDTO == null || id != updatedTagDTO.TagId)
            {
                return BadRequest("Invalid tag data");
            }

            // Kiểm tra xem cuộc thi có tồn tại không
            var existingTag = repository.GetTagById(id);
            if (existingTag == null)
            {
                return NotFound("Type not found");
            }

            // Cập nhật thông tin cuộc thi từ DTO
            existingTag.TagName = updatedTagDTO.TagName;


            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            repository.UpdateTag(existingTag);

            // Trả về một thông báo thành công hoặc các thông tin khác cần thiết
            return Ok("Tag updated successfully");
        }

        // DELETE api/<TypeController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var temp = repository.GetTagById(id);
            if (temp == null)
            {
                return NotFound();
            }
            repository.DeleteTag(temp);
            return NoContent();
        }
    }
}
