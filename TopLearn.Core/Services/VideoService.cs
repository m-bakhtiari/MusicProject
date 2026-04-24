using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class VideoService : IVideoService
    {
        private readonly TopLearnContext _context;

        public VideoService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task AddVideo(Video video, IFormFile imgLogo)
        {
            if (imgLogo != null)
            {
                video.ThumbnailImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/video", video.ThumbnailImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgLogo.CopyToAsync(stream);
                }
            }
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideo(Video video)
        {
            if (string.IsNullOrWhiteSpace(video.ThumbnailImage) == false)
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/video", video.ThumbnailImage);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Video>> GetAllVideo()
        {
            return await _context.Videos.OrderBy(x => x.Position).ToListAsync();
        }



        public async Task<Video> GetById(int videoId)
        {
            return await _context.Videos.FirstOrDefaultAsync(x => x.VideoId == videoId);
        }

        public async Task UpdateVideo(Video video, IFormFile imgCourse)
        {
            if (imgCourse != null)
            {
                if (string.IsNullOrWhiteSpace(video.ThumbnailImage) == false)
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/video", video.ThumbnailImage);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                video.ThumbnailImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/video", video.ThumbnailImage);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgCourse.CopyToAsync(stream);
                }
            }
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }
    }
}
