using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly TopLearnContext _context;
        public GalleryImageService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task AddGalleryImage(IFormFile imgLogo)
        {
            var gallery = new GalleryImage();
            if (imgLogo != null)
            {
                gallery.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/galleryImage", gallery.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgLogo.CopyToAsync(stream);
                }
            }
            await _context.GalleryImages.AddAsync(gallery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGalleryImage(GalleryImage galleryImage)
        {
            if (string.IsNullOrWhiteSpace(galleryImage.ImageName) == false)
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/galleryImage", galleryImage.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.GalleryImages.Remove(galleryImage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GalleryImage>> GetAllGalleryImage()
        {
            return await _context.GalleryImages.ToListAsync();
        }

        public async Task<GalleryImage> GetById(int id)
        {
            return await _context.GalleryImages.FindAsync(id);
        }
    }
}
