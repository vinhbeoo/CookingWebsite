using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IRecipeRepository
    {
        List<Recipe> GetRecipes();
        Recipe GetRecipeById(int id);
        void SaveRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
    }
}
