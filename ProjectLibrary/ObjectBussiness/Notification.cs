using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; } // Khóa ngoại tới Bảng Người Dùng
        public User User { get; set; }
    }
}
