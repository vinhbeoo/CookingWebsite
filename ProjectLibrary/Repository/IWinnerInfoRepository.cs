using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IWinnerInfoRepository
    {
        void SaveWinnerInfo(WinnerInfo winnerInfo);
        WinnerInfo GetWinnerInfoById(int id);
        void DeleteWinnerInfo(WinnerInfo winnerInfo);
        void UpdateWinnerInfo(WinnerInfo winnerÌno);
        List<WinnerInfo> GetWinnerInfos();
    }
}
