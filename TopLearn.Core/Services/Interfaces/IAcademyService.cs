using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
   public interface IAcademyService
    {
        List<Academy> GetAllAcademy();
        Academy GetById(int academyId);
        void AddAcademy(Academy academy,IFormFile imgLogo);
        void UpdateAcademy(Academy academy,IFormFile imgLogo);
        void DeleteAcademy(Academy academy);
    }
}
