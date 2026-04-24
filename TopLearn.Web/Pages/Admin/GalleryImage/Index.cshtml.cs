using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.GalleryImage
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IGalleryImageService _GalleryImageService;

        public IndexModel(IGalleryImageService GalleryImageService)
        {
            _GalleryImageService = GalleryImageService;
        }

        public List<DataLayer.Entities.Course.GalleryImage> GalleryImages { get; set; }
        public async Task OnGet()
        {
            GalleryImages = await _GalleryImageService.GetAllGalleryImage();
        }
    }
}