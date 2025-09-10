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
        Task AddStudent(Student student, IFormFile imgLogo);
        Task UpdateStudent(Student student, IFormFile imgLogo);
        Task DeleteStudent(Student student);
    }
}
