using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
   public class StudentConcertImage
    {
        [Key]
        public int StudentConcertImageId { get; set; }

        [Required]
        [Display(Name = "نام تصویر")]
        [MaxLength(500,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ImageName { get; set; }

        [Required]
        public int StudentConcertId { get; set; }

        [ForeignKey(nameof(StudentConcertId))]
        public virtual StudentConcert StudentConcert { get; set; }
    }
}
