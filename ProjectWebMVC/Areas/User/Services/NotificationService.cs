using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Mvc;

namespace ProjectWebMVC.Areas.User.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private string _userApiUrl = "https://localhost:7269/api/Notification";

        public NotificationService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Notification>> GetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_userApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Notification> notificationList = JsonSerializer.Deserialize<List<Notification>>(strData, options);
                return notificationList;
            }

            return new List<Notification>();
        }
    }
}