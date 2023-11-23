using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IIngredientsDetailRepository
    {
        List<IngredientsDetail> GetIngredientsDetails();
        void SaveIngredientsDetail(IngredientsDetail ind);
        IngredientsDetail GetIngredientsDetailById(int id);
        void DeleteIngredientsDetail(IngredientsDetail ind);
        void UpdateIngredientsDetail(IngredientsDetail ind);
    }
}
