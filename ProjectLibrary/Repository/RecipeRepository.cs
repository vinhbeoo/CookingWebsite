using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        public List<Recipe> GetRecipes() => RecipeDAO.Instance.GetRecipes();
        public void SaveRecipe(Recipe r) => RecipeDAO.Instance.SaveRecipe(r);
        public Recipe GetRecipeById(int id) => RecipeDAO.Instance.GetRecipeById(id);
        public void DeleteRecipe(Recipe r) => RecipeDAO.Instance.DeleteRecipe(r);
        public void UpdateRecipe(Recipe r) => RecipeDAO.Instance.UpdateRecipe(r);
    }
}
