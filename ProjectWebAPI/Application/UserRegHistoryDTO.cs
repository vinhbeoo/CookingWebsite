namespace ProjectWebAPI.Application
{
    public class UserRegHistoryDTO
    {
        public int RegistrationId { get; set; }
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Amount { get; set; }
    }
}
