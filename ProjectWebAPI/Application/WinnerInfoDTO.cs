namespace ProjectWebAPI.Application
{
    public class WinnerInfoDTO
    {
        public int WinnerId { get; set; }

        public int? ContestId { get; set; }

        public int? WinnerUserId { get; set; }

        public DateTime? WinningDate { get; set; }

        public string? Prize { get; set; }

        public int? Vote { get; set; }
    }
}
