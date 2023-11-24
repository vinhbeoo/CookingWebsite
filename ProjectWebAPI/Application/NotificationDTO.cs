namespace ProjectWebAPI.Application
{
    public class NotificationDTO
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
    }
}
