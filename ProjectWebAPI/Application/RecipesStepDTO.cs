namespace ProjectWebAPI.Application
{
    public class RecipesStepDTO
    {
        public int Step { get; set; }

        public int? RecipeId { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }
    }
}
