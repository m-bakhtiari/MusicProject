using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Instruments
{
    [Authorize]
    public class CreateInstrumentModel : PageModel
    {
        private readonly IInstrumentService _instrumentService;

        public CreateInstrumentModel(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [BindProperty]
        public Instrument Instruments { get; set; }

        public void OnGet(int? id)
        {
            Instruments = new Instrument() { };
        }

        public async Task<IActionResult> OnPost(IFormFile imgInstrumentUp,IFormFile imgInstrumentLogoUp)
        {
            if (!ModelState.IsValid)
                return Page();

            await _instrumentService.AddInstrument(Instruments, imgInstrumentUp, imgInstrumentLogoUp);

            return RedirectToPage("Index");
        }
    }
}