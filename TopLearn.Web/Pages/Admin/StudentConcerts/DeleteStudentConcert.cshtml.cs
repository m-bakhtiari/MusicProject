using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
    [PermissionChecker]
    public class DeleteStudentConcertModel : PageModel
    {
        private readonly IStudentConcertService _studentConcertService;

        public DeleteStudentConcertModel(IStudentConcertService studentConcertService)
        {
            _studentConcertService = studentConcertService;
        }
        [BindProperty]
        public StudentConcert StudentConcert { get; set; }
        public async Task OnGet(int id)
        {
            StudentConcert = await _studentConcertService.GetItemById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var model = await _studentConcertService.GetItemById(StudentConcert.StudentConcertId);
            await _studentConcertService.DeleteItem(model);
            return Redirect($"/Admin/StudentConcerts?type={model.Type}");
        }
    }
}
