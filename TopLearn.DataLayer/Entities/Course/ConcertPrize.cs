using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class ConcertPrize
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserIp { get; set; }

        [Required]
        public int PrizeTypeId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(PrizeTypeId))]
        public virtual ConcertPrizeType ConcertPrizeType { get; set; }
    }
}
