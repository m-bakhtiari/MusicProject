using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Video
{
    [PermissionChecker]
    public class CreateVideoModel : PageModel
    {
        private readonly IVideoService _videoService;

        public CreateVideoModel(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Video Video { get; set; }

        public void OnGet()
        {
            Video = new DataLayer.Entities.Course.Video() { };
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _videoService.AddVideo(Video, imgLogo);

            return RedirectToPage("Index");
        }
    }
}