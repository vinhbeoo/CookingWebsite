using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        public List<Recipe> GetRecipes() => RecipeDAO.Instance.GetRecipes();
		public List<Recipe> GetRecipeListById(int id) => RecipeDAO.Instance.GetRecipeListById(id);
		public Recipe GetRecipeById(int id) => RecipeDAO.Instance.GetRecipeById(id);
        public void SaveRecipe(Recipe recipe) => RecipeDAO.Instance.SaveRecipe(recipe);
        public void UpdateRecipe(Recipe recipe) => RecipeDAO.Instance.UpdateRecipe(recipe);
        public void DeleteRecipe(Recipe recipe) =>RecipeDAO.Instance.DeleteRecipe(recipe);
    }
}
