using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Student
{
    [Authorize]
    public class CreateStudentModel : PageModel
    {
        private readonly IStudentService _studentService;

        public CreateStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.Student Student { get; set; }

        public void OnGet(int? id)
        {
            Student = new DataLayer.Entities.Course.Student() { StudentImages = new List<StudentImage>() };
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo, List<IFormFile> imageList)
        {
            if (!ModelState.IsValid)
                return Page();

            await _studentService.AddStudent(Student, imgLogo, imageList);

            return RedirectToPage("Index");
        }
    }
}