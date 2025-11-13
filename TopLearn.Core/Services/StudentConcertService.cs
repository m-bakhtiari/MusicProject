using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class StudentConcertService : IStudentConcertService
    {
        private readonly TopLearnContext _context;

        public StudentConcertService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task Add(StudentConcert studentConcert, List<IFormFile> imagesFiles)
        {
            var insert = await _context.StudentConcerts.AddAsync(studentConcert);

            if (imagesFiles != null)
            {
                foreach (var file in imagesFiles)
                {
                    var photo = new StudentConcertImage()
                    {
                        StudentConcertId = insert.Entity.StudentConcertId,
                        ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName)
                    };
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/studentConcert", photo.ImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.StudentConcertImages.AddAsync(photo);
                }
            }
            else
            {
                var img = new StudentConcertImage
                {
                    ImageName = "no-photo.jpg",
                    StudentConcertId = insert.Entity.StudentConcertId
                };
                await _context.StudentConcertImages.AddAsync(img);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(int id)
        {
            var img = await _context.StudentConcertImages.FindAsync(id);
            if (img.ImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/studentConcert", img.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.StudentConcertImages.Remove(img);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(StudentConcert studentConcert)
        {
            if (EnumerableExtensions.Any(studentConcert.StudentConcertImages))
            {
                foreach (var img in studentConcert.StudentConcertImages)
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
            _context.StudentConcertImages.RemoveRange(studentConcert.StudentConcertImages);
            _context.StudentConcerts.Remove(studentConcert);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentConcert>> GetAll(int type)
        {
            var res = _context.StudentConcerts.OrderByDescending(x => x.Position).Where(x => x.Type == type);
            return await res.Include(x => x.StudentConcertImages).ToListAsync();
        }

        public async Task<StudentConcert> GetItemById(int id)
        {
            return await _context.StudentConcerts.Include(x => x.StudentConcertImages)
                .FirstOrDefaultAsync(x => x.StudentConcertId == id);
        }

        public async Task<int> GetItemByImageId(int id)
        {
            var model = await _context.StudentConcertImages.FindAsync(id);
            return model.StudentConcertId;
        }

        public async Task<List<StudentConcertImage>> GetImagesByConcertId(int concertId)
        {
            return await _context.StudentConcertImages.Where(x=>x.StudentConcertId==concertId).ToListAsync();
        }

        public async Task<StudentConcert> GetWorkshop()
        {
            return await _context.StudentConcerts.Include(x=>x.StudentConcertImages).FirstOrDefaultAsync(x => x.Type == (int)ConstantValue.Type.Workshop);
        }
        public async Task<StudentConcert> GetHavana()
        {
            return await _context.StudentConcerts.Include(x => x.StudentConcertImages).FirstOrDefaultAsync(x => x.Type == (int)ConstantValue.Type.Havana);
        }
        public async Task<List<StudentConcert>> GetBook()
        {
            return await _context.StudentConcerts.Include(x => x.StudentConcertImages).Where(x => x.Type == (int)ConstantValue.Type.Book).ToListAsync();
        }
        public async Task Update(StudentConcert studentConcert, List<IFormFile> imagesFiles)
        {
            _context.StudentConcerts.Update(studentConcert);
            if (imagesFiles != null)
            {
                foreach (var file in imagesFiles)
                {
                    var photo = new StudentConcertImage()
                    {
                        StudentConcertId = studentConcert.StudentConcertId,
                        ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName)
                    };
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/studentConcert", photo.ImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.StudentConcertImages.AddAsync(photo);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
