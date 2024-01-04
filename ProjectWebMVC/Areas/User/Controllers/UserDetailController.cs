using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectWebAPI.Application;
using ProjectWebMVC.Areas.User.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ProjectWebMVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
    public class UserDetailController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private readonly INotificationService notificationService;
        private string UserDetailApiUrl = "";
        private string _userApiUrl = "https://localhost:7269/api/UserManager";

        public UserDetailController(INotificationService notificationService)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            UserDetailApiUrl = "https://localhost:7269/api/UserDetail";
            this.notificationService = notificationService;
        }
        // GET: UserDetailController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Notifications = await this.notificationService.GetAsync();
            return View();
        }

        // POST: UserDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, UserDetailDTO userDetailDTO)
        {
            var user = User as ClaimsPrincipal;
            var userName = user?.FindFirstValue(ClaimTypes.Name);
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            
                try
                {
                    // Kiểm tra xem người dùng có chọn ảnh mới hay không
                    if (file != null && file.Length > 0)
                    {
                        // Lưu ảnh mới
                        Random rnd = new Random();
                        string imageName = rnd.Next().ToString() + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/User/images", imageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Cập nhật đường dẫn ảnh mới vào userDetailDTO
                        userDetailDTO.Avatar = "/User/images/" + imageName;
                    }
                    else
                    {
                        // Nếu không có ảnh mới, gán giá trị mặc định cho Avatar
                        userDetailDTO.Avatar = "/User/Images/avatar.jpg";
                    }

                    userDetailDTO.UserId = int.Parse(userId.ToString());
                // Tiếp tục xử lý đối tượng UserDetailDTO và gửi nó đi như trước
                string strData = JsonSerializer.Serialize(userDetailDTO);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage res = await _httpClient.PostAsync(UserDetailApiUrl, contentData);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "UserDetail inserted successfully";
                        return RedirectToAction(nameof(Edit), new { id = userId });
                    }
                    else
                    {
                        TempData["Message"] = "Error while calling Web API";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = $"An error occurred: {ex.Message}";
                }

            ViewBag.Notifications = await notificationService.GetAsync();
            return View(userDetailDTO);
        }


        // GET: UserDetailController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{UserDetailApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                UserDetail userDetail = JsonSerializer.Deserialize<UserDetail>(strData, options);

                if (userDetail != null)
                {
                    // Lưu giữ giá trị Avatar vào TempData
                    TempData["CurrentAvatar"] = userDetail.Avatar;
                    ViewBag.Notifications = await notificationService.GetAsync();
                    return View(userDetail);
                }
                else
                {
                    // Nếu userDetail là null, chuyển hướng đến action Create
                    return RedirectToAction("Create");
                }
            }
            else
            {
                // Xử lý khi không thành công, có thể chuyển hướng hoặc hiển thị thông báo lỗi
                return RedirectToAction("Create");
            }
        }

        // POST: UserDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, UserDetailDTO userDetailDTO)
        {
            // Kiểm tra xem TempData có giữ giá trị Avatar không
            if (TempData.TryGetValue("CurrentAvatar", out object currentAvatarObj))
            {
                string currentAvatar = currentAvatarObj as string;

                // Kiểm tra xem người dùng có chọn ảnh mới hay không
                if (file != null && file.Length > 0)
                {
                    // Lưu ảnh mới
                    Random rnd = new Random();
                    string imageName = rnd.Next().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/User/images", imageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Cập nhật đường dẫn ảnh mới vào userDetailDTO
                    userDetailDTO.Avatar = "/User/images/" + imageName;
                }
                else
                {
                    // Nếu không có ảnh mới, giữ nguyên giá trị hiện tại của Avatar
                    userDetailDTO.Avatar = currentAvatar;
                }

                // Thực hiện cập nhật thông tin userDetailDTO vào API
                string strData = JsonSerializer.Serialize(userDetailDTO);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PutAsync($"{UserDetailApiUrl}/{id}", contentData);

                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "UserDetail updated successfully";
                    return RedirectToAction(nameof(Edit), new { id = id });
                }
                else
                {
                    TempData["Message"] = "Error while calling Web API";
                }
            }
            ViewBag.Notifications = await notificationService.GetAsync();
            return View(userDetailDTO);
        }

    }
}