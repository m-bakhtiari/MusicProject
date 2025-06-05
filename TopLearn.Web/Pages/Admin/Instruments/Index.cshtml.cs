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
    public class IndexModel : PageModel
    {
        private IInstrumentService _instrumentService;

        public IndexModel(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        public List<Instrument> Instruments { get; set; }
        public void OnGet()
        {
            Instruments = _instrumentService.GetAll();
        }
    }
}