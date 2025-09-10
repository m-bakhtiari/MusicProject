using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
   public class StudentService:IStudentService
   {
       private readonly TopLearnContext _context;

       public StudentService(TopLearnContext context)
       {
           _context = context;
       }

        public async Task AddStudent(Student student, IFormFile imgLogo)
        {
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
        }

        public async Task DeleteStudent(Student student)
        {
            if (student.ImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/student", student.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudent()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetById(int studentId)
        {
            return await _context.Students.FindAsync(studentId);
        }

        public async Task UpdateStudent(Student student, IFormFile imgLogo)
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
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
