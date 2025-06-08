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
    public class EditCourseModel : PageModel
    {
        private ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Product Product { get; set; }
        public async Task OnGet(int id)
        {
            Product = await _courseService.GetCourseById(id);

            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Product.GroupId);

            List<SelectListItem> subgroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            subgroups.AddRange(await _courseService.GetSubGroupForManageCourse(Product.GroupId));
            string selectedSubGroup = null;
            if (Product.SubGroup != null)
            {
                selectedSubGroup = Product.SubGroup.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", selectedSubGroup);

        }

        public async Task<IActionResult> OnPost(IFormFile imgCourseUp)
        {
            if (!ModelState.IsValid)
                return Page();

            await _courseService.UpdateCourse(Product, imgCourseUp);

            return RedirectToPage("Index");
        }
    }
}