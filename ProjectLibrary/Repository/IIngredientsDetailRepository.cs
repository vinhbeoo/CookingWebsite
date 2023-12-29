using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IIngredientsDetailRepository
    {
		List<IngredientsDetail> GetIngredientDetails();
		List<IngredientsDetail> GetIngredientsDetailByRecipeId(int recipeId);
		void SaveIngredientDetail(IngredientsDetail ingredientDetail);
		IngredientsDetail GetIngredientDetailById(int id);
		void DeleteIngredientDetail(IngredientsDetail ingredientDetail);
		void UpdateIngredientDetail(IngredientsDetail ingredientDetail);
	}
}
