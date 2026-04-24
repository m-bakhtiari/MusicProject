using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Video
{
    [PermissionChecker]
    public class DeleteVideoModel : PageModel
    {
        private readonly IVideoService _VideoService;

        public DeleteVideoModel(IVideoService VideoService)
        {
            _VideoService = VideoService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.Video Video { get; set; }
        public async Task OnGet(int id)
        {
            Video = await _VideoService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var video = await _VideoService.GetById(Video.VideoId);
            await _VideoService.DeleteVideo(video);
            return RedirectToPage("Index");
        }
    }
}
