using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
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
        }
        public async Task<IActionResult> OnPost(List<IFormFile> imageList)
        {
            if (ModelState.IsValid == false)
                return Page();
            await _studentConcertService.Update(StudentConcert, imageList);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteStudentImage(int id)
        {
            // حذف تصویر از سرویس مربوطه
            await _studentConcertService.DeleteImage(id);

            // در صورت موفقیت‌آمیز بودن حذف، به عنوان پاسخ یک نتیجه JSON برگردانید
            return new JsonResult(new { success = true });
        }
    }
}
