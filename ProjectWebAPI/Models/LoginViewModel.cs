using System.ComponentModel.DataAnnotations;

namespace ProjectWebAPI.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Phải nhập {0}")]
		[Display(Name = "UserName")]
		public string UserName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; }
	}
}
