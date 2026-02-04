using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;

namespace TopLearn.Web.Pages.Admin
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}