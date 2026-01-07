using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IStudentConcertService
    {
        Task Add(StudentConcert studentConcert, List<IFormFile> imagesFiles);

        Task Update(StudentConcert studentConcert, List<IFormFile> imagesFiles);

        Task<List<StudentConcert>> GetAll(int type);

        Task<StudentConcert> GetItemById(int id);

        Task DeleteItem(StudentConcert studentConcert);
        Task DeleteImage(int id);
        Task<List<StudentConcertImage>> GetImagesByConcertId(int concertId);
        Task<StudentConcert> GetWorkshop();
        Task<StudentConcert> GetHavana();
        Task<List<StudentConcert>> GetBook();
        Task<int> GetItemByImageId(int id);
        Task<List<StudentConcert>> GetVahidConcert();
        Task<List<StudentConcert>> GetMedia();
    }
}
