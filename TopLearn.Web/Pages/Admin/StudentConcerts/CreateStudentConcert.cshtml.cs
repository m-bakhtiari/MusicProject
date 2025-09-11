using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
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
        }

        public async Task<IActionResult> OnPost(List<IFormFile> imageList)
        {
            if (ModelState.IsValid == false)
                return Page();
            await _studentConcertService.Add(StudentConcert, imageList);
            return RedirectToPage("Index");
        }
    }
}
