using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
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
