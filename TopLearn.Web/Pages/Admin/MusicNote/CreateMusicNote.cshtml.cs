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
    public class CreateMusicNoteModel : PageModel
    {
        private readonly IMusicNoteService _musicNoteService;
        private readonly IInstrumentService _instrumentService;

        public CreateMusicNoteModel(IMusicNoteService musicNoteService, IInstrumentService instrumentService)
        {
            _musicNoteService = musicNoteService;
            _instrumentService = instrumentService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.MusicNote MusicNote { get; set; }

        public async Task OnGet(int? id)
        {
            var groups = await _instrumentService.GetAll();
            var groupSelect = groups.Select(x => new SelectListItem()
            {
                Text = x.InstrumentTitle,
                Value = x.InstrumentId.ToString()
            });
            ViewData["Instruments"] = new SelectList(groupSelect, "Value", "Text");
            MusicNote = new DataLayer.Entities.Course.MusicNote() { };
        }

        public async Task<IActionResult> OnPost(IFormFile noteFile,IFormFile imageFile)
        {
            if (!ModelState.IsValid)
                return Page();

            await _musicNoteService.AddNote(MusicNote, noteFile,imageFile);

            return RedirectToPage("Index");
        }
    }
}