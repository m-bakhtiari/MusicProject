using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.Video
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IVideoService _videoService;

        public IndexModel(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public List<DataLayer.Entities.Course.Video> Videos { get; set; }
        public async Task OnGet()
        {
            Videos = await _videoService.GetAllVideo();
        }
    }
}