using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class MediaController : Controller
    {
        private readonly IStudentConcertService _studentConcertService;
        public MediaController(IStudentConcertService studentConcertService)
        {
            _studentConcertService = studentConcertService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _studentConcertService.GetMedia();
            return View(model);
        }

        [HttpGet("/MediaInfo")]
        public async Task<IActionResult> MediaInfo([FromQuery] int id)
        {
            var model = await _studentConcertService.GetItemById(id);
            return View("MediaInfo", model);
        }
    }
}
