using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IStudentConcertService _studentConcertService;

        public IndexModel(IStudentConcertService studentConcertService)
        {
            _studentConcertService = studentConcertService;
        }
        public List<StudentConcert> StudentConcert { get; set; }
        public async Task OnGet(int type)
        {
            ViewData["type"] = type;
            if (type == (int)ConstantValue.Type.StudentConcert)
                ViewData["Header"] = "کنسرت هنرجویی";
            else if (type == (int)ConstantValue.Type.Workshop)
                ViewData["Header"] = "ورکشاپ";
            else if (type == (int)ConstantValue.Type.Havana)
                ViewData["Header"] = "کلاس گروهی";
            else if (type == (int)ConstantValue.Type.Book)
                ViewData["Header"] = "کتاب";
            StudentConcert = await _studentConcertService.GetAll(type);
        }
    }
}
