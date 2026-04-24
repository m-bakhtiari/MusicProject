using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Comments
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly ICommentService _commentService;
        public IndexModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public List<Comment> Comments { get; set; }
        public async Task OnGet()
        {
            Comments = await _commentService.GetAllComments();
        }

        public IActionResult OnPost(int id)
        {
            return null;
        }

        public async Task<IActionResult> OnPostShowInSite(int id)
        {
            await _commentService.ToggleShowStatus(id);
            return Redirect("/Admin/Comments/Index");
        }
    }
}
