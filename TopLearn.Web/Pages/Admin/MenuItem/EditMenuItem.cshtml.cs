using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.Security;


namespace TopLearn.Web.Pages.Admin.MenuItem
{
    [PermissionChecker]
    public class EditMenuItemModel : PageModel
    {
        private readonly IMenuItemService _menuItemService;

        public EditMenuItemModel(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.MenuItem MenuItem { get; set; }

        public async Task OnGet(int id)
        {
            MenuItem = await _menuItemService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _menuItemService.UpdateMenuItem(MenuItem);

            return RedirectToPage("Index");
        }

    }
}