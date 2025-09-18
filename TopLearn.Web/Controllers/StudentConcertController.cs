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
            var concert = await _studentConcertService.GetAll((int)ConstantValue.Type.StudentConcert);
            var image = await _studentConcertService.GetImages();
            var model = new StudentConcertDto()
            {
                Images = image.OrderByDescending(x => Guid.NewGuid()).Take(10).ToList(),
                StudentConcerts = concert
            };
            return View(model);
        }
    }
}
