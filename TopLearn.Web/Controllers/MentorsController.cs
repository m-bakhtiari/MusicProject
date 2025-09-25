using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class MentorsController : Controller
    {
        private readonly IStudentService _studentService;

        public MentorsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _studentService.GetAllStudent();
            return View(model);
        }
    }
}
