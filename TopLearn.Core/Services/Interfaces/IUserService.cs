using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsExistMobile(string mobile);
        Task<bool> IsExistUsername(string username);
        Task<int> AddUser(User user);
        Task<User> LoginUser(LoginViewModel login);
        Task<ProfileVM> GetUserProfile(string username);
        Task<bool> CheckPermission(string username);

        Task<bool> IsUserRegistered(string username);
        Task<bool> UpdateProfile(ProfileVM profile,IFormFile imageFile);
        Task<bool> UpdatePassword(string username, string password);
        Task<User> GetUserByUsername(string username);
    }
}
