namespace ProjectWebAPI.Application
{
    public class ContestDTO
    {
        public int ContestId { get; set; }
        public string ContestName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int OwnerUserId { get; set; }
    }
}
