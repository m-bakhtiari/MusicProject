using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    public class DeleteGroupModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DeleteGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public CourseGroup Groups { get; set; }
        public void OnGet(int id)
        {
            ViewData["GroupId"] = id;

            Groups = _courseService.GetById(id);
        }

        public IActionResult OnPost(CourseGroup group)
        {
            _courseService.DeleteGroup(group);

            return RedirectToPage("Index");
        }
    }
}
