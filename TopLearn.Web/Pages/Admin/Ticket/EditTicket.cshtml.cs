using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Ticket
{
    [PermissionChecker]
    public class EditTicketModel : PageModel
    {
        private readonly ITicketService _TicketService;

        public EditTicketModel(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.ConcertTicket Ticket { get; set; }

       
        public async Task OnGet(int id)
        {
            Ticket = await _TicketService.GetTicketById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _TicketService.UpdateTicketManual(Ticket);

            return RedirectToPage("Index");
        }

    }
}