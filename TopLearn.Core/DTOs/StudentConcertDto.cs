using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.DTOs
{
   public class StudentConcertDto
    {
        public List<StudentConcert> StudentConcerts { get; set; }
        public List<StudentConcertImage> Images { get; set; }
    }
}
