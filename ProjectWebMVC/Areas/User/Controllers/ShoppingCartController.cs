using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.ObjectBussiness;
using ProjectWebMVC.Areas.User.Models;
using ProjectWebMVC.Areas.User.Services;
using System.Net.Http.Headers;

namespace ProjectWebMVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = "User")]
    public class ShoopingCartController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly IVnPayService _vnPayService;
        private readonly CookingWebsiteContext _db;
        private readonly INotificationService notificationService;

        public ShoopingCartController(IVnPayService vnPayService, CookingWebsiteContext db, INotificationService notificationService)
        {
            _vnPayService = vnPayService;
            this._db = db;
            this.notificationService = notificationService;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _url = "https://localhost:7269/api";
        }

        public async  Task<IActionResult> Index()
        {
            ViewBag.Notifications = await this.notificationService.GetAsync();
            return View();
        }

        public async Task<IActionResult> CreatePaymentUrlAsync(PaymentInformationModel model)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url);
            try
            {
                // Gọi dịch vụ Vnpay để lấy URL thanh toán
                (string paymmentUrl, string transactionRef) = _vnPayService.CreatePaymentUrl(model, HttpContext);

                var claimUserId = User.Claims.FirstOrDefault(x => x.Type == "UserId");
                
                // User not login
                if (claimUserId == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                // Lưu thông tin thanh toán vào UserPaymentHistory
                var paymentHistory = new UserPaymentHistory
                {
                    UserId = int.Parse(claimUserId.Value.ToString()),
                    TransactionRef = transactionRef
                };
               
                // Sử dụng Entity Framework hoặc phương thức lưu dữ liệu tương ứng để lưu vào cơ sở dữ liệu
                _db.UserPaymentHistories.Add(paymentHistory);
                _db.SaveChanges();

                return Redirect(paymmentUrl);
            }
            catch (Exception ex)
            {
                // Chuyển hướng đến trang lỗi
                return RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> PaymentCallback()
        {
            
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response.Success)
            {
                // Logic khi thanh toán thành công
                var transactionRef = response.OrderId;

                // Tìm trong UserPaymentHistory bằng TransactionRef
                var paymentHistory = _db.UserPaymentHistories
                    .FirstOrDefault(p => p.TransactionRef == transactionRef);

                if (paymentHistory != null)
                {

                    // Kiểm tra xem user có tồn tại không
                    var user = _db.Users.FirstOrDefault(u => u.UserId == paymentHistory.UserId);

                    // đổi trạng thái user
                    if (user != null)
                    {

                        user.UserType = 2;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        _db.SaveChanges();
                    }

                    double amount = response.Amount / 10;
                    // lưu lại thông tin userreghistory
                    string subcriptionType = "Unknown";

                    if (amount == 1000000)
                    {
                        subcriptionType = "Yearly";
                    }

                    if (amount == 100000)
                    {
                        subcriptionType = "Monthly";
                    }

                    DateTime startDate = DateTime.Now;
                    DateTime endDate = DateTime.Now.AddDays(-1); // Het han
                    
                    if (subcriptionType == "Yearly")
                    {
                        endDate = startDate.AddYears(1);
                    }

                    if (subcriptionType == "Monthly")
                    {
                        endDate = startDate.AddMonths(1);
                    }

                    string memberType = "Unknown";

                    if (subcriptionType == "Yearly")
                    {
                        memberType = "Thành viên năm";
                    }

                    if (subcriptionType == "Monthly")
                    {
                        memberType = "Thành viên tháng";
                    }

                    var userRegHistory = new UserRegHistory()
                    {
                        UserId = paymentHistory.UserId,
                        SubscriptionType = subcriptionType,
                        StartDate = startDate,
                        EndDate = endDate,
                        MemberType = memberType,
                        Amount = Convert.ToDecimal(response.Amount)
                    };

                    _db.UserRegHistories.Add(userRegHistory);
                    _db.SaveChanges();
                }
            }
            else
            {
                ViewBag.Notifications = await notificationService.GetAsync();
                return View();
            }

            // Assume that you have the PaymentInformationModel available, you need to retrieve it based on your logic
            var paymentInformation = new PaymentInformationModel
            {
                // Populate the properties based on your logic
            };

            var viewModel = new PaymentViewModel
            {
                PaymentResponse = response,
                PaymentInformation = paymentInformation
            };
            ViewBag.Notifications = await notificationService.GetAsync();
            // Chuyen huong den trang thong bao thanh toan thanh cong
            return View(viewModel);
        }
    }
}
