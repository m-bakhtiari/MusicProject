using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudent();
        Task<Student> GetById(int studentId);
        Task AddStudent(Student student, IFormFile imgLogo, List<IFormFile> imageList);
        Task UpdateStudent(Student student, IFormFile imgLogo, List<IFormFile> imageList);
        Task DeleteStudent(Student student);
        Task DeleteImage(int id);
        Task<Student> GetStudentByKey(string key);
        Task<int> GetStudentByImageId(int imageId);

    }
}
