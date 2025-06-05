using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
   public interface IAcademyService
    {
        List<Academy> GetAllAcademy();
        Academy GetById(int academyId);
        void AddAcademy(Academy academy);
        void UpdateAcademy(Academy academy);
        void DeleteAcademy(Academy academy);
    }
}
