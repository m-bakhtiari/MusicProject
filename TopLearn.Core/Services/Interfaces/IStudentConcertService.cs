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
       Task Add(StudentConcert studentConcert,List<IFormFile> imagesFiles);

       Task Update(StudentConcert studentConcert, List<IFormFile> imagesFiles);

       Task<List<StudentConcert>> GetAll(int type);

       Task<StudentConcert> GetItemById(int id);

       Task DeleteItem(StudentConcert studentConcert);
       Task DeleteImage(int id);
       Task<List<StudentConcertImage>> GetImages();
   }
}
