using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.Academy
{
    [PermissionChecker]
    public class EditAcademyModel : PageModel
    {
        private readonly IAcademyService _academyService;

        public EditAcademyModel(IAcademyService academyService)
        {
            _academyService = academyService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Academy Academy { get; set; }

        public async Task OnGet(int id)
        {
            Academy = await _academyService.GetById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _academyService.UpdateAcademy(Academy, imgLogo);

            return RedirectToPage("Index");
        }
    }
}