using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Certificate
{
    [Authorize]
    public class UploadCertificateModel : PageModel
    {
        private readonly ICertificateService _certificateService;

        public UploadCertificateModel(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.Certificate Certificate { get; set; }

        public async Task OnGet(int id)
        {
            Certificate = await _certificateService.GetCertificateById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            await _certificateService.UploadCertificate(Certificate.CertificateId, imgLogo);

            return RedirectToPage("Index");
        }
    }
}