using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class ConcertTicket
    {
        [Key]
        public int ConcertTicketId { get; set; }

        [Required]
        [MaxLength(500)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(500)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        public string Mobile { get; set; }

        [Required]
        public int TicketCount { get; set; }

        public int Amount { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public List<ConcertTicketSeat> ConcertTicketSeats { get; set; }
    }
}
