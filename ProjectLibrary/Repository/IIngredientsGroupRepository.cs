using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IIngredientsGroupRepository
    {
        List<IngredientsGroup> GetIngredientsGroups();
        IngredientsGroup GetIngredientsGroupById(int id);
        void SaveIngredientsGroup(IngredientsGroup ingredientsGroup);
        void UpdateIngredientsGroup(IngredientsGroup ingredientsGroup);
        void DeleteIngredientsGroup(IngredientsGroup ingredientsGroup);
    }
}
