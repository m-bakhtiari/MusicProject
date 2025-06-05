using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.User
{
   public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string MessageText { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string PhoneNumber { get; set; }

        [MaxLength(1500)]
        public string Title { get; set; }
    }
}
