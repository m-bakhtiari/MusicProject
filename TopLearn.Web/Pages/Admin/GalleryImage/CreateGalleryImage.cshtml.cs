using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.GalleryImage
{
    [PermissionChecker]
    public class CreateGalleryImageModel : PageModel
    {
        private readonly IGalleryImageService _GalleryImageService;

        public CreateGalleryImageModel(IGalleryImageService GalleryImageService)
        {
            _GalleryImageService = GalleryImageService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.GalleryImage GalleryImage { get; set; }

        public void OnGet(int? id)
        {
            GalleryImage = new DataLayer.Entities.Course.GalleryImage() { };
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _GalleryImageService.AddGalleryImage(imgLogo);

            return RedirectToPage("Index");
        }
    }
}