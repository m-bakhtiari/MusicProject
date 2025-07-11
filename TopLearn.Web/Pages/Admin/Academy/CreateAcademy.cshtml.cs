﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Academy
{
    [Authorize]
    public class CreateAcademyModel : PageModel
    {
        private readonly IAcademyService _academyService;

        public CreateAcademyModel(IAcademyService academyService)
        {
            _academyService = academyService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Academy Academy { get; set; }

        public void OnGet(int? id)
        {
            Academy = new DataLayer.Entities.Course.Academy() { };
        }

        public async Task<IActionResult> OnPost(IFormFile imgLogo)
        {
            if (!ModelState.IsValid)
                return Page();

            await _academyService.AddAcademy(Academy, imgLogo);

            return RedirectToPage("Index");
        }
    }
}