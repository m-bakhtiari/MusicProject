using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.Core.DTOs
{
    public class TicketDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public int TicketCount { get; set; }
        public string SeatNumber { get; set; }
    }
}
