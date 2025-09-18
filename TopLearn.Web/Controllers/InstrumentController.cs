using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly IInstrumentService _instrumentService;

        public InstrumentController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _instrumentService.GetAll();
            return View(model);
        }
    }
}
