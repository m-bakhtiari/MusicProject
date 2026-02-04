using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Web.Pages.Admin.Instruments
{
    [PermissionChecker]
    public class DeleteInstrumentModel : PageModel
    {
        private readonly IInstrumentService _instrumentService;

        public DeleteInstrumentModel(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }
        [BindProperty]
        public Instrument Instruments { get; set; }
        public async Task OnGet(int id)
        {
            Instruments = await _instrumentService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var instrument = await _instrumentService.GetById(Instruments.InstrumentId);
            await _instrumentService.DeleteInstrument(instrument);
            return RedirectToPage("Index");
        }
    }
}
