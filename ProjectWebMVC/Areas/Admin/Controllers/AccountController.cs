using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectWebAPI.Models;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _userApiUrl = "";

        public AccountController()
        {
            _httpClient = new HttpClient();
            var contextType = new MediaTypeWithQualityHeaderValue("application/json");
            _userApiUrl = "https://localhost:7269/api/User";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_userApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<ProjectLibrary.ObjectBussiness.User> userList = JsonSerializer.Deserialize<List<ProjectLibrary.ObjectBussiness.User>>(strData, options);
                return View(userList);
            }
            else
            {
                return View(new List<ProjectLibrary.ObjectBussiness.User>());
            }
        }

        public async Task<IActionResult> Details(string identifier)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_userApiUrl}/{identifier}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
				ProjectLibrary.ObjectBussiness.User user = JsonSerializer.Deserialize<ProjectLibrary.ObjectBussiness.User>(strData, options);
                return View(user);
            }
            else
            {
                return View(new ProjectLibrary.ObjectBussiness.User());
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                string loginData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(loginData, System.Text.Encoding.UTF8, "application/json");
                string loginApiUrl = $"{_userApiUrl}/login";
                HttpResponseMessage response = await _httpClient.PostAsync(loginApiUrl, contentData);

                if (response.IsSuccessStatusCode)
                {
                    var strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
					ProjectLibrary.ObjectBussiness.User user = JsonSerializer.Deserialize<ProjectLibrary.ObjectBussiness.User>(strData, options);
                    if (!user.EmailConfirmed)
                    {
                        TempData["Message"] = "Email chưa được xác thực.";
                        return View("NotificationEmailComfirm");
                    }
                    // Xử lý các trường hợp RoleId
                    switch (user.RoleId)
                    {
                        case 1:
                            return RedirectToAction("Index", "Admin");
                        case 2:
                            return RedirectToAction("Index", "Customer");
                        // Xử lý các RoleId khác tại đây
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError, "Unhandled RoleId.");
                    }
                }
                else
                {
                    // Đăng nhập thất bại, xử lý lỗi hoặc hiển thị thông báo
                    string errorData = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error: {errorData}");
                    return View("Login", model);
                }
            }
            catch
            {
                // Xử lý ngoại lệ
                return View("Error");
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                string userData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(userData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_userApiUrl}/register", contentData);
                if (response.IsSuccessStatusCode)
                {
                    // Đăng ký thành công, hiển thị thông báo xác nhận email
                    ViewData["Email"] = model.Email;
                    return View("EmailAuthentication");
                }
                else
                {
                    // Xử lý lỗi, đọc đối tượng JSON từ response
                    string errorData = await response.Content.ReadAsStringAsync();
                    var error = JsonSerializer.Deserialize<ErrorResponseModel>(errorData);

                    // Lưu thông báo lỗi vào TempData
                    TempData["ErrorMessage"] = error?.Message;

                    // Redirect về view NotificationEmailConfirm
                    return View("NotificationEmailConfirm");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, lưu thông báo lỗi vào TempData
                TempData["ErrorMessage"] = ex.Message;

                // Redirect về view Register
                return RedirectToAction("Register");
            }
        }

        public IActionResult ConfirmEmail()
        {
            // Lấy giá trị từ đường dẫn query string
            string email = HttpContext.Request.Query["email"];
            string token = HttpContext.Request.Query["token"];

            // Tạo và gán giá trị cho ConfirmEmailModel
            ConfirmEmailModel model = new ConfirmEmailModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
        {
            try
            {
                string confirmData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(confirmData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_userApiUrl}/confirm-email", contentData);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Login");
                }
                TempData["ErrorMessage"] = "Invalid response from server.";
                return View("NotificationEmailConfirm");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, lưu thông báo lỗi vào TempData
                TempData["ErrorMessage"] = ex.Message;

                // Redirect về view NotificationEmailConfirm
                return View("NotificationEmailConfirm");
            }
        }
    }
}
