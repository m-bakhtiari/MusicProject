using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseGroup> CourseGroups { get; set; }
        public async Task OnGet()
        {
            CourseGroups = await _courseService.GetAllGroup();
        }
    }
}