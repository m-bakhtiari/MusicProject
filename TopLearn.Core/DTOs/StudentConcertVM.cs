using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.DTOs
{
    public class StudentConcertVM
    {
        public StudentConcert StudentConcert { get; set; }
        public List<Student> Students { get; set; }
    }
}
