using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Pages.Admin.MenuItem
{
    [PermissionChecker]
    public class DeleteMenuItemModel : PageModel
    {
        private readonly IMenuItemService _menuItemService;

        public DeleteMenuItemModel(IMenuItemService menuItemService)
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
            await _menuItemService.DeleteMenuItem(MenuItem);
            return RedirectToPage("Index");
        }
    }
}
