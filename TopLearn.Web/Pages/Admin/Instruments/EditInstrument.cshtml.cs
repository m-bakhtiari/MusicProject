using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Instruments
{
    [Authorize]
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

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _instrumentService.UpdateInstrument(Instruments);

            return RedirectToPage("Index");
        }
    }
}