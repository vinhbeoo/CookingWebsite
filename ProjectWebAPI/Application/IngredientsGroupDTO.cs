using ProjectLibrary.DataAccess;

namespace ProjectWebAPI.Application
{
    public class IngredientsGroupDTO
    {
        public int IngredientId { get; set; }

        public string? NameIngredients { get; set; }

        public int? RecipeId { get; set; }

    }
}
