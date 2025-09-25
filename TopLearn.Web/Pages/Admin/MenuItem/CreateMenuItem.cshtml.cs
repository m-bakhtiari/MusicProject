using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.MenuItem
{
    [Authorize]
    public class CreateMenuItemModel : PageModel
    {
        private readonly IMenuItemService _menuItemService;

        public CreateMenuItemModel(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.MenuItem MenuItem { get; set; }

        public async Task OnGet(int? id)
        {
            MenuItem = new DataLayer.Entities.Course.MenuItem();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _menuItemService.AddMenuItem(MenuItem);

            return RedirectToPage("Index");
        }
    }
}