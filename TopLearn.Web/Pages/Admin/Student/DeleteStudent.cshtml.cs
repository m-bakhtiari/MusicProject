using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Student
{
    [Authorize]
    public class DeleteStudentModel : PageModel
    {
        private readonly IStudentService _studentService;

        public DeleteStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.Student Student { get; set; }
        public async Task OnGet(int id)
        {
            Student = await _studentService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var student = await _studentService.GetById(Student.StudentId);
            await _studentService.DeleteStudent(student);
            return RedirectToPage("Index");
        }
    }
}
