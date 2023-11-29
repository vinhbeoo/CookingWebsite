using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    /// <summary>
    /// Đây là lớp DAO (Data Access Object) dùng để thao tác với cơ sở dữ liệu liên quan đến Table User (người dùng).
    /// </summary>
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

        //Thể hiện duy nhất của lớp UserDAO, sử dụng mẫu thiết kế Singleton.
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        //Lấy danh sách tất cả User (người dùng) từ cơ sở dữ liệu.
        public List<User> GetUsers()
        {
            var listUsers = new List<User>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    listUsers = context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ném ngoại lệ mới với thông điệp lỗi.
                throw new Exception("Error retrieving User list: " + ex.Message);
            }
            return listUsers;
        }

        /// <summary>
        /// Tìm kiếm một User (người dùng) theo ID.
        /// </summary>
        /// <param name="userId">ID của User (người dùng) cần tìm kiếm.</param>
        /// <returns>Đối tượng người dùng được tìm thấy.</returns>
        public User FindUserById(int userId)
        {
            User user = new User();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    user = context.Users.FirstOrDefault(x => x.UserId == userId);
                }
                if (user == null)
                {
                    throw new Exception("User doesn't exists");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        /// <summary>
        /// Lưu một User (người dùng) mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="user">User: Đối tượng chi tiết người dùng cần lưu.</param>
        public void SaveUser(User user)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra trùng lặp trước khi thêm mới
                    var _user = context.Users.FirstOrDefault(x => x.UserId == user.UserId);
                    if (_user != null)
                    {
                        // Xử lý trường hợp trùng lặp (ví dụ: ném ngoại lệ hoặc cập nhật đối tượng đã tồn tại)
                        throw new Exception("User already exists");
                    }

                    // Thêm đối tượng vào cơ sở dữ liệu
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một User (người dùng) trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="user">User: Đối tượng User (người dùng) cần cập nhật.</param>
        public void UpdateUser(User user)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Truy vấn đối tượng cần cập nhật từ cơ sở dữ liệu
                    var _user = context.Users.FirstOrDefault(x => x.UserId == user.UserId);

                    if (_user != null)
                    {
                        // Sao chép dữ liệu từ đối tượng đầu vào, vào đối tượng đã truy vấn được (existingUser)
                        context.Entry(_user).CurrentValues.SetValues(user);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                    }
                    else
                    {
                        // Xử lý trường hợp đối tượng không tồn tại
                        throw new Exception("User not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete một người dùng trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="user">User: Đối tượng người dùng cần xóa.</param>
        public void DeleteUser(User user)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var userDel = context.Users.FirstOrDefault(x => x.UserId == user.UserId);
                    if (userDel == null)
                    {
                        // Nếu đối tượng không tồn tại, đưa ra thông báo lỗi
                        throw new Exception("User is null");
                    }
                    else
                    {
                        context.Users.Remove(userDel);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Check UserName va Email 
        public string CheckAddUser(User user)
        {           
            string error = null;
            try
            {
                using (var context = new CookingWebsiteContext())
                {                   
                    var _username = context.Users.Where(s => s.UserName == user.UserName).FirstOrDefault<User>();
                    var _email = context.Users.Where(s => s.Email == user.Email).FirstOrDefault<User>();
                    if (_username != null && _email != null)
                    {
                        error = "UserName and Email already exists";
                    }
                    else if (_username != null)
                    {
                        error = "UserName already exists";
                    }
                    else if (_email != null)
                    {
                        error = "Email already exists";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return error;
        }
    }
}
