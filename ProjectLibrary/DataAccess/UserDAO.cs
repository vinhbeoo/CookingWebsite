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

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var users = await context.Users.ToListAsync();
                    return users;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }

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

        public async Task<User> CreateUser(User user)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                    if (existingUser != null)
                    {
                        throw new Exception("Email is already taken.");
                    }

                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }

        public async Task<User> DeleteUser(User user)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingUser = await context.Users.FindAsync(user.UserId);
                    if (existingUser != null)
                    {
                        context.Users.Remove(existingUser);
                        await context.SaveChangesAsync();
                        return existingUser;
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
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
