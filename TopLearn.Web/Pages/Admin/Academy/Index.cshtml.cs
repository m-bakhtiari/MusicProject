using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Academy
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAcademyService _academyService;

        public IndexModel(IAcademyService academyService)
        {
            _academyService = academyService;
        }

        public List<DataLayer.Entities.Course.Academy> Academies { get; set; }
        public void OnGet()
        {
            Academies = _academyService.GetAllAcademy();
        }
    }
}