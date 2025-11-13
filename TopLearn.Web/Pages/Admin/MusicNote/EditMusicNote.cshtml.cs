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
            MusicNote = await _musicNoteService.GetNoteById(id);
            var groups = await _instrumentService.GetAll();
            var groupSelect = groups.Select(x => new SelectListItem()
            {
                Text = x.InstrumentTitle,
                Value = x.InstrumentId.ToString()
            });
            ViewData["Instruments"] = new SelectList(groupSelect, "Value", "Text");
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