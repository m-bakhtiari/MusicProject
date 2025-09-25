using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.MenuItem
{
    [Authorize]
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
