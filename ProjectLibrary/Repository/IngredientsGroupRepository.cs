using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class IngredientsGroupRepository : IIngredientsGroupRepository
    {
        public List<IngredientsGroup> GetIngredientsGroups() => IngredientsGroupDAO.Instance.GetIngredientsGroups();
        public IngredientsGroup GetIngredientsGroupById(int id) => IngredientsGroupDAO.Instance.GetIngredientsGroupById(id);
        public void SaveIngredientsGroup(IngredientsGroup ingredientsGroup) => IngredientsGroupDAO.Instance.SaveIngredientsGroup(ingredientsGroup);
        public void UpdateIngredientsGroup(IngredientsGroup ingredientsGroup) => IngredientsGroupDAO.Instance.UpdateIngredientsGroup(ingredientsGroup);
        public void DeleteIngredientsGroup(IngredientsGroup ingredientsGroup) => IngredientsGroupDAO.Instance.DeleteIngredientsGroup(ingredientsGroup);
    }
}
