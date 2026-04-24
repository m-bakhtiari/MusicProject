using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
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

        public async Task<bool> IsExistMobile(string mobile)
        {
            return await _context.Users.AnyAsync(u => u.Mobile == mobile);
        }
        public async Task<bool> IsExistUsername(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
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
            return await _context.Users.FirstOrDefaultAsync(u => (u.Mobile == mobile && u.Password == hashPassword && u.IsActive && !u.IsDelete) ||
            (u.UserName == mobile && u.Password == hashPassword && u.IsActive && !u.IsDelete));
        }

        public async Task<ProfileVM> GetUserProfile(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;
            return new ProfileVM()
            {
                User = user
            };
        }

        public async Task<bool> CheckPermission(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username && x.IsActive == true && x.IsAdmin == true && !x.IsDelete);
        }

        public async Task<bool> IsUserRegistered(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username || x.Mobile == username);
        }

        public async Task<bool> UpdateProfile(ProfileVM profile, IFormFile imageFile)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == profile.UserName);
            if (user == null)
                return false;
            if (string.IsNullOrWhiteSpace(user.ImageUrl) == false)
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.ImageUrl);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            if (imageFile != null && imageFile.IsImage())
            {
                user.ImageUrl = NameGenerator.GenerateUniqCode() + Path.GetExtension(imageFile.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.ImageUrl);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            user.Mobile = FixedText.FixEmail(profile.Mobile);
            user.Email = profile.Email;
            user.Address = profile.Address;
            user.FullName = profile.FullName;
            user.NationalCode = profile.NationalCode;
            user.PostalCode = profile.PostalCode;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePassword(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
                return false;
            user.Password = PasswordHelper.EncodePasswordMd5(password);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByUsername(string username)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
