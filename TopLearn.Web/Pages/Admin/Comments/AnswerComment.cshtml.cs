using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.Comments
{
    [PermissionChecker]
    public class AnswerCommentModel : PageModel
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public AnswerCommentModel(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        [BindProperty]
        public Comment Comment { get; set; }

        public async Task OnGet(int id)
        {
            var old = await _commentService.GetCommentById(id);
            var user = await _userService.GetUserByUsername(User.Identity.Name);
            Comment = new Comment()
            {
                ParentId = id,
                CommentType = old.CommentType,
                ProductId = old.ProductId,
                InstrumentId = old.InstrumentId,
                StudentConcertId = old.StudentConcertId,
                Mobile = user.Mobile,
                FullName = user.FullName,
                UserId = user.UserId
            };
        }

        public async Task<IActionResult> OnPost()
        {
            Comment.IsShowOnSite = true;
            await _commentService.AddComment(Comment);
            return RedirectToPage("Index");
        }
    }
}
