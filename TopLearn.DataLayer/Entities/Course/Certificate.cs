using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Certificate
    {
        [Key]
        public int CertificateId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string NationalCode { get; set; }

        [Required]
        [MaxLength(25)]
        public string Mobile { get; set; }

        [MaxLength(800)]
        public string Academy { get; set; }
        public string Description { get; set; }

        [MaxLength(1500)]
        public string Instrument { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsPay { get; set; }

        [Required]
        public bool IsDone { get; set; }

        [MaxLength(500)]
        public string FileName { get; set; }

        public string Address { get; set; }

        [MaxLength(200)]
        public string PostalCode { get; set; }

        [MaxLength(200)]
        public string TrackingCode { get; set; }

        [MaxLength(50)]
        public string SendDate { get; set; }
    }
}
