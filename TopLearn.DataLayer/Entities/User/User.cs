using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {
            
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsDelete { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        [MaxLength(100)]
        public string PostalCode { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string NationalCode { get; set; }
    }
}
