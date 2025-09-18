using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class AcademyController : Controller
    {
        private readonly IAcademyService _academy;

        public AcademyController(IAcademyService academy)
        {
            _academy = academy;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _academy.GetAllAcademy();
            return View(model);
        }
    }
}
