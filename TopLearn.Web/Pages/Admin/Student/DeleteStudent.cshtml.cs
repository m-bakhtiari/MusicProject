using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Student
{
    [PermissionChecker]
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
            return Redirect($"/Admin/Student?type={student.ShortKey}");
        }
    }
}
