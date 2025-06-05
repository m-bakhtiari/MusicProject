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

        public DataLayer.Entities.Course.Academy Academy { get; set; }
        public void OnGet(int id)
        {
            Academy = _academyService.GetById(id);
        }

        public IActionResult OnPost()
        {
            _academyService.DeleteAcademy(Academy);
            return RedirectToPage("Index");
        }
    }
}
