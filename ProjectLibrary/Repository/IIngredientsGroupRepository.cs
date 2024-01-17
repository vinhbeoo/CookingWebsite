using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IIngredientsGroupRepository
    {
        List<IngredientsGroup> GetIngredientsGroups();
        IngredientsGroup GetIngredientsGroupById(int id);
        IngredientsGroup GetIngredientsGroupByRecId(int recipeId);
        List<IngredientsGroup> GetIngredientsGroupsByRecipeId(int RecipeId);
		void SaveIngredientsGroup(IngredientsGroup ingredientsGroup);
        void UpdateIngredientsGroup(IngredientsGroup ingredientsGroup);
        void DeleteIngredientsGroup(IngredientsGroup ingredientsGroup);
    }
}
