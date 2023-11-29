using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    /// <summary>
    /// Đây là lớp DAO (Data Access Object) dùng để thao tác với cơ sở dữ liệu liên quan đến thông tin người thắng và giải thưởng của cuộc thi .
    /// </summary>
    public class WinnerInfoDAO
    {
        private static WinnerInfoDAO instance = null;
        private static readonly object instanceLock = new object();

        //Thể hiện duy nhất của lớp WinnerInfoDAO, sử dụng mẫu thiết kế Singleton.
        public static WinnerInfoDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WinnerInfoDAO();
                    }
                    return instance;
                }
            }
        }

        //Lấy danh sách tất cả người chiến thắng cùng giải thưởng của các cuộc thi từ cơ sở dữ liệu.
        public List<WinnerInfo> GetWinnerInfos()
        {
            var listWinnerInfos = new List<WinnerInfo>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    listWinnerInfos = context.WinnerInfos.ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ném ngoại lệ mới với thông điệp lỗi.
                throw new Exception("Error retrieving WinnerInfo list: " + ex.Message);
            }
            return listWinnerInfos;
        }

        public WinnerInfo FindWinnerInfoById(int winnerinfoId)
        {
            WinnerInfo winnerInfo = new WinnerInfo();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    winnerInfo = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winnerinfoId);
                }
                if (winnerInfo == null)
                {
                    throw new Exception("WinnerInfo doesn't exists");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return winnerInfo;
        }

        /// <summary>
        /// Lưu người chiến thắng và giải thưởng của một cuộc thi mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="winnerInfo"> Đối tượng thông tin người chiến thắng cùng giải thưởng cuộc thi cần lưu.</param>
        public void SaveWinnerInfo(WinnerInfo winnerInfo)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra trùng lặp trước khi thêm mới
                    var winner = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winnerInfo.WinnerId);
                    if (winner != null)
                    {
                        // Xử lý trường hợp trùng lặp (ví dụ: ném ngoại lệ hoặc cập nhật đối tượng đã tồn tại)
                        throw new Exception("WinnerInfo already exists");
                    }

                    // Thêm đối tượng vào cơ sở dữ liệu
                    context.WinnerInfos.Add(winnerInfo);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin người chiến thắng cùng giải thưởng của 1 cuộc thi trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="winnerInfo">WinnerInfo: Đối tượng chi tiết người thắng cùng giải thưởng cần cập nhật.</param>
        public void UpdateWinnerInfo(WinnerInfo winnerInfo)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Truy vấn đối tượng cần cập nhật từ cơ sở dữ liệu
                    var winner = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winnerInfo.WinnerId);

                    if (winner != null)
                    {
                        // Sao chép dữ liệu từ đối tượng đầu vào vào đối tượng đã truy vấn được
                        context.Entry(winner).CurrentValues.SetValues(winnerInfo);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                    }
                    else
                    {
                        // Xử lý trường hợp đối tượng không tồn tại
                        throw new Exception("WinnerInfo not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Xóa thông tin về người chiến thắng cùng giải thưởng của 1 cuộc thi trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="winnerInfo">WinnerInfo: Đối tượng chi tiết người chiến thắng cùng giải thưởng cần xóa.</param>
        public void DeleteWinnerInfo(WinnerInfo winnerInfo)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var winnerInfoDel = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winnerInfo.WinnerId);
                    if (winnerInfoDel == null)
                    {
                        // Nếu đối tượng không tồn tại, đưa ra thông báo lỗi
                        throw new Exception("WinnerInfo is null");
                    }
                    else
                    {
                        context.WinnerInfos.Remove(winnerInfoDel);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
