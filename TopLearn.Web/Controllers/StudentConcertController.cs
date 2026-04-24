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
        private readonly IStudentService _studentService;

        public StudentConcertController(IStudentConcertService studentConcertService, IStudentService studentService)
        {
            _studentConcertService = studentConcertService;
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _studentConcertService.GetAll((int)ConstantValue.Type.StudentConcert);
            ViewData["typeUrl"] = "/StudentConcertInfo?id=";
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
            var havana = await _studentConcertService.GetHavana();
            var student = await _studentService.GetAllStudent((int)ConstantValue.StudentType.TopStudent);
            return View("Havana", new StudentConcertVM()
            {
                StudentConcert = havana,
                Students = student
            });
        }

        [HttpGet("/Book")]
        public async Task<IActionResult> Book()
        {
            var model = await _studentConcertService.GetBook();
            ViewBag.typeUrl = "/BookInfo?id=";
            return View("Index", new List<StudentConcert>() { model });
        }

        [HttpGet("/BookInfo")]
        public async Task<IActionResult> BookInfo(int id)
        {
            var model = await _studentConcertService.GetItemById(id);
            return View("StudentConcertInfo", model);
        }


    }
}
