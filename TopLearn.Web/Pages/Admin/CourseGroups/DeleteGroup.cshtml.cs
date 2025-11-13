using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public async Task OnGet(int id)
        {
            ViewData["GroupId"] = id;

            Groups = await _courseService.GetById(id);
        }

        public async Task<IActionResult> OnPost(CourseGroup group)
        {          

            if (await _context.Courses.AnyAsync(x => x.GroupId == group.GroupId))
            {
                ViewData["Error"] = "برای این گروه محصولاتی ثبت شده است . امکان حذف وجود ندارد";
                return RedirectToPage("Index");
            }
            await _courseService.DeleteGroup(group);
            ViewData["Error"] = null;
            return RedirectToPage("Index");
        }
    }
}
