using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Certificate
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICertificateService _CertificateService;

        public IndexModel(ICertificateService CertificateService)
        {
            _CertificateService = CertificateService;
        }

        public List<DataLayer.Entities.Course.Certificate> Certificates { get; set; }
        public async Task OnGet()
        {
            Certificates = await _CertificateService.GetCertificates();
        }
    }
}