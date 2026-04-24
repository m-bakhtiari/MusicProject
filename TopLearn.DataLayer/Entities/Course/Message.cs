using System;
using System.ComponentModel.DataAnnotations;

namespace TopLearn.DataLayer.Entities.Course
{
    public class ContactMessage
    {
        [Key]
        public int MessageId { get; set; }

        [MaxLength(7500)]
        public string Text { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsSeen { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
