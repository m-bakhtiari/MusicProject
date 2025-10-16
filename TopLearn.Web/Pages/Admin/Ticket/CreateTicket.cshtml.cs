using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Ticket
{
    [Authorize]
    public class CreateTicketModel : PageModel
    {
        private readonly ITicketService _TicketService;

        public CreateTicketModel(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.ConcertTicket Ticket { get; set; }

        public async Task OnGet(int? id)
        {
            Ticket = new DataLayer.Entities.Course.ConcertTicket();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _TicketService.AddTicketManual(Ticket);

            return RedirectToPage("Index");
        }
    }
}