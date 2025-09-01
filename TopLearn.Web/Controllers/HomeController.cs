using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IAcademyService _academyService;

        public HomeController(IInstrumentService instrumentService, IAcademyService academyService)
        {
            _instrumentService = instrumentService;
            _academyService = academyService;
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

    }
}