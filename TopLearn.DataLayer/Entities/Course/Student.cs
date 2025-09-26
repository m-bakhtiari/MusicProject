using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [MaxLength(200)]
        public string StudentFullName { get; set; }

        [MaxLength(200)]
        public string LearningInstrument { get; set; }

        public string Description { get; set; }

        [MaxLength(500)]
        public string ImageName { get; set; }

        public int? Position { get; set; }

        [MaxLength(5)]
        public string ShortKey { get; set; }

        public List<StudentImage> StudentImages { get; set; }
    }
}
