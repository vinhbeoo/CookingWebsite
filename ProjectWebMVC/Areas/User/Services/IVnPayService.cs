using ProjectWebMVC.Areas.User.Models;

namespace ProjectWebMVC.Areas.User
{
    public interface IVnPayService
    {
        (string PaymmentUrl, string TransactionRef) CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
