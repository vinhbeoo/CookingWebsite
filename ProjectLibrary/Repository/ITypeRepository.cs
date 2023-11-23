using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{

    public interface ITypeRepository
    {
        List<ObjectBussiness.Type> GetTypes();
        void SaveType(ObjectBussiness.Type t);
        ObjectBussiness.Type GetTypeById(int id);
        void DeleteType(ObjectBussiness.Type t);
        void UpdateType(ObjectBussiness.Type t);
    }
}
