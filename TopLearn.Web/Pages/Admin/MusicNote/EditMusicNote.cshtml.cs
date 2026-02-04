using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.MusicNote
{
    [PermissionChecker]
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

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _musicNoteService.UpdateNote(MusicNote, imgLogo);

            return RedirectToPage("Index");
        }

    }
}