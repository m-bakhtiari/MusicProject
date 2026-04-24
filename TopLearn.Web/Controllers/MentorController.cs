using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
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
            var model = await _studentService.GetAllStudent((int)ConstantValue.StudentType.Mentor);
            return View(model);
        }

        [HttpGet("/Info")]
        public async Task<IActionResult> Info([FromQuery]int id, [FromQuery] string name)
        {
            var model = await _studentService.GetById(id);
            return View("MentorInfo",model);
        }

        [Route("/TopStudent")]
        public async Task<IActionResult> TopStudent()
        {
            var model = await _studentService.GetAllStudent((int)ConstantValue.StudentType.TopStudent);
            return View("Index",model);
        }

    }
}
