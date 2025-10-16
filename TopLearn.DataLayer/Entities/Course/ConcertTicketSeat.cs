using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class ConcertTicketSeat
    {
        [Key]
        public int ConcertTicketSeatId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SeatNumber { get; set; }

        [Required]
        public int ConcertTicketId { get; set; }

        public bool IsPay { get; set; }
        public DateTime? CreatedDate { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }
        [ForeignKey(nameof(ConcertTicketId))]
        public ConcertTicket ConcertTicket { get; set; }


    }
}
