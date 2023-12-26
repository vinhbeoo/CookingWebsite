using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;
using ProjectWebMVC.Services;

namespace ProjectWebMVC.Controllers
{
	public class ShoopingCartController : Controller
	{
		private readonly IVnPayService _vnPayService;

		public ShoopingCartController(IVnPayService vnPayService)
		{
			_vnPayService = vnPayService;
		}

		public IActionResult Index()
		{
			return View();
		}


		public IActionResult CreatePaymentUrl(PaymentInformationModel model)
		{
			var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

			return Redirect(url);
		}

		public IActionResult PaymentCallback()
		{
			var response = _vnPayService.PaymentExecute(Request.Query);

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

			return View(viewModel);
		}
	}
}

