using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.Academy
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IAcademyService _academyService;

        public IndexModel(IAcademyService academyService)
        {
            _academyService = academyService;
        }

        public List<DataLayer.Entities.Course.Academy> Academies { get; set; }
        public async Task OnGet()
        {
            Academies = await _academyService.GetAllAcademy();
        }
    }
}