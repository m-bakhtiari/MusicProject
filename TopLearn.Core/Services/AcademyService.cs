using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class AcademyService : IAcademyService
    {
        private readonly TopLearnContext _context;

        public AcademyService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task AddAcademy(Academy academy, IFormFile imgLogo)
        {
            academy.LogoImageName = "no-photo.jpg";
            if (imgLogo != null && imgLogo.IsImage())
            {
                academy.LogoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/academy", academy.LogoImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgLogo.CopyToAsync(stream);
                }
            }
            await _context.Academies.AddAsync(academy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAcademy(Academy academy)
        {
            if (academy.LogoImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/academy", academy.LogoImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.Academies.Remove(academy);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Academy>> GetAllAcademy()
        {
            return await _context.Academies.ToListAsync();
        }

     

        public async Task<Academy> GetById(int academyId)
        {
            return await _context.Academies.FirstOrDefaultAsync(x => x.AcademyId == academyId);
        }

        public async Task UpdateAcademy(Academy academy, IFormFile imgCourse)
        {
            if (imgCourse != null && imgCourse.IsImage())
            {
                if (academy.LogoImageName != "no-photo.jpg")
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/academy", academy.LogoImageName);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                academy.LogoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/academy", academy.LogoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgCourse.CopyToAsync(stream);
                }
            }
            _context.Academies.Update(academy);
            await _context.SaveChangesAsync();
        }
    }
}
