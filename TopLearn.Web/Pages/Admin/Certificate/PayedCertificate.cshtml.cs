using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Certificate
{
    [Authorize]
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
