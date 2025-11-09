using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ICertificateService _certificateService;
        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("RequestCertificate")]
        public async Task<IActionResult> RequestCertificate(string firstName, string lastName, string mobile, string nationalCode,
            string academy, string instrument, string address, string postalCode, string description)
        {
            await _certificateService.AddCertificate(firstName, lastName, mobile, nationalCode, academy, instrument, description,address,postalCode);
            return View("Index");
        }

        [HttpGet]
        [Route("ShowCertificate")]
        public async Task<IActionResult> ShowCertificate(string mobile)
        {
            var model = await _certificateService.GetCertificatesByMobile(mobile);
            return View(model);
        }

        [HttpGet("DownloadCertificate/{id}")]
        public async Task<IActionResult> DownloadCertificate(int id)
        {
            var data = await _certificateService.GetCertificateById(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/certificate", data.FileName);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(imagePath);
            return File(fileBytes, "application/pdf", data.FileName);
        }
    }
}
