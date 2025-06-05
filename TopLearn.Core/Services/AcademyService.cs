using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class AcademyService : IAcademyService
    {
        private readonly TopLearnContext _context;

        public AcademyService(TopLearnContext context)
        {
            _context = context;
        }

        public void AddAcademy(Academy academy)
        {
            _context.Academies.Add(academy);
            _context.SaveChanges();
        }

        public void DeleteAcademy(Academy academy)
        {
            _context.Academies.Remove(academy);
            _context.SaveChanges();
        }

        public List<Academy> GetAllAcademy()
        {
            return _context.Academies.ToList();
        }

        public Academy GetById(int academyId)
        {
            return _context.Academies.FirstOrDefault(x => x.AcademyId == academyId);
        }

        public void UpdateAcademy(Academy academy)
        {
            _context.Academies.Update(academy);
            _context.SaveChanges();
        }
    }
}
