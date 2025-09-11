using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.MusicNote
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMusicNoteService _musicNoteService;

        public IndexModel(IMusicNoteService musicNoteService)
        {
            _musicNoteService = musicNoteService;
        }

        public List<DataLayer.Entities.Course.MusicNote> MusicNotes { get; set; }
        public async Task OnGet()
        {
            MusicNotes = await _musicNoteService.GetAllNotes();
        }
    }
}