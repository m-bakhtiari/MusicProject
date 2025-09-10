using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
