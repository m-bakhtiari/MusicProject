using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class DeleteCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task OnGet(int id)
        {
            ViewData["CourseId"] = id;
            Product = await _courseService.GetCourseById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _courseService.DeleteCourse(Product.ProductId);
            return RedirectToPage("Index");
        }
    }
}
