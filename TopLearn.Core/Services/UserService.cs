using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        private TopLearnContext _context;

        public UserService(TopLearnContext context)
        {
            _context = context;
        }


        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public async Task<bool> IsExistMobile(string mobile)
        {
            return await _context.Users.AnyAsync(u => u.Mobile == mobile);
        }

        public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string mobile = FixedText.FixEmail(login.Mobile);
            return await _context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile && u.Password == hashPassword);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }


        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }


        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }
    }
}
