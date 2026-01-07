using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        Task<bool> IsExistMobile(string mobile);
        Task<int> AddUser(User user);
        Task<User> LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        User GetUserByUserName(string username);
        void UpdateUser(User user);
        void DeleteUser(int userId);

        #region User Panel

        bool CompareOldPassword(string oldPassword, string username);

        void ChangeUserPassword(string userName, string newPassword);

        #endregion

        #region Admin Panel

        UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        #endregion
    }
}
