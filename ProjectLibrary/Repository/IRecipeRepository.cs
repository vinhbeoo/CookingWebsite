using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IRecipeRepository
    {
        List<Recipe> GetRecipes();
        void SaveRecipe(Recipe r);
        Recipe GetRecipeById(int id);
        void DeleteRecipe(Recipe r);
        void UpdateRecipe(Recipe r);
    }
}
