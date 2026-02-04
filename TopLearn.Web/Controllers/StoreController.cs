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

        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
                ViewData["Target"] = "Product";
            else
                ViewData["Target"] = "Store";
            var model = await _courseService.GetAllGroup(id);
            return View(model);
        }

        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> Product([FromQuery] int id)
        {
            var model = await _courseService.GetProductsBySubGroup(id);
            return View(model);
        }

        [HttpGet]
        [Route("ProductInfo")]
        public async Task<IActionResult> ProductInfo([FromQuery] int id)
        {
            var model = await _courseService.GetCourseById(id);
            return View(model);
        }
    }
}
