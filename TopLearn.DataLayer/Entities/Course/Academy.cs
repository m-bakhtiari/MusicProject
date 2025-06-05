using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
   public class Academy
    {
        [Key]
        public int AcademyId { get; set; }

        [Display(Name = "عنوان آموزشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string AcademyTitle { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(4200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Address { get; set; }


        [Display(Name = "روزهای تدریس")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActiveDays { get; set; }

        [Display(Name = "سازهای قابل تدریس ")]
        [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LearningInstrument { get; set; }
    }
}
