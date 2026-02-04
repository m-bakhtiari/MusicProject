using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.MusicNote
{
    [PermissionChecker]
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
