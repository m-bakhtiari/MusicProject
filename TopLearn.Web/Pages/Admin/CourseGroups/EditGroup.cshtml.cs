using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    [PermissionChecker]
    public class EditGroupModel : PageModel
    {
        private ICourseService _courseService;

        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroup CourseGroups { get; set; }

        public async Task OnGet(int id)
        {
            CourseGroups = await _courseService.GetById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _courseService.UpdateGroup(CourseGroups, imgLogo);

            return RedirectToPage("Index");
        }
    }
}