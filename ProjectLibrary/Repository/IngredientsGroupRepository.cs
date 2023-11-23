using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class IngredientsGroupRepository : IIngredientsGroupRepository
    {
        public List<IngredientsGroup> GetIngredientsGroups() => IngredientsGroupDAO.Instance.GetIngredientsGroups();
        public void SaveIngredientsGroup(IngredientsGroup ig) => IngredientsGroupDAO.Instance.SaveIngredientsGroup(ig);
        public IngredientsGroup GetIngredientsGroupById(int id) => IngredientsGroupDAO.Instance.GetIngredientsGroupById(id);
        public void DeleteIngredientsGroup(IngredientsGroup ig) => IngredientsGroupDAO.Instance.DeleteIngredientsGroup(ig);
        public void UpdateIngredientsGroup(IngredientsGroup ig) => IngredientsGroupDAO.Instance.UpdateIngredientsGroup(ig);
    }
}
