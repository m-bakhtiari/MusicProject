using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EditStudentModel : PageModel
    {
        private readonly IStudentService _studentService;

        public EditStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Student Student { get; set; }

        public async Task OnGet(int id)
        {
            Student = new DataLayer.Entities.Course.Student()
            {
                StudentImages = new List<StudentImage>()
            };
            Student = await _studentService.GetById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo, List<IFormFile> imageList)
        {
            if (!ModelState.IsValid)
                return Page();

            await _studentService.UpdateStudent(Student, imgLogo, imageList);

            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostDeleteStudentImage(int id)
        {
            // حذف تصویر از سرویس مربوطه
            var studentId= await _studentService.GetStudentByImageId(id);
            await _studentService.DeleteImage(id);
            return Redirect($"/Admin/Student/EditStudent/{studentId}");
        }
    }
}