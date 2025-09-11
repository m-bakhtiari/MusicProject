using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
   public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(255)]
        public string Thread { get; set; }


        [Required]
        [MaxLength(50)]
        public string Level { get; set; }


        [Required]
        [MaxLength(255)]
        public string Logger { get; set; }


        [Required]
        [MaxLength(12000)]
        public string Message { get; set; }
    }
}
