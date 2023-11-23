using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IIngredientsGroupRepository
    {
        List<IngredientsGroup> GetIngredientsGroups();
        void SaveIngredientsGroup(IngredientsGroup ind);
        IngredientsGroup GetIngredientsGroupById(int id);
        void DeleteIngredientsGroup(IngredientsGroup ind);
        void UpdateIngredientsGroup(IngredientsGroup ind);
    }
}
