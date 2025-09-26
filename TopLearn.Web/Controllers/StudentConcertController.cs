using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

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
        
        [HttpGet("/StudentConcertInfo")]
        public async Task<IActionResult> StudentConcertInfo([FromQuery] int id)
        {
            var model = await _studentConcertService.GetItemById(id);
            return View(model);
        }

        [HttpGet("/Workshop")]
        public async Task<IActionResult> WorkshopInfo()
        {
            var model = await _studentConcertService.GetWorkshop();
            return View("StudentConcertInfo", model);
        }

        [HttpGet("/Havana")]
        public async Task<IActionResult> Havana()
        {
            var model = await _studentConcertService.GetHavana();
            return View("StudentConcertInfo", model);
        }

        [HttpGet("/Book")]
        public async Task<IActionResult> Book()
        {
            var model = await _studentConcertService.GetBook();
            return View("StudentConcertInfo", model);
        }
    }
}
