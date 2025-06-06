using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
   public class Instrument
    {
        [Key]
        public int InstrumentId { get; set; }

        [DisplayName("عنوان ساز")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string InstrumentTitle { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [DisplayName("تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
    }
}
