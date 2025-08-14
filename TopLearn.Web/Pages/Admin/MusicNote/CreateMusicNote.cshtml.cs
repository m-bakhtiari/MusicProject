using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.MusicNote
{
    [Authorize]
    public class CreateMusicNoteModel : PageModel
    {
        private readonly IMusicNoteService _MusicNoteService;

        public CreateMusicNoteModel(IMusicNoteService MusicNoteService)
        {
            _MusicNoteService = MusicNoteService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.MusicNote MusicNote { get; set; }

        public void OnGet(int? id)
        {
            MusicNote = new DataLayer.Entities.Course.MusicNote() { };
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _MusicNoteService.AddNote(MusicNote, imgLogo);

            return RedirectToPage("Index");
        }
    }
}