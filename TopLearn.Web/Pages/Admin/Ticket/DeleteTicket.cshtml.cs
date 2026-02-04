using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Ticket
{
    [PermissionChecker]
    public class DeleteTicketModel : PageModel
    {
        private readonly ITicketService _TicketService;

        public DeleteTicketModel(ITicketService TicketService)
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
            await _TicketService.DeleteTicketManual(Ticket);
            return RedirectToPage("Index");
        }
    }
}
