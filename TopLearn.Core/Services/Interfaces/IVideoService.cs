using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IVideoService
    {
        Task<List<Video>> GetAllVideo();
        Task<Video> GetById(int videoId);
        Task AddVideo(Video video, IFormFile imgLogo);
        Task UpdateVideo(Video video, IFormFile imgLogo);
        Task DeleteVideo(Video video);
    }
}
