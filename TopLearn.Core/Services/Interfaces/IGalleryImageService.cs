using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IGalleryImageService
    {
        Task<List<GalleryImage>> GetAllGalleryImage();
        Task AddGalleryImage(IFormFile imgLogo);
        Task DeleteGalleryImage(GalleryImage galleryImage);
        Task<GalleryImage> GetById(int id);
    }
}
