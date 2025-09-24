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
        public async Task<IActionResult> Index(int pageId = 1, string filter = "",int instrumentId = 0)
        {
            var items = await _musicNoteService.GetMusicNote(pageId, filter, instrumentId, 5);
            var notes = await _musicNoteService.GetAllNotes();
            var model = new NoteViewModel()
            {
                MusicNotes = notes.OrderByDescending(x => x.MusicNoteId).Take(10).ToList(),
                NoteViewModelItem = items,
                Instruments = await _instrumentService.GetInstrumentHasNote()
            };
            if (instrumentId == 0)
                ViewData["A"] = "active";
            else
                ViewData["A"] = "";
            ViewBag.pageId = pageId;
            ViewBag.instrumentId = instrumentId;
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
