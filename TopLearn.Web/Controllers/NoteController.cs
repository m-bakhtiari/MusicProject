using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class NoteController : Controller
    {
        private readonly IMusicNoteService _musicNoteService;

        public NoteController(IMusicNoteService musicNoteService)
        {
            _musicNoteService = musicNoteService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _musicNoteService.GetAllNotes();
            return View(model);
        }
    }
}
