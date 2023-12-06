using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IContestRepository
    {
        List<Contest> GetContests();
        void SaveContest(Contest c,int userId);
        Contest GetContestById(int id);
        void DeleteContest(Contest c, int userId);
        void UpdateContest(Contest c, int userId);
    }
}
