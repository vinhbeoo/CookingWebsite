using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface INotificationRepository
    {
        List<Notification> GetNotifications();
        Notification GetNotificationById(int id);
        void SaveNotification(Notification notification, int userId);
        void UpdateNotification(Notification notification, int userId);
        void DeleteNotification(Notification notification, int userId);
    }
}
