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

        [Display(Name = "عکس لوگو آموزشگاه ")]
        [MaxLength(100,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LogoImageName { get; set; }

        [Display(Name = "تلفن")]
        [MaxLength(50,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Phone { get; set; }

        [Display(Name = "آدرس سایت")]
        [MaxLength(100,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Website { get; set; }
    }
}
