using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.MusicNote
{
    [Authorize]
    public class DeleteMusicNoteModel : PageModel
    {
        private readonly IMusicNoteService _MusicNoteService;

        public DeleteMusicNoteModel(IMusicNoteService MusicNoteService)
        {
            _MusicNoteService = MusicNoteService;
        }
        [BindProperty]
        public DataLayer.Entities.Course.MusicNote MusicNote { get; set; }
        public async Task OnGet(int id)
        {
            MusicNote = await _MusicNoteService.GetNoteById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _MusicNoteService.DeleteNote(MusicNote.MusicNoteId);
            return RedirectToPage("Index");
        }
    }
}
