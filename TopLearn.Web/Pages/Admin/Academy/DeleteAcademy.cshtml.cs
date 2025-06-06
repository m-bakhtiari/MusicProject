using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Academy
{
    [Authorize]
    public class DeleteAcademyModel : PageModel
    {
        private readonly IAcademyService _academyService;

        public DeleteAcademyModel(IAcademyService academyService)
        {
            _academyService = academyService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.Academy Academy { get; set; }
        public async Task OnGet(int id)
        {
            Academy = await _academyService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var academy = await _academyService.GetById(Academy.AcademyId);
            await _academyService.DeleteAcademy(academy);
            return RedirectToPage("Index");
        }
    }
}
