using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Subscribers
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly ISubscriberService _subscriberService;
        public IndexModel(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [BindProperty]
        public List<Subscriber> Subscribers { get; set; }

        public async Task OnGet()
        {
            Subscribers = await _subscriberService.GetSubscribers();
        }
    }
}
