using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class NoteController : Controller
    {
        private readonly IMusicNoteService _musicNoteService;
        private readonly IInstrumentService _instrumentService;

        public NoteController(IMusicNoteService musicNoteService, IInstrumentService instrumentService)
        {
            _musicNoteService = musicNoteService;
            _instrumentService = instrumentService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _musicNoteService.GetMusicNote(1, "", null, 15);
            var notes = await _musicNoteService.GetAllNotes();
            var model = new NoteViewModel()
            {
                MusicNotes = notes,
                NoteViewModelItem = items
            };
            ViewData["A"] = "active";
            return View(model);
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var data = await _musicNoteService.GetNoteById(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", data.FileName);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(imagePath);
            return File(fileBytes, "application/pdf", data.FileName);
        }
    }
}
