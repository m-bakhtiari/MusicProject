using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using TopLearn.Core.Convertors;
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

        public void AddAcademy(Academy academy, IFormFile imgLogo)
        {
            academy.LogoImageName = "no-photo.jpg";
            if (imgLogo != null && imgLogo.IsImage())
            {
                academy.LogoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", academy.LogoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgLogo.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", academy.LogoImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 250);
            }
            _context.Academies.Add(academy);
            _context.SaveChanges();
        }

        public void DeleteAcademy(Academy academy)
        {
            if (academy.LogoImageName != "no-photo.jpg")
            {
                var deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", academy.LogoImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                var deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", academy.LogoImageName);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }
            }
            _context.Academies.Remove(academy);
            _context.SaveChanges();
        }

        public List<Academy> GetAllAcademy()
        {
            return _context.Academies.ToList();
        }

        public Academy GetById(int academyId)
        {
            return _context.Academies.FirstOrDefault(x => x.AcademyId == academyId);
        }

        public void UpdateAcademy(Academy academy, IFormFile imgCourse)
        {
            if (imgCourse != null && imgCourse.IsImage())
            {
                if (academy.LogoImageName != "no-photo.jpg")
                {
                    var deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", academy.LogoImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    var deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", academy.LogoImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                academy.LogoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", academy.LogoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                var imgResizer = new ImageConvertor();
                var thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", academy.LogoImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 250);
            }
            _context.Academies.Update(academy);
            _context.SaveChanges();
        }
    }
}
