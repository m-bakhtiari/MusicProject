using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.Certificate
{
    [PermissionChecker]
    public class PayedCertificateModel : PageModel
    {
        private readonly ICertificateService _certificateService;

        public PayedCertificateModel(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.Certificate Certificate { get; set; }
        public async Task OnGet(int id)
        {
            Certificate = await _certificateService.GetCertificateById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _certificateService.PayedCertificateById(Certificate.CertificateId);
            return RedirectToPage("Index");
        }
    }
}
