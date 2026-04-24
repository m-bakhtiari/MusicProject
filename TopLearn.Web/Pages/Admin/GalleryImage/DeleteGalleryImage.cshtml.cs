using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.GalleryImage
{
    [PermissionChecker]
    public class DeleteGalleryImageModel : PageModel
    {
        private readonly IGalleryImageService _GalleryImageService;

        public DeleteGalleryImageModel(IGalleryImageService GalleryImageService)
        {
            _GalleryImageService = GalleryImageService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.GalleryImage GalleryImage { get; set; }
        public async Task OnGet(int id)
        {
            GalleryImage = await _GalleryImageService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var galleryImage = await _GalleryImageService.GetById(GalleryImage.GalleryImageId);
            await _GalleryImageService.DeleteGalleryImage(galleryImage);
            return RedirectToPage("Index");
        }
    }
}
