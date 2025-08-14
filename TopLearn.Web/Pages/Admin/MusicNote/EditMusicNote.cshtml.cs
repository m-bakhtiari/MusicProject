using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.MusicNote
{
    public class EditMusicNoteModel : PageModel
    {
        private readonly IMusicNoteService _MusicNoteService;

        public EditMusicNoteModel(IMusicNoteService MusicNoteService)
        {
            _MusicNoteService = MusicNoteService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.MusicNote MusicNote { get; set; }

        [Authorize]
        public async Task OnGet(int id)
        {
            MusicNote = await _MusicNoteService.GetNoteById(id);
        }

        [Authorize]
        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _MusicNoteService.UpdateNote(MusicNote, imgLogo);

            return RedirectToPage("Index");
        }

    }
}