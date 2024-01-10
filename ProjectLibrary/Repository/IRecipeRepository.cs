using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IRecipeRepository
    {
        List<Recipe> GetRecipes();
        List<Recipe> GetRecipeListByUserId(int userId);

        List<Recipe> GetRecipeListById(int recipeId);
		Recipe GetRecipeById(int id);
        void SaveRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
    }
}
