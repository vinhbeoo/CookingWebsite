using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.Application;
using ProjectWebAPI.Models;
using System.Net.Mail;
using System.Net;
using ProjectWebMVC.Areas.App.Code;
using ProjectLibrary.DataAccess;
using System;
using ProjectWebAPI.App.Code;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repository = new UserRepository();
        /*private IUserDetailRepository detailRepository = new UserDetailRepository();*/
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers() => repository.GetUsers();


        [HttpGet("{input}")]
        public async Task<IActionResult> GetUserByEmailOrUserName(string input)
        {
            var user = await repository.GetUserByEmailOrUserName(input);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await repository.GetUserByEmailOrUserName(model.UserName);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Mã hóa mật khẩu trong model để so sánh với mật khẩu đã lưu trong cơ sở dữ liệu
                var encryptedPassword = Common.EncryptMD5(model.Password);

                if (user.Password != encryptedPassword)
                {
                    return Unauthorized("Invalid credentials.");
                }
                /*LogUserActivity.LogUserLoginActivity(user.UserId);*/
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during login: " + ex.Message);
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Password and confirmation password do not match.");
            }
			if (string.IsNullOrWhiteSpace(model.UserName))
			{
                return BadRequest("UserName is null");
			}
			if (string.IsNullOrWhiteSpace(model.Email))
			{
				return BadRequest("Email is null");
			}
			if (string.IsNullOrWhiteSpace(model.Password))
			{
				return BadRequest("Password is null");
			}
			if (string.IsNullOrWhiteSpace(model.ConfirmPassword))
			{
				return BadRequest("ConfirmPassword is null");
			}

			try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra xem UserName đã tồn tại chưa
                    var existingUserName = await repository.GetUserByEmailOrUserName(model.UserName);
                    if (existingUserName != null)
                    {
                        return BadRequest("UserName is already taken.");
                    }

                    // Kiểm tra xem Email đã tồn tại chưa
                    var existingEmail = await repository.GetUserByEmailOrUserName(model.Email);
                    if (existingEmail != null)
                    {
                        return BadRequest("Email is already taken.");
                    }

                    // Tiếp tục với logic đăng ký nếu mật khẩu và mật khẩu nhập lại khớp nhau
                    var user = new User
                    {
                        UserName = model.UserName,
                        Password = Common.EncryptMD5(model.Password),
                        Email = model.Email,
                        EmailConfirmed = false,
                        EmailConfirmationToken = Guid.NewGuid().ToString(),
                        RoleId = 2, // Mặc định giá trị cho RoleId
                        Status = "Hoạt động", // Mặc định giá trị cho Status
                        UserType = 1

                    };

                    repository.SaveUser(user);

                    LogUserActivity.LogUserRegistrationActivity(user.UserId);

                    // Gửi email xác nhận
                    SendConfirmationEmail(user.Email, user.EmailConfirmationToken);

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error registering user: " + ex.Message);
            }
        }

        private void SendConfirmationEmail(string email, string token)
        {
            // Tạo link xác nhận từ token và gửi email
            string confirmationLink = $"https://localhost:7178/Admin/Account/ConfirmEmail?email={email}&token={token}";
            string subject = "Confirm Your Email";
            string body = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.";
            SendEmail(email, subject, body);

        }

        private void SendEmail(string to, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("jamesthewchess@gmail.com"); // Điền email của bạn ở đây
                    mailMessage.To.Add(to);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true; // Có thể sử dụng HTML trong nội dung email

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.Credentials = new NetworkCredential("jamesthewchess@gmail.com", "lqrz rman kncd tsgh"); // Điền email và mật khẩu của trang web
                        smtp.EnableSsl = true; // Sử dụng SSL

                        smtp.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending email: " + ex.Message);
            }
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
        {
            try
            {
                var result = await repository.ConfirmEmailAsync(model.Email, model.Token);

                if (result)
                {
                    // Xác thực thành công, trả về true
                    return Ok(new { success = true, message = "Email confirmed successfully." });
                }
                else
                {
                    // Xác thực thất bại, trả về false
                    return BadRequest(new { success = false, message = "Invalid email or token." });
                }
            }
            catch (Exception ex)
            {
                // Lỗi xác thực, trả về thông báo lỗi
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
