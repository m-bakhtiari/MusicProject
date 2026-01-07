using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Login")]
        public ActionResult Login(bool EditProfile = false)
        {
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    await HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    //if (ReturnUrl != "/")
                    //{
                    //    return Redirect(ReturnUrl);
                    //}
                    //return View();
                    return Redirect("/Admin");
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }


        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(string mobile, string password)
        {
            var exist = await _userService.IsExistMobile(mobile);
            if (exist)
            {
                ViewData["Message"] = "این شماره موبایل قبلا در سایت ثبت نام شده است";
                return View("Register");
            }
            else
            {
                var user = new User()
                {
                    IsActive = true,
                    IsDelete = false,
                    Mobile = mobile,
                    Password = PasswordHelper.EncodePasswordMd5(password),
                    UserName = mobile,
                    RegisterDate = DateTime.Now,
                    Email = ""
                };
                await _userService.AddUser(user);
                ViewData["Message"] = "این شماره موبایل قبلا در سایت ثبت نام شده است";
                return View("Login");
            }
        }
    }
}