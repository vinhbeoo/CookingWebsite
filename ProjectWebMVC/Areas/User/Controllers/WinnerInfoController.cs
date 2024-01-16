using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectLibrary.ObjectBussiness;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjectWebMVC.Areas.User.Controllers
{
	[Area("User")]
	[Authorize(Roles = "User")]
	[Authorize(AuthenticationSchemes = "User")]
	public class WinnerInfoController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string _url;

		public WinnerInfoController()
		{
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_url = "https://localhost:7269/api/WinnerInfo";
		}

		public async Task<ActionResult> Index()
		{
			HttpResponseMessage responseMessage;
			List<WinnerInfo> winnerInfoList = new List<WinnerInfo>();

			try
			{
				responseMessage = await _httpClient.GetAsync(_url);

				if (responseMessage.IsSuccessStatusCode)
				{
					var data = await responseMessage.Content.ReadAsStringAsync();
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true,
					};

					winnerInfoList = System.Text.Json.JsonSerializer.Deserialize<List<WinnerInfo>>(data, options);

					// Lấy danh sách UserId từ WinnerInfo
					var userIds = winnerInfoList.Select(w => w.WinnerUserId.GetValueOrDefault()).ToList();

					// Gọi action mới để lấy danh sách UserDetail
					var userDetailList = await GetUserDetails(userIds);

					// Truyền danh sách UserDetail vào view
					return View("WinnerIndex", userDetailList);
				}
				else
				{
					ModelState.AddModelError(string.Empty, $"Error: {responseMessage.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
			}

			// Nếu có lỗi, trả về view với danh sách rỗng hoặc xử lý tùy thuộc vào yêu cầu của bạn
			return View("WinnerIndex", new List<UserDetail>());
		}


		private async Task<List<UserDetail>> GetUserDetails(List<int> userIds)
		{
			try
			{
				var userDetailList = new List<UserDetail>();

				foreach (var userId in userIds)
				{
					var apiUrl = $"https://localhost:7269/api/UserDetail/{userId}";
					var response = await _httpClient.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						var responseData = await response.Content.ReadAsStringAsync();
						var userDetail = JsonConvert.DeserializeObject<UserDetail>(responseData);
						userDetailList.Add(userDetail);
					}
					else if (response.StatusCode == HttpStatusCode.NotFound)
					{
						// Nếu không tìm thấy UserDetail, có thể xử lý tùy thuộc vào yêu cầu của bạn
						// Ví dụ: userDetailList.Add(new UserDetail { UserId = userId, UserName = "Not Found" });
					}
					else
					{
						// Xử lý lỗi từ API
						var errorMessage = await response.Content.ReadAsStringAsync();
						throw new Exception($"API error: {errorMessage}");
					}
				}

				return userDetailList;
			}
			catch (Exception ex)
			{
				throw new Exception($"API error: {ex}");
			}
		}
	}
}
