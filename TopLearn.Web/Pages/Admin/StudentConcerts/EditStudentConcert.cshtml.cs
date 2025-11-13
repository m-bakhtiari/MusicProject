using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
    [Authorize]
    public class EditStudentConcertModel : PageModel
    {
        private readonly IStudentConcertService _studentConcertService;

        public EditStudentConcertModel(IStudentConcertService studentConcertService)
        {
            _studentConcertService = studentConcertService;
        }
        [BindProperty]
        public StudentConcert StudentConcert { get; set; }
        public async Task OnGet(int id)
        {
            StudentConcert = await _studentConcertService.GetItemById(id);
            ViewData["Type"] = StudentConcert.Type;
            if (StudentConcert.Type == (int)ConstantValue.Type.StudentConcert)
                ViewData["Header"] = "کنسرت هنرجویی";
            else if (StudentConcert.Type == (int)ConstantValue.Type.Workshop)
                ViewData["Header"] = "ورکشاپ";
            else if (StudentConcert.Type == (int)ConstantValue.Type.Havana)
                ViewData["Header"] = "کلاس گروهی";
            else if (StudentConcert.Type == (int)ConstantValue.Type.Book)
                ViewData["Header"] = "کتاب";
        }
        public async Task<IActionResult> OnPost(List<IFormFile> imageList)
        {
            if (ModelState.IsValid == false)
                return Page();
            await _studentConcertService.Update(StudentConcert, imageList);
            return Redirect($"/Admin/StudentConcerts?type={StudentConcert.Type}");
        }

        public async Task<IActionResult> OnPostDeleteStudentImage(int id)
        {
            var model = await _studentConcertService.GetItemByImageId(id);
            await _studentConcertService.DeleteImage(id);
            return Redirect($"/Admin/StudentConcerts/EditStudentConcert/{model}");
        }
    }
}
