using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class MentorController : Controller
    {
        private readonly IStudentService _studentService;

        public MentorController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _studentService.GetAllStudent();
            return View(model);
        }

        [HttpGet("/MentorInfo")]
        public async Task<IActionResult> MentorInfo([FromQuery]int id, [FromQuery] string name)
        {
            var model = await _studentService.GetById(id);
            return View(model);
        }

        [HttpGet("/m")]
        public async Task<IActionResult> MentorInfoByKey([FromQuery] string key)
        {
            var model = await _studentService.GetStudentByKey(key);
            return View("MentorInfo",model);
        }
    }
}
