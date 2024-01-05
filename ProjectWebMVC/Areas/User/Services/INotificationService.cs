using ProjectLibrary.ObjectBussiness;

namespace ProjectWebMVC.Areas.User.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAsync();
    }
}