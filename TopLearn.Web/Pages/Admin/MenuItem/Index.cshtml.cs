using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.MenuItem
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMenuItemService _menuItemService;

        public IndexModel(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public List<DataLayer.Entities.Course.MenuItem> MenuItems { get; set; }
        public async Task OnGet()
        {
            MenuItems = await _menuItemService.GetAllMenuItem();
        }
    }
}