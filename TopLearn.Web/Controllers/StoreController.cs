using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly ICourseService _courseService;
        public StoreController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index(int? groupId)
        {
            if (groupId == null)
                groupId = 0;
            var model = await _courseService.GetCourse(groupId: groupId.Value);
            ViewBag.pageId = 1;
            return View(model);
        }

        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> Product([FromQuery] int id)
        {
            var model = await _courseService.GetProductsBySubGroup(id);
            return View(model);
        }
    }
}
