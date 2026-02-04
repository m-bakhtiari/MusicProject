using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Web.Pages.Admin.Instruments
{
    [PermissionChecker]
    public class EditInstrumentModel : PageModel
    {
        private readonly IInstrumentService _instrumentService;

        public EditInstrumentModel(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [BindProperty]
        public Instrument Instruments { get; set; }

        public async Task OnGet(int id)
        {
            Instruments = await _instrumentService.GetById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile imgInstrumentUp, IFormFile imgInstrumentLogoUp)
        {
            if (!ModelState.IsValid)
                return Page();

            await _instrumentService.UpdateInstrument(Instruments, imgInstrumentUp, imgInstrumentLogoUp);

            return RedirectToPage("Index");
        }
    }
}