using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Ticket
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ITicketService _TicketService;

        public IndexModel(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        public List<DataLayer.Entities.Course.ConcertTicket> Tickets { get; set; }
        public async Task OnGet()
        {
            Tickets = await _TicketService.GetReservedSeat();
        }
    }
}