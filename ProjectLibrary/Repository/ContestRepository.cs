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
        public void SaveContest(Contest c) => ContestDAO.Instance.SaveContest(c);
        public Contest GetContestById(int id) => ContestDAO.Instance.FindContestById(id);
        public void DeleteContest(Contest c) => ContestDAO.Instance.DeleteContest(c);
        public void UpdateContest(Contest c) => ContestDAO.Instance.UpdateContest(c);
    }
}
