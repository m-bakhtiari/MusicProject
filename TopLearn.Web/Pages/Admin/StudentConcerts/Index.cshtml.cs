using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Pages.Admin.StudentConcerts
{
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
            StudentConcert = await _studentConcertService.GetAll(type);
        }
    }
}
