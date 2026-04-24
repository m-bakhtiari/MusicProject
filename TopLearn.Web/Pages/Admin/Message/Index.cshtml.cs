using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Message
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IContactMessageService _contactMessage;
        public IndexModel(IContactMessageService contactMessage)
        {
            _contactMessage = contactMessage;
        }
        public List<ContactMessage> ContactMessages { get; set; }

        public async Task OnGet()
        {
            ContactMessages = await _contactMessage.GetContactMessages();
        }

        public async Task<IActionResult> OnPostToggleSeenStatus(int id)
        {
            await _contactMessage.ToggleSeenStatus(id);
            return Redirect("/Admin/Message/Index");
        }
    }
}
