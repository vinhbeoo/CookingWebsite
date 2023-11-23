using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class IngredientsDetailRepository : IIngredientsDetailRepository
    {

        public List<IngredientsDetail> GetIngredientsDetails() => IngredientsDetailDAO.Instance.GetIngredientsDetails();
        public void SaveIngredientsDetail(IngredientsDetail ind) => IngredientsDetailDAO.Instance.SaveIngredientsDetail(ind);
        public IngredientsDetail GetIngredientsDetailById(int id) => IngredientsDetailDAO.Instance.GetIngredientsDetailById(id);
        public void DeleteIngredientsDetail(IngredientsDetail ind) => IngredientsDetailDAO.Instance.DeleteIngredientsDetail(ind);
        public void UpdateIngredientsDetail(IngredientsDetail ind) => IngredientsDetailDAO.Instance.UpdateIngredientsDetail(ind);
    }
}
