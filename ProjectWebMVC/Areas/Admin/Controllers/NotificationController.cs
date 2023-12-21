using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class NotificationController : Controller
	{
		// GET: NotificationController
		private readonly HttpClient _httpClient;
		private string NotificationUrl = "";

		public NotificationController()
		{
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			NotificationUrl = "https://localhost:7269/api/Notification";
		}
		// GET: NotificationController
		public async Task<IActionResult> Index()
		{
			HttpResponseMessage response = await _httpClient.GetAsync(NotificationUrl);
			if (response.IsSuccessStatusCode)
			{
				var strData = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				};
				List<Notification> notificationList = JsonSerializer.Deserialize<List<Notification>>(strData, options);

				return View(notificationList);
			}

			return View(new List<Notification>()); // Trả về một danh sách trống nếu có lỗi
		}


		// GET: NotificationController/Details/5
		public async Task<IActionResult> Details(int id)
		{
			HttpResponseMessage response = await _httpClient.GetAsync($"{NotificationUrl}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var strData = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				};
				Notification notification = JsonSerializer.Deserialize<Notification>(strData, options);
				return View(notification);
			}

			return NotFound();
		}


		// GET: NotificationController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: NotificationController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Notification notification)
		{
			if (ModelState.IsValid)
			{
				string strData = JsonSerializer.Serialize(notification);
				var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
				HttpResponseMessage res = await _httpClient.PostAsync(NotificationUrl, contentData);
				if (res.IsSuccessStatusCode)
				{
					TempData["Message"] = "notification inserted successfully";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					TempData["Message"] = "Error while call Web API";
				}
			}
			return View(notification);
		}


		// GET: NotificationController/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			HttpResponseMessage response = await _httpClient.GetAsync($"{NotificationUrl}/{id}");
			if (response.IsSuccessStatusCode)
			{
				var strData = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				};
				Notification notification = JsonSerializer.Deserialize<Notification>(strData, options);
				return View(notification);
			}

			return NotFound();
		}

		// POST: NotificationController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Notification notification)
		{
			var notificationData = JsonSerializer.Serialize(notification);
			var content = new StringContent(notificationData, System.Text.Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PutAsync($"{NotificationUrl}/{id}", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(notification);
		}

		// GET: NotificationController/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{NotificationUrl}/{id}");
			var data = await responseMessage.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			Notification notification = JsonSerializer.Deserialize<Notification>(data, options);
			return View(notification);
		}

		// POST: NotificationController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, IFormCollection collection)
		{
			if (ModelState.IsValid)
			{
				HttpResponseMessage response = await _httpClient.DeleteAsync($"{NotificationUrl}/{id}");
				if (response.IsSuccessStatusCode)
				{
					TempData["Message"] = "Notification delete successfully";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					TempData["Message"] = "Error while call Web API";
				}
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
