namespace ProjectWebAPI.Application
{
    public class RatingDTO
    {
        public int RatingId { get; set; }

        public int? UserId { get; set; }

        public int? ContestId { get; set; }

        public int? RecipeId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Vote { get; set; }
    }
}
