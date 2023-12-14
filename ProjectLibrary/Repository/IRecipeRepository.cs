using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IRecipeRepository
    {
        List<Recipe> GetRecipes();
		List<Recipe> GetRecipeListById(int recipeId);
		Recipe GetRecipeById(int id);
        void SaveRecipe(Recipe recipe, int userId);
        void UpdateRecipe(Recipe recipe, int userId);
        void DeleteRecipe(Recipe recipe, int userId);
    }
}
