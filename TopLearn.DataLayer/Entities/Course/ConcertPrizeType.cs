using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class ConcertPrizeType
    {
        [Key]
        public int Id { get; set; }

        [Required]  
        public string Name { get; set; }
    }
}
