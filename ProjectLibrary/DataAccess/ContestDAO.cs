using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    /// <summary>
    /// Đây là lớp DAO (Data Access Object) dùng để thao tác với cơ sở dữ liệu liên quan đến cuộc thi.
    /// </summary>
    public class ContestDAO
    {
        private static ContestDAO instance = null;
        private static readonly object instanceLock = new object();

        //Thể hiện duy nhất của lớp ContestDAO, sử dụng mẫu thiết kế Singleton.
        public static ContestDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ContestDAO();
                    }
                    return instance;
                }
            }
        }

        //Lấy danh sách tất cả cuộc thi từ cơ sở dữ liệu.
        public List<Contest> GetContests()
        {
            var list = new List<Contest>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.Contests.ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ném ngoại lệ mới với thông điệp lỗi.
                throw new Exception("Error retrieving contest list: " + ex.Message);
            }
            return list;
        }

        /// <summary>
        /// Tìm kiếm một cuộc thi theo ID.
        /// </summary>
        /// <param name="contestId">ID của cuộc thi cần tìm kiếm.</param>
        /// <returns>Contest: Đối tượng cuộc thi được tìm thấy.</returns>
        public Contest FindContestById(int contestId)
        {
            Contest contest = new Contest();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    contest = context.Contests.FirstOrDefault(x => x.ContestId == contestId);
                }
                if (contest == null)
                {
                    throw new Exception("Contest doesn't exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return contest;
        }

        /// <summary>
        /// Lưu một cuộc thi mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="contest">Contest: Đối tượng cuộc thi cần lưu.</param>
        public void SaveContest(Contest contest, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra trùng lặp trước khi thêm mới
                    var existingContest = context.Contests.FirstOrDefault(x => x.ContestId == contest.ContestId);
                    if (existingContest != null)
                    {
                        // Xử lý trường hợp trùng lặp (ví dụ: ném ngoại lệ hoặc cập nhật đối tượng đã tồn tại)
                        throw new Exception("Contest already exists");
                    }

                    // Thêm đối tượng vào cơ sở dữ liệu
                    context.Contests.Add(contest);
                    context.SaveChanges();
                    // Log user activity for adding a contest
                    context.LogUserActivity(userId, "CreateContest", $"Created a new contest with ID {contest.ContestId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một cuộc thi trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="contest">Contest: Đối tượng cuộc thi cần cập nhật.</param>
        public void UpdateContest(Contest contest, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Truy vấn đối tượng cần cập nhật từ cơ sở dữ liệu
                    var existingContest = context.Contests.FirstOrDefault(x => x.ContestId == contest.ContestId);

                    if (existingContest != null)
                    {
                        // Sao chép dữ liệu từ đối tượng đầu vào (c) vào đối tượng đã truy vấn được (existingContest)
                        context.Entry(existingContest).CurrentValues.SetValues(contest);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "UpdateContest", $"Updated contest with ID {contest.ContestId}");
                    }
                    else
                    {
                        // Xử lý trường hợp đối tượng không tồn tại
                        throw new Exception("Contest not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một cuộc thi trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="c">Contest: Đối tượng cuộc thi cần xóa.</param>
        public void DeleteContest(Contest contest, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var contestDel = context.Contests.FirstOrDefault(x => x.ContestId == contest.ContestId);
                    if (contestDel == null)
                    {
                        // Nếu đối tượng không tồn tại, đưa ra thông báo lỗi
                        throw new Exception("Contest is null");
                    }
                    else
                    {
                        context.Contests.Remove(contestDel);
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "DeleteContest", $"Deleted contest with ID {contest.ContestId}");
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
