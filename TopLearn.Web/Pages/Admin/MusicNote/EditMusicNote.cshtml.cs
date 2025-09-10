using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.MusicNote
{
    public class EditMusicNoteModel : PageModel
    {
        private readonly IMusicNoteService _musicNoteService;
        private readonly IInstrumentService _instrumentService;

        public EditMusicNoteModel(IMusicNoteService musicNoteService, IInstrumentService instrumentService)
        {
            _musicNoteService = musicNoteService;
            _instrumentService = instrumentService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.MusicNote MusicNote { get; set; }

        [Authorize]
        public async Task OnGet(int id)
        {
            var groups = await _instrumentService.GetAll();
            ViewData["Instruments"] = new SelectList(groups, "Value", "Text",MusicNote.InstrumentId);
            MusicNote = await _musicNoteService.GetNoteById(id);
        }

        [Authorize]
        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _musicNoteService.UpdateNote(MusicNote, imgLogo);

            return RedirectToPage("Index");
        }

    }
}