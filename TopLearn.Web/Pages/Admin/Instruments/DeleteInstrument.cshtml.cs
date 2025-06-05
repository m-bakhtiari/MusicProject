using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Instruments
{
    public class DeleteInstrumentModel : PageModel
    {
        private readonly IInstrumentService _instrumentService;

        public DeleteInstrumentModel(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        public Instrument Instruments { get; set; }
        public void OnGet(int id)
        {
            Instruments = _instrumentService.GetById(id);
        }

        public IActionResult OnPost()
        {
            _instrumentService.DeleteInstrument(Instruments);
            return RedirectToPage("Index");
        }
    }
}
