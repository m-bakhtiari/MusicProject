using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
   public class StudentConcert
    {
        [Key]
        public int StudentConcertId { get; set; }

        [MaxLength(800,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ConcertDate { get; set; }

        public int? Type { get; set; }

        public int? Position { get; set; }

        [MaxLength(3500)]
        public string VideoUrl { get; set; }

        public List<StudentConcertImage> StudentConcertImages { get; set; }
    }
}
