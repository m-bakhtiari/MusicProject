using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IAcademyService
    {
        Task<List<Academy>> GetAllAcademy();
        Task<Academy> GetById(int academyId);
        Task AddAcademy(Academy academy, IFormFile imgLogo);
        Task UpdateAcademy(Academy academy, IFormFile imgLogo);
        Task DeleteAcademy(Academy academy);
       
    }
}
