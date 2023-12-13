using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
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
        /// <summary>
        /// CRUD dùng cho admin
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Logic dùng cho việc xác thực Email
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserByEmailOrUserName(string input)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == input || u.UserName == input);
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.EmailConfirmationToken == token);

                    if (user != null)
                    {
                        user.EmailConfirmed = true;
                        user.EmailConfirmationToken = null; // Xác nhận thành công, loại bỏ token
                        await context.SaveChangesAsync();

                        return true;
                    }
                }


                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error confirming email: " + ex.Message);
            }
        }
    }
}
