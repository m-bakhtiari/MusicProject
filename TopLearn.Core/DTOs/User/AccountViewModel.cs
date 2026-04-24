using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.DTOs
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        public string Mobile { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        public string RePassword { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
    }


    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public class ProfileVM
    {
        public User User { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public IFormFile ImageUrl { get; set; }

        public string FullName { get; set; }

        public string NationalCode { get; set; }
    }
}
