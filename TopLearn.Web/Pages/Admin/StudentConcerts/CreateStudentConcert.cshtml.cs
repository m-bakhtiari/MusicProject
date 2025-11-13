using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
    [Authorize]
    public class CreateStudentConcertModel : PageModel
    {
        private readonly IStudentConcertService _studentConcertService;
        public CreateStudentConcertModel(IStudentConcertService studentConcertService)
        {
            _studentConcertService = studentConcertService;
        }
        [BindProperty]
        public StudentConcert StudentConcert { get; set; }
        public void OnGet(int? type)
        {
            StudentConcert = new StudentConcert()
            {
                Type = type
            };
            ViewData["Type"] = type;
            if (type == (int)ConstantValue.Type.StudentConcert)
                ViewData["Header"] = "کنسرت هنرجویی";
            else if (type == (int)ConstantValue.Type.Workshop)
                ViewData["Header"] = "ورکشاپ";
            else if (type == (int)ConstantValue.Type.Havana)
                ViewData["Header"] = "کلاس گروهی";
            else if (type == (int)ConstantValue.Type.Book)
                ViewData["Header"] = "کتاب";
        }

        public async Task<IActionResult> OnPost(List<IFormFile> imageList)
        {
            if (ModelState.IsValid == false)
                return Page();
            await _studentConcertService.Add(StudentConcert, imageList);
            return Redirect($"/Admin/StudentConcerts?type={StudentConcert.Type}");
        }
    }
}
