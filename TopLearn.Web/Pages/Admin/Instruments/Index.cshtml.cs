using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Web.Pages.Admin.Instruments
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private IInstrumentService _instrumentService;

        public IndexModel(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        public List<Instrument> Instruments { get; set; }
        public async Task OnGet()
        {
            Instruments = await _instrumentService.GetAll();
        }
    }
}