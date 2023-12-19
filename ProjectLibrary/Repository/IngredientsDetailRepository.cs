using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
	public class IngredientsDetailRepository : IIngredientsDetailRepository
	{
		public List<IngredientsDetail> GetIngredientDetails() => IngredientsDetailDAO.Instance.GetIngredientsDetails();
		public List<IngredientsDetail> GetIngredientsDetailByRecipeId(int recipeId) => IngredientsDetailDAO.Instance.GetIngredientsDetailByRecipeId(recipeId);
		public void SaveIngredientDetail(IngredientsDetail ingredientDetail) => IngredientsDetailDAO.Instance.SaveIngredientsDetail(ingredientDetail);
		public IngredientsDetail GetIngredientDetailById(int id) => IngredientsDetailDAO.Instance.GetIngredientsDetailById(id);
		public void DeleteIngredientDetail(IngredientsDetail ingredientDetail) => IngredientsDetailDAO.Instance.DeleteIngredientsDetail(ingredientDetail);
		public void UpdateIngredientDetail(IngredientsDetail ingredientDetail) => IngredientsDetailDAO.Instance.UpdateIngredientsDetail(ingredientDetail);
	}
}
