using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LogApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IAcademyService _academyService;
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentConcertService _studentConcertService;

        public HomeController(IInstrumentService instrumentService, IAcademyService academyService, ILogger<HomeController> logger, IStudentConcertService studentConcertService)
        {
            _instrumentService = instrumentService;
            _academyService = academyService;
            _logger = logger;
            _studentConcertService = studentConcertService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ItemForIndexDto()
            {
                Instruments = await _instrumentService.GetAll(),
                Academies = await _academyService.GetAllAcademy()
            };
            return View(model);
        }

        [HttpGet("Gallery")]
        public IActionResult Gallery()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var data = HttpContext.Request.Headers["LogData"];
            _logger.LogError(data);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[Route("OnlinePayment/{id}")]
        //public IActionResult onlinePayment(int id)
        //{
        //    if (HttpContext.Request.Query["Status"] != "" &&
        //        HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
        //        && HttpContext.Request.Query["Authority"] != "")
        //    {
        //        string authority = HttpContext.Request.Query["Authority"];

        //        var wallet = _userService.GetWalletByWalletId(id);

        //        var payment = new ZarinpalSandbox.Payment(wallet.Amount);
        //        var res = payment.Verification(authority).Result;
        //        if (res.Status == 100)
        //        {
        //            ViewBag.code = res.RefId;
        //            ViewBag.IsSuccess = true;
        //            wallet.IsPay = true;
        //            _userService.UpdateWallet(wallet);
        //        }

        //    }

        //    return View();
        //}
    }
}