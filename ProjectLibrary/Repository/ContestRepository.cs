using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class ContestRepository : IContestRepository
    {
        public List<Contest> GetContests() => ContestDAO.Instance.GetContests();
        public void SaveContest(Contest c, int userId) => ContestDAO.Instance.SaveContest(c, userId);
        public Contest GetContestById(int id) => ContestDAO.Instance.FindContestById(id);
        public void DeleteContest(Contest c, int userId) => ContestDAO.Instance.DeleteContest(c, userId);
        public void UpdateContest(Contest c, int userId) => ContestDAO.Instance.UpdateContest(c, userId);
    }
}
