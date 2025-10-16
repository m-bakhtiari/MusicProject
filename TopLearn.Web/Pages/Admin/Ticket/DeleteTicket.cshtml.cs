using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Ticket
{
    [Authorize]
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
