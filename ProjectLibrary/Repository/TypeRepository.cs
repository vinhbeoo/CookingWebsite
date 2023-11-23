using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{

    public class TypeRepository : ITypeRepository
    {
        public List<DataAccess.Type> GetTypes() => TypeDAO.Instance.GetTypes();
        public void SaveType(DataAccess.Type t) => TypeDAO.Instance.SaveType(t);
        public DataAccess.Type GetTypeById(int id) => TypeDAO.Instance.GetTypeById(id);
        public void DeleteType(DataAccess.Type t) => TypeDAO.Instance.DeleteType(t);
        public void UpdateType(DataAccess.Type t) => TypeDAO.Instance.UpdateType(t);
    }
}
