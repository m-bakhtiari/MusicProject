using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly TopLearnContext _context;

        public StudentService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student, IFormFile imgLogo, List<IFormFile> imagesFiles)
        {
            student.ShortKey = await GenerateShortKey();
            student.ImageName = "no-photo.jpg";
            if (imgLogo != null && imgLogo.IsImage())
            {
                student.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", student.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgLogo.CopyToAsync(stream);
                }
            }
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            if (imagesFiles != null)
            {
                foreach (var file in imagesFiles)
                {
                    var photo = new StudentImage()
                    {
                        StudentId = student.StudentId,
                        ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName)
                    };
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", photo.ImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.StudentImages.AddAsync(photo);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            if (EnumerableExtensions.Any(student.StudentImages))
            {
                foreach (var img in student.StudentImages)
                {
                    if (img.ImageName != "no-photo.jpg")
                    {
                        var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/studentConcert", img.ImageName);
                        if (File.Exists(deleteImagePath))
                        {
                            File.Delete(deleteImagePath);
                        }
                    }
                }
            }
            _context.StudentImages.RemoveRange(student.StudentImages);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudent()
        {
            return await _context.Students.OrderBy(x => Guid.NewGuid()).ToListAsync();
        }

        public async Task<Student> GetById(int studentId)
        {
            return await _context.Students.Include(x => x.StudentImages).FirstOrDefaultAsync(x => x.StudentId == studentId);
        }

        public async Task<int> GetStudentByImageId(int imageId)
        {
            var image = await _context.StudentImages.FindAsync(imageId);
            return image.StudentId;
        }
        public async Task UpdateStudent(Student student, IFormFile imgLogo, List<IFormFile> imagesFiles)
        {
            if (imgLogo != null && imgLogo.IsImage())
            {
                if (student.ImageName != "no-photo.jpg")
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", student.ImageName);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                student.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", student.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgLogo.CopyToAsync(stream);
                }
            }
            if (imagesFiles != null)
            {
                foreach (var file in imagesFiles)
                {
                    var photo = new StudentImage()
                    {
                        StudentId = student.StudentId,
                        ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName)
                    };
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", photo.ImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.StudentImages.AddAsync(photo);
                }
            }
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteImage(int id)
        {
            var img = await _context.StudentImages.FindAsync(id);
            if (img.ImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", img.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.StudentImages.Remove(img);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByKey(string key)
        {
            return await _context.Students.Include(x => x.StudentImages).FirstOrDefaultAsync(x => x.ShortKey.Equals(key));
        }
        private async Task<string> GenerateShortKey(int length = 4)
        {
            var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);

            while (await _context.Students.AnyAsync(s => s.ShortKey == key))
            {
                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
            }

            return key;
        }
    }
}
