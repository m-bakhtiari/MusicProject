using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    [Authorize]
    public class DeleteGroupModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly TopLearnContext _context;

        public DeleteGroupModel(ICourseService courseService, TopLearnContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        public CourseGroup Groups { get; set; }
        public void OnGet(int id)
        {
            ViewData["GroupId"] = id;

            Groups = _courseService.GetById(id);
        }

        public IActionResult OnPost(CourseGroup group)
        {
            if (_context.CourseGroups.Any(x => x.ParentId == group.GroupId))
            {
                ViewData["Error"] = "ابتدا تمام زیر گروه های این گروه را حذف نمایید";
                return RedirectToPage("Index");
            }

            if (_context.Courses.Any(x => x.GroupId == group.GroupId))
            {
                ViewData["Error"] = "برای این گروه محصولاتی ثبت شده است . امکان حذف وجود ندارد";
                return RedirectToPage("Index");
            }
            _courseService.DeleteGroup(group);
            ViewData["Error"] = null;
            return RedirectToPage("Index");
        }
    }
}
