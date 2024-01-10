using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    /// <summary>
    /// Đây là lớp DAO (Data Access Object) dùng để thao tác với cơ sở dữ liệu liên quan đến chi tiết thông tin người dùng.
    /// </summary>
    public class UserDetailDAO
    {
        private static UserDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        //Thể hiện duy nhất của lớp ContestDAO, sử dụng mẫu thiết kế Singleton.
        public static UserDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDetailDAO();
                    }
                    return instance;
                }
            }
        }

        //Lấy danh sách tất cả chi tiết người dùng từ cơ sở dữ liệu.
        public List<UserDetail> GetUserDetails()
        {
            var listUserDetails = new List<UserDetail>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    listUserDetails = context.UserDetails.Include(u => u.User).ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ném ngoại lệ mới với thông điệp lỗi.
                throw new Exception("Error retrieving UserDetail list: " + ex.Message);
            }
            return listUserDetails;
        }

        /// <summary>
        /// Tìm kiếm một chi tiết người dùng theo ID.
        /// </summary>
        /// <param name="userId">ID của chi tiết người dùng cần tìm kiếm.</param>
        /// <returns>Contest: Đối tượng chi tiết người dùng được tìm thấy.</returns>
        public UserDetail FindUserDetailById(int userId)
        {
            UserDetail userDetail = new UserDetail();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    userDetail = context.UserDetails.Include(u => u.User).FirstOrDefault(x => x.UserId == userId);
                    if (userDetail == null)
                    {
                        return null;
                    }
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return userDetail;
        }

        /// <summary>
        /// Lưu một chi tiết người dùng mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="userDetail">CserDetail: Đối tượng chi tiết người dùng cần lưu.</param>
        public void SaveUserDetail(UserDetail userDetail)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra trùng lặp trước khi thêm mới
                    var user = context.UserDetails.FirstOrDefault(x => x.UserId == userDetail.UserId);
                    if (user != null)
                    {
                        // Xử lý trường hợp trùng lặp (ví dụ: ném ngoại lệ hoặc cập nhật đối tượng đã tồn tại)
                        throw new Exception("UserDetail already exists");
                    }

                    // Thêm đối tượng vào cơ sở dữ liệu
                    context.UserDetails.Add(userDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một chi tiết người dùng trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="userDetail">UserDetail: Đối tượng chi tiết người dùng cần cập nhật.</param>
        public void UpdateUserDetail(UserDetail userDetail)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Truy vấn đối tượng cần cập nhật từ cơ sở dữ liệu
                    var user = context.UserDetails.FirstOrDefault(x => x.UserId == userDetail.UserId);

                    if (user != null)
                    {
                        // Sao chép dữ liệu từ đối tượng đầu vào (c) vào đối tượng đã truy vấn được (existingContest)
                        context.Entry(user).CurrentValues.SetValues(userDetail);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                    }
                    else
                    {
                        // Xử lý trường hợp đối tượng không tồn tại
                        throw new Exception("UserDetail not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật thông tin của một chi tiết người dùng trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="userDetail">UserDetail: Đối tượng chi tiết người dùng cần xóa.</param>
        public void DeleteUserDetail(UserDetail userDetail)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var userDetailDel = context.UserDetails.FirstOrDefault(x => x.UserId == userDetail.UserId);
                    if (userDetailDel == null)
                    {
                        // Nếu đối tượng không tồn tại, đưa ra thông báo lỗi
                        throw new Exception("Contest is null");
                    }
                    else
                    {
                        context.UserDetails.Remove(userDetailDel);
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
