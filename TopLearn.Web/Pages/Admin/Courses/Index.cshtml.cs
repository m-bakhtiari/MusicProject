using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.Course;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Courses
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> ListCourse { get; set; }

        public async Task OnGet()
        {
            ListCourse = await _courseService.GetCoursesForAdmin();
        }
    }
}