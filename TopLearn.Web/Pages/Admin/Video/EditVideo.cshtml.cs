using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.Video
{
    [PermissionChecker]
    public class EditVideoModel : PageModel
    {
        private readonly IVideoService _VideoService;

        public EditVideoModel(IVideoService VideoService)
        {
            _VideoService = VideoService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Video Video { get; set; }

        public async Task OnGet(int id)
        {
            Video = await _VideoService.GetById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _VideoService.UpdateVideo(Video, imgLogo);

            return RedirectToPage("Index");
        }
    }
}