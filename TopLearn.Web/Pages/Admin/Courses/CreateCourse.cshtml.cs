using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Courses
{
    [Authorize]
    public class CreateCourseModel : PageModel
    {
        private ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task OnGet()
        {
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            if (groups.Any())
            {
                var subGroups = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
                ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");
            }

        }

        public async Task<IActionResult> OnPost(IFormFile imgCourseUp)
        {
            if (!ModelState.IsValid)
                return Page();

            await _courseService.AddCourse(Product, imgCourseUp);

            return RedirectToPage("Index");
        }
    }
}