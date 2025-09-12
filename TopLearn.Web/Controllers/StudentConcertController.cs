using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class StudentConcertController : Controller
    {
        private readonly IStudentConcertService _studentConcertService;

        public StudentConcertController(IStudentConcertService studentConcertService)
        {
            _studentConcertService = studentConcertService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _studentConcertService.GetAll((int)ConstantValue.Type.StudentConcert);
            return View(model);
        }
    }
}
