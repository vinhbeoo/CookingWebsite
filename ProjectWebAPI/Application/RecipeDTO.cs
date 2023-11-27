namespace ProjectWebAPI.Application
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }

        public string RecipeTitle { get; set; } = null!;

        public string ImageTitle { get; set; } = null!;

        public int Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public int TagId { get; set; }

        public string Description { get; set; } = null!;

        public string VideoUrl { get; set; }

        public string PrepTime { get; set; } = null!;

        public string CookTime { get; set; } = null!;

        public string TotalTime { get; set; } = null!;

        public string Servings { get; set; } = null!;

        public string Calories { get; set; } = null!;

        public string Fat { get; set; }

        public string Carbs { get; set; }

        public string Protein { get; set; }

        public int? CategoryId { get; set; }

        public int? ContestId { get; set; }

        public int Rating { get; set; }

        public Boolean ReadFree { get; set; }
    }
}
